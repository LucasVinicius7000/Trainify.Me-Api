using Trainify.Me_Api.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Trainify.Me_Api.Infra.Data.Repositories;
using Trainify.Me_Api.Infra.Services.BlobStorage;
using Trainify.Me_Api.Services;
using Trainify.Me_Api.Domain.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder()
 .SetBasePath(builder.Environment.ContentRootPath)
 .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
 .Build();


builder.Services.AddControllers();

builder.Services.AddControllers().AddNewtonsoftJson(
   options =>
   {
       options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
   }
);

builder.Services.AddDbContext<TrainifyMeDbContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("TrainifyMeDb"))
);

builder.Services.AddScoped<IService, Service>();

builder.Services.AddScoped<IRepository, Repository>();

builder.Services.AddScoped<IBlobStorageService, BlobStorageService>();

builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.User.RequireUniqueEmail = true;
    options.Password.RequireNonAlphanumeric = false;
})
.AddEntityFrameworkStores<TrainifyMeDbContext> ()
.AddDefaultTokenProviders();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder =>
        {
            builder.AllowAnyOrigin();
            builder.AllowAnyHeader();
            builder.AllowAnyMethod();
        });
});

var key = Encoding.ASCII.GetBytes(builder.Configuration["Secrets:TokenSecret"]);
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(x => {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false,
        };
    });

var app = builder.Build();


using (var serviceScope = app.Services.CreateScope())
{
    var creatingRoles = CreateRoles(serviceScope.ServiceProvider);
    Task.WaitAny(creatingRoles);
    var creatingDefaultUser = CreateDefaultAdminUser(serviceScope.ServiceProvider);
    Task.WaitAny(creatingDefaultUser);
}


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();










async Task CreateRoles(IServiceProvider serviceProvider)
{
    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    string[] basicRoles = { "Admin", "Organizacao", "Treinador", "Aluno", };

    foreach (var role in basicRoles)
    {
        var exists = await roleManager.RoleExistsAsync(role);
        if (!exists)
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }
}

async Task CreateDefaultAdminUser(IServiceProvider serviceProvider)
{

    var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var configuration = serviceProvider.GetRequiredService<IConfiguration>();
    var logger = serviceProvider.GetRequiredService<ILogger<Program>>();

    var adminUser = new User()
    {
        Email = "lucascambraia@unipam.edu.br",
        UserName = "Admin",
        EmailConfirmed = true,
        IsActive = true,
    };

    var existUser = await userManager.FindByEmailAsync(adminUser.Email);

    if (existUser == null)
    {
        try
        {
            IdentityResult userToCreateResult = await userManager.
                CreateAsync(adminUser, configuration["Secrets:DefaultAdmin"]);

            var createdUser = await userManager.FindByEmailAsync(adminUser.Email);

            IdentityResult assignedRoleResult = new IdentityResult();
            if (createdUser != null)
            {
                assignedRoleResult = await userManager.
                    AddToRoleAsync(createdUser, "Admin");
            }

            if (userToCreateResult.Succeeded && assignedRoleResult.Succeeded)
            {
                logger.
                    LogInformation("O usu�rio administrador padr�o: "
                    + adminUser.UserName + " foi criado com sucesso.");

            }

        }
        catch (Exception ex)
        {
            logger.LogError("Falha ao criar usu�rio administrador padr�o: "
                    + adminUser.UserName + ". Erro: " + ex.Message);
        }

    }
    else
    {
        logger.LogInformation("J� existe um usu�rio administrador padr�o cadastrado.");
    }


}
    