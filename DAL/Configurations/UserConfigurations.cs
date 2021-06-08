using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.EntitiesDatabase;

namespace DAL.Configurations
{
    public class UserConfigurations : IEntityTypeConfiguration<UserDetailsEntity>
    {
        public void Configure(EntityTypeBuilder<UserDetailsEntity> builder)
        {
        }
    }
}