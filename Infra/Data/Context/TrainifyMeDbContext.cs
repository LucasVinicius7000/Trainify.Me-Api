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

        public DbSet<Curso> Cursos
        {
            get { return Set<Curso>(); }
        }

        public DbSet<Aula> Aulas
        {
            get { return Set<Aula>(); }
        }

        public DbSet<Atividade> Atividades
        {
            get { return Set<Atividade>(); }
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
                .HasMany(o => o.Treinadores)
                .WithOne(t => t.Organizacao)
                .HasForeignKey(t => t.OrganizacaoId);

            builder.Entity<Organizacao>()
                .HasMany(o => o.Alunos)
                .WithOne(a => a.Organizacao)
                .HasForeignKey(a => a.OrganizacaoId);

            builder.Entity<Organizacao>()
                .HasOne(o => o.User)
                .WithOne()
                .HasForeignKey<Organizacao>(o => o.UserId)
                .OnDelete(DeleteBehavior.NoAction); ;

            #endregion

            #region Treinador

            builder.Entity<Treinador>()
                .HasMany(t => t.Alunos)
                .WithOne(a => a.Treinador)
                .HasForeignKey(a => a.TreinadorId);

            builder.Entity<Treinador>()
                .HasOne(t => t.User)
                .WithOne()
                .HasForeignKey<Treinador>(t => t.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            #endregion

            #region Aluno

            builder.Entity<Aluno>()
                .HasOne(a => a.User)
                .WithOne()
                .HasForeignKey<Aluno>(a => a.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            #endregion 

            #region Curso

            builder.Entity<Curso>()
                .HasOne(c => c.OrganizacaoPertencente)
                .WithMany(o => o.Cursos)
                .HasForeignKey(c => c.OrganizacaoId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Curso>()
                .HasMany(c => c.Aulas)
                .WithOne(a => a.Curso)
                .HasForeignKey(a => a.CursoId)
                .OnDelete(DeleteBehavior.NoAction);

            #endregion

            #region Aula

            builder.Entity<Aula>()
                .HasOne(al => al.Atividade)
                .WithOne(at => at.AulaPertencente)
                .HasForeignKey<Atividade>(at => at.AulaId);

            builder.Entity<Aula>()
                .HasOne(a => a.Curso)
                .WithMany(c => c.Aulas)
                .HasForeignKey(a => a.CursoId)
                .OnDelete(DeleteBehavior.Cascade);

            #endregion

            #region Atividade

            builder.Entity<Atividade>()
                .HasMany(a => a.Alternativas)
                .WithOne(alt => alt.Atividade)
                .HasForeignKey(alt => alt.AtividadeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Atividade>()
                .HasOne(a => a.AlternativaCorreta)
                .WithOne()
                .HasForeignKey<Atividade>(a => a.AlternativaCorretaId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Atividade>()
                .HasOne(a => a.AulaPertencente)
                .WithOne(al => al.Atividade)
                .HasForeignKey<Atividade>(a => a.AulaId)
                .OnDelete(DeleteBehavior.Cascade);


            #endregion

        }
    }
}
