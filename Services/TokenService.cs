using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Trainify.Me_Api.Domain.Entities;
using Trainify.Me_Api.Domain.DTOs;

namespace Trainify.Me_Api.Services
{
    public class TokenService
    {
        private readonly IConfiguration _configuration;
        private readonly IService _services;

        public TokenService(IService services, IConfiguration configuration)
        {
            _configuration = configuration;
            _services = services;
        }

        #region private methods

        private string GenerateToken(IEnumerable<Claim> claims, DateTime? expires)
        {
            var handler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Secrets:TokenSecret"]);
                        
            SecurityTokenDescriptor descriptor = new()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = expires ?? DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var generatedToken = handler.CreateToken(descriptor);
            return handler.WriteToken(generatedToken);

        }
        private static string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
       
        private async Task SaveRefreshToken(string refreshToken, string userId)
        {
            try
            {
                if (string.IsNullOrEmpty(refreshToken))
                {
                    throw new Exception("Falha ao salvar refresh token. Refresh token gerado é inválido.");
                }

                if (string.IsNullOrEmpty(userId))
                {
                    throw new Exception("Falha ao salvar refresh token. Id do usuário é inválido.");
                }

                var usuario = _services.UserManager.Users.First(u => u.Id == userId);
                usuario.RefreshToken = refreshToken;
                var usuarioAtualizado = await _services.UserManager.UpdateAsync(usuario);

                if (!usuarioAtualizado.Succeeded)
                {
                    throw new Exception("Falha ao atualizar refresh token do usuário.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion private methods



        #region public methods

        public async Task<TokenDataDTO> GetAccessToken(string userId)
        {
            try
            {
                var usuario = _services.UserManager.Users.First(u => u.Id == userId);
                if (usuario is null) throw new Exception("Falha ao obter token de acesso. Usuário não encontrado.");

                var roles = await _services.UserManager.GetRolesAsync(usuario);
                var claims = new[]
                {
                    new Claim(ClaimTypes.Email, usuario.Email),
                    new Claim(ClaimTypes.Role, roles[0]),
                };

                var token = GenerateToken(claims, DateTime.UtcNow.AddHours(3));
                var refreshToken = GenerateRefreshToken();
                await SaveRefreshToken(refreshToken, usuario.Id);

                return new TokenDataDTO()
                {
                    Token = token,
                    RefreshToken = refreshToken
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<TokenDataDTO> RefreshToken(string oldRefreshToken)
        {
            try
            {
                var usuario = _services.UserManager.Users.First(u => u.RefreshToken == oldRefreshToken);

                if (usuario is null) throw new Exception("Falha ao obter novo token. Usuário não encontrado.");

                var roles = await _services.UserManager.GetRolesAsync(usuario);
                var tokenClaims = new[]
                {
                    new Claim(ClaimTypes.Email, usuario.Email),
                    new Claim(ClaimTypes.Role, roles.First()),
                };

                var newToken = GenerateToken(tokenClaims, DateTime.UtcNow.AddHours(3));
                if (string.IsNullOrEmpty(newToken)) throw new Exception("Falha ao gerar token.");

                var newRefreshToken = GenerateRefreshToken();
                await SaveRefreshToken(newRefreshToken, usuario.Id);

                return new TokenDataDTO()
                {
                    Token = newToken,
                    RefreshToken = newRefreshToken
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }   
        
        #endregion public methods




    }
}
