using Trainify.Me_Api.Infra.Data.Repositories;
using Trainify.Me_Api.Application.Models.Requests;
using Trainify.Me_Api.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Trainify.Me_Api.Services
{
    public class PerfilService
    {
        private readonly IService _services;
        private readonly IRepository _repositories;

        public PerfilService(IService services, IRepository repositories)
        {
            _repositories = repositories;
            _services = services;
        }

        public async Task<dynamic> GetPerfilByUserId(string userId)
        {
            try
            {
                dynamic? perfil = string.Empty;
                
                var usuario = _services.UserManager.Users.First(u => u.Id == userId);
                
                if (usuario is null)
                    throw new Exception("Usuário não encontrado. Falha ao buscar perfil do usuário.");
                
                var rolePerfil = await _services.UserManager.GetRolesAsync(usuario);

                if (rolePerfil[0].Contains("Aluno"))
                    perfil = await _repositories.Aluno.BuscarAlunoPorUserId(userId);
                else if (rolePerfil[0].Contains("Organizacao"))
                    perfil = await _repositories.Organizacao.BuscarOrganizacaoPorUserId(userId);
                else if (rolePerfil[0].Contains("Treinador"))
                    perfil = _repositories.Treinador.BuscarTreinadorPorUserId(userId);

                if (perfil is null)
                    throw new Exception("Não foi possível recuperar o perfil do usuário.");

                return perfil;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    
        public async Task<dynamic> CriarPerfilUsuario(CriarUsuarioRequest criarUsuarioRequest)
        {
            try
            {
                await _repositories.BeginTransaction();
                if (string.IsNullOrWhiteSpace(criarUsuarioRequest.Nome) || criarUsuarioRequest.Nome == string.Empty) throw new Exception("O nome do usuário é inválido.");
                if (string.IsNullOrWhiteSpace(criarUsuarioRequest.Email) || criarUsuarioRequest.Email == string.Empty) throw new Exception("O email do usuário é inválido.");
                if (string.IsNullOrWhiteSpace(criarUsuarioRequest.Senha) || criarUsuarioRequest.Senha == string.Empty) throw new Exception("A senha do usuário é inválida.");
                if (string.IsNullOrWhiteSpace(criarUsuarioRequest.Role) || criarUsuarioRequest.Role == string.Empty) throw new Exception("A role do usuário é inválida.");

                var user = new User()
                {
                    Email = criarUsuarioRequest.Email,
                    UserName = criarUsuarioRequest.NomeDeUsuario,
                    EmailConfirmed = false,
                    IsActive = true,
                };


                IdentityResult usuarioCriado = await _services.UserManager.CreateAsync(user, criarUsuarioRequest.Senha);
                if (!usuarioCriado.Succeeded) throw new Exception("Houve um erro ao criar usuário.");

                var createdUser = await _services.UserManager.FindByEmailAsync(criarUsuarioRequest.Email);

                IdentityResult assignedRoleResult = await _services.UserManager.
                    AddToRoleAsync(createdUser, criarUsuarioRequest.Role);
                if (!assignedRoleResult.Succeeded) throw new Exception("Houve um erro ao atribuir a role do usuário. Operação abortada.");

                if (criarUsuarioRequest.Role == "Aluno")
                {
                    var aluno = new Aluno()
                    {
                        Nome = criarUsuarioRequest.Nome,
                        UserId = createdUser.Id,
                        OrganizacaoId = criarUsuarioRequest.OrganizacaoPertencenteId,
                        TreinadorId = criarUsuarioRequest.TreinadorDoAlunoId,
                    };
                    var alunoCriado = _repositories.Aluno.CriarAluno(aluno);
                    if (aluno is null) throw new Exception("Houve um erro ao criar perfil do usuário. Operação abortada.");
                    await _repositories.CommitTransaction();
                    return alunoCriado;
                }
                else if(criarUsuarioRequest.Role == "Organizacao")
                {
                    var organizacao = new Organizacao()
                    {
                        NomeFantasia = criarUsuarioRequest.Nome,
                        UserId = createdUser.Id,
                    };
                    var organizacaoCriada = await _repositories.Organizacao.CriarOrganizacao(organizacao);
                    if (organizacaoCriada is null) throw new Exception("Houve um erro ao criar perfil do usuário. Operação abortada.");
                    await _repositories.CommitTransaction();
                    return organizacaoCriada;
                }
                else if (criarUsuarioRequest.Role == "Treinador")
                {
                    var treinador = new Treinador()
                    {
                        Nome = criarUsuarioRequest.Nome,
                        UserId = createdUser.Id,
                        OrganizacaoId = criarUsuarioRequest.OrganizacaoPertencenteId,
                    };
                    var treinadorCriado = await _repositories.Treinador.CriarTreinador(treinador);
                    if (treinadorCriado is null) throw new Exception("Houve um erro ao criar perfil do usuário. Operação abortada."); ;
                    await _repositories.CommitTransaction();
                    return treinadorCriado;
                }
                return null;
            }
            catch (Exception ex)
            {
                await _repositories.RollBackTransaction();
                throw new Exception(ex.Message);
            }
        }
    
    }
}
