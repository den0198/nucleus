using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models.EntitiesDatabase;

// ReSharper disable RedundantOverriddenMember
namespace DAL.EntityFramework
{
    public sealed class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        #region Entities
        
        public DbSet<AccountEntity> AccountEntities { get; set; }
        
        #endregion
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var applicationContextAssembly = typeof(AppDbContext).Assembly;

            modelBuilder.ApplyConfigurationsFromAssembly(applicationContextAssembly);
            
        }
    }
}