using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Trainify.Me_Api.Domain.Entities;

namespace Trainify.Me_Api.Infra.Data.Context
{
    public class TrainifyMeDbContext : IdentityDbContext<User>
    {
        public TrainifyMeDbContext(DbContextOptions<TrainifyMeDbContext> options) : base(options) { }

        public DbSet<Organizacao> Organizacoes
        {
            get { return Set<Organizacao>(); }
        }

        public DbSet<Treinador> Treinadores
        {
            get { return Set<Treinador>(); }
        }

        public DbSet<Aluno> Alunos
        {
            get { return Set<Aluno>(); }
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            #region Normalizando nome de tabelas do Identity

            builder.Entity<User>()
                .ToTable("Users");

            builder.Entity<IdentityRole>()
                .ToTable("Roles");

            builder.Entity<IdentityRoleClaim<string>>()
                .ToTable("RoleClaims");

            builder.Entity<IdentityUserClaim<string>>()
                .ToTable("UserClaims");

            builder.Entity<IdentityUserLogin<string>>()
                .ToTable("UserLogins");

            builder.Entity<IdentityUserRole<string>>()
                .ToTable("UserRoles");

            builder.Entity<IdentityUserToken<string>>()
               .ToTable("UserTokens");


            #endregion


            #region Organização

            builder.Entity<Organizacao>()
                .HasOne(o => o.User);

            builder.Entity<Organizacao>()
                .HasMany(o => o.Treinadores)
                .WithOne(t => t.Organizacao)
                .HasForeignKey(t => t.OrganizacaoId);

            builder.Entity<Organizacao>()
                .HasMany(o => o.Alunos)
                .WithOne(a => a.Organizacao)
                .HasForeignKey(a => a.OrganizacaoId);

            #endregion


            #region Treinador

            builder.Entity<Treinador>()
                .HasMany(t => t.Alunos)
                .WithOne(a => a.Treinador)
                .HasForeignKey(a => a.TreinadorId);

            #endregion

            #region Curso

            

            #endregion

        }
    }
}
