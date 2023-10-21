using Trainify.Me_Api.Infra.Data.Repositories;
using Trainify.Me_Api.Domain.Entities;
using Trainify.Me_Api.Application.Models.Requests;
using Trainify.Me_Api.Infra.Services.BlobStorage;

namespace Trainify.Me_Api.Services
{
    public class AulaService
    {
        private readonly IService _services;
        private readonly IRepository _repositories;
        private readonly IServiceProvider _serviceProvider;

        public AulaService(IService services, IRepository repositories, IServiceProvider serviceProvider)
        {
            _repositories = repositories;
            _services = services;
            _serviceProvider = serviceProvider;
        }

        public async Task<Aula> AdicionarAula(AdicionarAulaRequest adicionarAulaRequest)
        {
            try
            {
                await _repositories.BeginTransaction();
                if (adicionarAulaRequest.DadosAula is null && (string.IsNullOrEmpty(adicionarAulaRequest.AlternativaCorreta) || string.IsNullOrEmpty(adicionarAulaRequest.EnunciadoQuestao) || adicionarAulaRequest.Alternativas is null))
                    throw new Exception("Os dados da aula são inválidos.");

                var cursoRelacionado = await _repositories.Curso.GetCursoById(adicionarAulaRequest.CursoId);
                if (cursoRelacionado is null)
                    throw new Exception("Falha ao adicionar aula, curso relacionado não encontrado.");

                if (cursoRelacionado.Aulas.Any(a => a.Indice == adicionarAulaRequest.IndiceAula))
                    throw new Exception("Falha ao adicionar aula, já existe uma aula com esse indice.");

                if (adicionarAulaRequest.IndiceAula == 0)
                    adicionarAulaRequest.IndiceAula = 1;

                var aula = new Aula()
                {
                    CursoId = adicionarAulaRequest.CursoId,
                    TipoAula = adicionarAulaRequest.TipoAula,
                    Indice = adicionarAulaRequest.IndiceAula,
                    Titulo = adicionarAulaRequest.TituloAula,
                };

                var aulaCriada = await _repositories.Aula.CriarAula(aula);

                if(adicionarAulaRequest.TipoAula == Domain.Enums.TipoAula.Video || adicionarAulaRequest.TipoAula == Domain.Enums.TipoAula.PDF)
                {
                    var blobService = _serviceProvider.GetService<IBlobStorageService>();
                    if (blobService is null)
                        throw new Exception("Falha no serviço de upload de arquivos, tente mais tarde.");

                    if (
                        adicionarAulaRequest.DadosAula.MimeType is null ||
                        string.IsNullOrEmpty(adicionarAulaRequest.DadosAula.ExtensaoArquivo) ||
                        string.IsNullOrEmpty(adicionarAulaRequest.DadosAula.NomeArquivo) ||
                        string.IsNullOrEmpty(adicionarAulaRequest.DadosAula.Base64Arquivo)  
                    ) throw new Exception("Os dados do arquivo não são válidos.");

                    var data = new MemoryStream(Convert.FromBase64String(adicionarAulaRequest.DadosAula.Base64Arquivo));
                    if (data is null) throw new Exception("Erro ao ler os dados do arquivo.");

                    var urlDoArquivo = await blobService.UploadFile(adicionarAulaRequest.DadosAula.NomeArquivo, adicionarAulaRequest.DadosAula.ExtensaoArquivo, adicionarAulaRequest.DadosAula.MimeType, data);
                    if (string.IsNullOrEmpty(urlDoArquivo) || string.IsNullOrWhiteSpace(urlDoArquivo))
                        throw new Exception("Erro ao realizar upload do arquivo.");

                    if (adicionarAulaRequest.TipoAula == Domain.Enums.TipoAula.PDF) aulaCriada.PDFUrl = urlDoArquivo;
                    else if (adicionarAulaRequest.TipoAula == Domain.Enums.TipoAula.Video) aulaCriada.VideoUrl = urlDoArquivo;

                }
                else if(adicionarAulaRequest.TipoAula == Domain.Enums.TipoAula.Atividade)
                {
                    if (!adicionarAulaRequest.Alternativas.Contains(adicionarAulaRequest.AlternativaCorreta))
                        throw new Exception("A lista de alternativas precisa conter a alternativa correta.");

                    var atividade = new Atividade()
                    {
                        Enunciado = adicionarAulaRequest.EnunciadoQuestao,
                        AulaId = aulaCriada.Id,
                    };
                    var atividadeCriada = await _repositories.Aula.CriarAtividade(atividade);

                    aulaCriada.AtividadeId = atividadeCriada.Id;
                    if (atividadeCriada is null)
                        throw new Exception("Erro ao criar atividade.");

                    var alternativas = new List<Alternativa>();
                    foreach(string alternativa in adicionarAulaRequest.Alternativas)
                    {
                        var alternativaAtual = new Alternativa()
                        {
                            Valor = alternativa,
                            AtividadeId = atividadeCriada.Id,
                        };
                        var result = await _repositories.Aula.CriarAlternativa(alternativaAtual);
                        if(result is null)
                            throw new Exception("Falha ao criar alternativa.");
                        alternativas.Add(result);
                    }

                    var alternativaCorreta = alternativas.Where(a => a.Valor.Equals(adicionarAulaRequest.AlternativaCorreta)).First();
                    atividadeCriada.AlternativaCorretaId = alternativaCorreta.Id;

                    var atividadeAtualizada = await _repositories.Aula.AtualizarAtividade(atividadeCriada);
                    if (atividadeAtualizada is null)
                        throw new Exception("Falha ao atualizar atividade.");    
                }

                var aulaAtualizada = await _repositories.Aula.AtualizarAula(aulaCriada);
                if (aulaAtualizada is null)
                    throw new Exception("Falha ao criar aula.");
                await _repositories.CommitTransaction();
                return aulaAtualizada;

            }
            catch (Exception ex)
            {
                await _repositories.RollBackTransaction();
                throw new Exception(ex.Message);
            }
        }
    
    
 
    
    }
}
