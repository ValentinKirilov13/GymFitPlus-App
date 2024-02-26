using GymFitPlus.Infrastructure.Data.Configuration;
using GymFitPlus.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GymFitPlus.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<UserInfo> UserInfos { get; set; } = null!;
        public DbSet<Excercise> Excercises { get; set; } = null!;
        public DbSet<UserExcercise> UsersExcercises { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserExcerciseConfiguration());


            base.OnModelCreating(builder);
        }
    }
}
