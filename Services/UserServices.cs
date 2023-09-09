using Trainify.Me_Api.Infra.Data.Repositories;

namespace Trainify.Me_Api.Services
{
    public class UserServices
    {
        private readonly IService _services;
        private readonly IRepository _repositories;


        public UserServices(IService services, IRepository repositories) 
        { 
            _repositories= repositories;
            _services = services;
        }



    }
}
