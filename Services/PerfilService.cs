using Trainify.Me_Api.Infra.Data.Repositories;

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
    }
}
