﻿using Microsoft.AspNetCore.Identity;
using Trainify.Me_Api.Infra.Data.Repositories;
using Trainify.Me_Api.Domain.Entities;

namespace Trainify.Me_Api.Services
{
    public class Service : IService
    {

        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<User> _signInManager;

        public IConfiguration Configuration => _configuration;
        public UserManager<User> UserManager => _userManager;
        public RoleManager<IdentityRole> RoleManager => _roleManager;
        public SignInManager<User> SignInManager => _signInManager;
        public TokenService TokenService { get; private set; }
        public PerfilService PerfilService { get; private set; }
        public CursoService CursoService { get; private set; }
        public AulaService AulaService { get; private set; }

        public Service
        (
            IServiceProvider serviceProvider,
            IConfiguration configuration,
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<User> signInManager,
            IRepository repositories
        )
        {
            this._configuration = configuration;
            this._userManager = userManager;
            this._roleManager = roleManager;
            this._signInManager = signInManager;
            this.TokenService = TokenService ?? new TokenService(this, configuration);
            this.PerfilService = PerfilService ?? new PerfilService(this, repositories);
            this.CursoService = CursoService ?? new CursoService(this, repositories);
            this.AulaService = AulaService ?? new AulaService(this, repositories, serviceProvider);
        }
    }
}
