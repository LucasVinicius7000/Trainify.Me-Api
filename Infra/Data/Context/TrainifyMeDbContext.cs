using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Trainify.Me_Api.Infra.Data.Context
{
    public class TrainifyMeDbContext : IdentityDbContext
    {
        public TrainifyMeDbContext(DbContextOptions<TrainifyMeDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityUser>()
                .HasKey(x => x.Id);
        }
    }
}
