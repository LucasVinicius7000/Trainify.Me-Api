using Microsoft.AspNetCore.Identity;
namespace Trainify.Me_Api.Domain.Entities
{
    public class User : IdentityUser
    {
        public bool IsActive { get; set; }
        public string RefreshToken { get; set; }
        public string Expires { get; set; }
    }
}
