using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyExpensesAPI.Domain;
using MyExpensesAPI.Domain.Interfaces;
using MyExpensesAPI.EfDal.Configurations;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MyExpensesAPI.EfDal
{
    public class AppDbContext : IdentityDbContext<User, Role, Guid>
    {
        public virtual DbSet<AccountType> AccountTypes { get; set; }
        public virtual DbSet<CurrencyType> CurrencyTypes { get; set; }
        public virtual DbSet<ExpenseCategory> ExpenseCategories { get; set; }
        public virtual DbSet<ExpenseJournal> ExpenseJournals { get; set; }
        public virtual DbSet<IncomeCategory> IncomeCategories { get; set; }
        public virtual DbSet<IncomeJournal> IncomeJournals { get; set; }
        public virtual DbSet<UsersAccount> UsersAccounts { get; set; }
        public virtual DbSet<UsersCurrency> UsersCurrencies { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            #region Identity

            builder.Entity<IdentityUserClaim<Guid>>(x => x.ToTable("UserClaims"));
            builder.Entity<IdentityUserLogin<Guid>>(x => x.ToTable("UserLogins"));
            builder.Entity<IdentityUserToken<Guid>>(x => x.ToTable("UserTokens"));
            builder.Entity<IdentityUserRole<Guid>>(x => x.ToTable("UserRoles"));
            builder.Entity<IdentityRoleClaim<Guid>>(x => x.ToTable("RoleClaims"));

            #endregion Identity
            #region Configurations

            builder.ApplyConfiguration(new AccountTypeConfiguration());
            builder.ApplyConfiguration(new CurrencyTypeConfiguration());
            builder.ApplyConfiguration(new ExpenseCategoryConfiguration());
            builder.ApplyConfiguration(new ExpenseJournalConfiguration());
            builder.ApplyConfiguration(new IncomeCategoryConfiguration());
            builder.ApplyConfiguration(new IncomeJournalConfiguration());
            builder.ApplyConfiguration(new RoleConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new UsersAccountConfiguration());
            builder.ApplyConfiguration(new UsersCurrencyConfiguration());

            #endregion Configurations
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            FakeDelete();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void FakeDelete()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is IFakeDelete fakeDelete && entry.State == EntityState.Deleted)
                {
                    fakeDelete.IsDeleted = true;
                    entry.State = EntityState.Modified;
                }
            }
        }
    }
}
