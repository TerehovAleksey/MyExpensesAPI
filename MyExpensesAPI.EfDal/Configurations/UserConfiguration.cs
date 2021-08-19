using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyExpensesAPI.Domain;

namespace MyExpensesAPI.EfDal.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users")
                .HasKey(x => x.Id);

            builder.Property(x => x.UserName)
                .HasMaxLength(50);

            builder.Property(x => x.NormalizedUserName)
                .HasMaxLength(50);

            builder.Property(x => x.FirstName)
                .HasMaxLength(30);

            builder.Property(x => x.LastName)
                .HasMaxLength(30);

            builder.Property(x => x.Email)
                .HasMaxLength(50);

            builder.Property(x => x.NormalizedEmail)
                .HasMaxLength(50);

            builder.Property(x => x.PhoneNumber)
                .HasMaxLength(50);

            builder.Property(x => x.PasswordHash)
                .HasMaxLength(1024);

            builder.Property(x => x.SecurityStamp)
                .HasMaxLength(1024);

            builder.Property(x => x.ConcurrencyStamp)
                .HasMaxLength(1024);
        }
    }
}
