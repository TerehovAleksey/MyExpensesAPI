using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyExpensesAPI.Domain;

namespace MyExpensesAPI.EfDal.Configurations
{
    public class UsersAccountConfiguration : IEntityTypeConfiguration<UsersAccount>
    {
        public void Configure(EntityTypeBuilder<UsersAccount> builder)
        {
            builder.ToTable("UsersAccounts")
                .HasKey(x => x.Id);

            builder.Property(x => x.DateOfCreation)
                .HasColumnType("datetime2(7)")
                .IsRequired()
                .HasDefaultValueSql("getdate()");

            builder.Property(x => x.DateOfChange)
                .HasColumnType("datetime2(7)");

            builder.Property(x => x.IsDeleted)
                .IsRequired()
                .HasDefaultValue(0);

            builder.Property(x => x.Balance)
              .IsRequired()
              .HasColumnType("money");
        }
    }
}
