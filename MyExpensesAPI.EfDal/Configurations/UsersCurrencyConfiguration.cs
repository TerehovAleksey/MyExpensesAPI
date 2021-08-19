using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyExpensesAPI.Domain;

namespace MyExpensesAPI.EfDal.Configurations
{
    public class UsersCurrencyConfiguration : IEntityTypeConfiguration<UsersCurrency>
    {
        public void Configure(EntityTypeBuilder<UsersCurrency> builder)
        {
            builder.ToTable("UsersCurrencies")
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

            builder.Property(x => x.Rate)
              .IsRequired()
              .HasColumnType("money");

            builder.Property(x => x.IsDefault)
                .IsRequired()
                .HasDefaultValue(0);
        }
    }
}
