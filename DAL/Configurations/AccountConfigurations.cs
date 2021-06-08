using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.EntitiesDatabase;

namespace DAL.Configurations
{
    public class AccountConfigurations : IEntityTypeConfiguration<AccountEntity>
    {
        public void Configure(EntityTypeBuilder<AccountEntity> builder)
        {
            builder
                .HasOne(b => b.UserDetails)
                .WithOne(i => i.Account)
                .HasForeignKey<UserDetailsEntity>(b=> b.AccountId);
        }
    }
}