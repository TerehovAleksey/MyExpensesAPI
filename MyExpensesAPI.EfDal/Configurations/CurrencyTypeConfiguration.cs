using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyExpensesAPI.Domain;

namespace MyExpensesAPI.EfDal.Configurations
{
    public class CurrencyTypeConfiguration : IEntityTypeConfiguration<CurrencyType>
    {
        public void Configure(EntityTypeBuilder<CurrencyType> builder)
        {
            builder.ToTable("CurrencyTypes")
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

            builder.Property(x => x.Name)
              .IsRequired()
              .HasMaxLength(30);
        }
    }
}
