using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyExpensesAPI.Domain;

namespace MyExpensesAPI.EfDal.Configurations
{
    public class IncomeJournalConfiguration : IEntityTypeConfiguration<IncomeJournal>
    {
        public void Configure(EntityTypeBuilder<IncomeJournal> builder)
        {
            builder.ToTable("IncomeJournals")
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

            builder.Property(x => x.Value)
              .IsRequired()
              .HasColumnType("money");

            builder.Property(x => x.Description)
                .HasMaxLength(250);
        }
    }
}
