using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.EntitiesDatabase;

namespace DAL.Configurations
{
    public class UserConfigurations : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.ToTable("UsersDetails");
        }
    }
}