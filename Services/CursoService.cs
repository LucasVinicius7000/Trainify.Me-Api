using Trainify.Me_Api.Infra.Data.Repositories;
using Trainify.Me_Api.Domain.Entities;

namespace Trainify.Me_Api.Services
{
    public class CursoService
    {
        private readonly IService _services;
        private readonly IRepository _repositories;

        public CursoService(IService services, IRepository repositories)
        {
            _repositories = repositories;
            _services = services;
        }

        public async Task<Curso> CriarCurso(Curso curso)
        {
            try
            {
                await _repositories.BeginTransaction();
                if (string.IsNullOrEmpty(curso.Nome)) throw new Exception("Nome do curso inválido.");
                if (curso.OrganizacaoId <= 0) throw new Exception("O id da organização do curso é inválido.");
                if (string.IsNullOrEmpty(curso.UsuarioCriadorId)) throw new Exception("O id da usuário criador é invalido.");

                var cursoCriado = await _repositories.Curso.CriarCurso(curso);
                if (cursoCriado is null) throw new Exception("Ocorreu um desconhecido ao criar o curso.");
                await _repositories.CommitTransaction();
                return cursoCriado;

            }
            catch (Exception ex)
            {
                await _repositories.RollBackTransaction();
                throw new Exception(ex.Message);
            }
        }

        public async Task<Curso> AtualizarCurso(Curso curso)
        {
            try
            {
                if (curso is null || curso.Id <= 0)
                    throw new Exception("Dados do curso inválidos, erro ao atualizar.");
                var cursoAtualizado = await _repositories.Curso.EditarCurso(curso);
                if(cursoAtualizado is null)
                    throw new Exception("Dados do curso inválidos, erro ao atualizar.");
                return cursoAtualizado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Curso> BuscarCursoPorId(int id)
        {
            try
            {
                if (id <= 0)
                    throw new Exception("O id do curso é inválido.");
                var curso = await _repositories.Curso.GetCursoById(id);
                curso.Aulas = await _repositories.Aula.BuscarAulasPorCursoId(id);
                foreach(var aula in curso.Aulas)
                {
                    if(aula.Atividade != null)
                    {
                        aula.Atividade.Alternativas = await _repositories.Aula.BuscarAlternativasPorAtividadeId(aula.Atividade.Id);
                    }
                }
                var aulasOrdernadas = curso.Aulas.OrderBy(a => a.Indice).ToList();
                curso.Aulas = aulasOrdernadas;
                if (curso is null || curso.Aulas is null)
                    throw new Exception("Falha ao buscar o curso.");
                return curso;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Curso>> BuscarCursoPorOrganizacaoId(int id)
        {
            try
            {
                if (id <= 0)
                    throw new Exception("O id da organização é inválido.");
                var curso = await _repositories.Curso.ListaCursosPelaOrganizacao(id);
                if (curso is null)
                    throw new Exception("Falha ao listar cursos.");
                return curso;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Curso> AtivarCursoExistente(int cursoId)
        {
            try
            {
                var curso = await _repositories.Curso.AtivarCurso(cursoId);
                if (curso is null)
                    throw new Exception("Ocorreu um erro ao ativar o curso.");
                return curso;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> AlunoJaEstaMatriculadoEmCurso(Aluno aluno, Curso curso)
        {
            try
            {
                var cursosEmAndamento = await _repositories.Curso.BuscaCursoEmAndamentoByAlunoId(aluno.Id);
                foreach(var cursoAtual in cursosEmAndamento)
                {
                    if(cursoAtual.CursoBaseId == curso.Id)
                        return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<CursoEmAndamento> MatricularAlunoEmCurso(int alunoId, int cursoId)
        {
            try
            {
                var aluno = await _repositories.Aluno.BuscarAlunoPorId(alunoId);
                if (aluno is null)
                    throw new Exception("Erro ao matricular. Aluno não encontrado.");
                var curso = await _repositories.Curso.GetCursoById(cursoId);
                if(curso is null)
                    throw new Exception("Erro ao matricular. Curso não encontrado.");

                var aulasOrdenadas = curso.Aulas.OrderBy(a => a.Indice).ToList();

                var jaMatriculado = await AlunoJaEstaMatriculadoEmCurso(aluno, curso);
                if (jaMatriculado)
                    throw new Exception("Aluno já está matriculado no curso.");

                var cursoMatriculado = new CursoEmAndamento()
                {
                    AlunoId = alunoId,
                    CursoBaseId = curso.Id,
                    MatriculadoEm = DateTime.UtcNow,
                    StatusCurso = Domain.Enums.StatusCursoAndamento.NaoIniciado,
                    AulaAtualId = aulasOrdenadas.First().Id,
                };

                var matricula = await _repositories.Curso.CriarCursoEmAndamento(cursoMatriculado);
                if (matricula is null)
                    throw new Exception("Erro ao matricular aluno.");
                return matricula;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<CursoEmAndamento>> ListarCursosEmAndamentoPorAlunoId(int alunoId)
        {
            try
            {
                if (alunoId <= 0)
                    throw new Exception("Id do aluno é inválido. Não foi possível listar cursos.");

                var cursos = await _repositories.Curso.BuscaCursoEmAndamentoByAlunoId(alunoId);
                
                if (cursos is null)
                    throw new Exception("Ocorreu um erro ao listar cursos do aluno.");

                var listCurso = new List<CursoEmAndamento>();
                foreach(var curso in cursos)
                {
                    var cursoBase = await _repositories.Curso.GetCursoById(curso.CursoBaseId);
                    if(cursoBase is not null)
                    {
                        curso.CursoBase = cursoBase;
                        listCurso.Add(curso);
                    }
                    
                }   
                return listCurso;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<CursoEmAndamento> BuscarCursoEmAndamentoPorId(int cursoId)
        {
            try
            {
                var cursoEmAndamento = await _repositories.Curso.BuscaCursoEmAndamentoById(cursoId);
                if (cursoEmAndamento is null)
                    throw new Exception("Erro ao buscar curso em andamento.");
                return cursoEmAndamento;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Curso> ExcluirCurso(int cursoId)
        {
            try
            {
                if (cursoId <= 0)
                    throw new Exception("O id não corresponde a um curso existente.");
                var cursoExcluido = await _repositories.Curso.ExcluirCurso(cursoId);
                if (cursoExcluido == null)
                    throw new Exception("Falha ao excluir curso.");
                return cursoExcluido;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
