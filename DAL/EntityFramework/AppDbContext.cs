using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models.EntitiesDatabase;

// ReSharper disable RedundantOverriddenMember
namespace DAL.EntityFramework
{
    public sealed class AppDbContext : IdentityDbContext<AccountEntity>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }

        #region Entities

        private DbSet<UserEntity> UsersDetails { get; set; }

        #endregion
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var applicationContextAssembly = typeof(AppDbContext).Assembly;

            modelBuilder.ApplyConfigurationsFromAssembly(applicationContextAssembly);
            
        }
    }
}