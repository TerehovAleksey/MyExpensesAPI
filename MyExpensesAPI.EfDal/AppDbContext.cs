using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyExpensesAPI.Domain;
using MyExpensesAPI.EfDal.Configurations;
using System;

namespace MyExpensesAPI.EfDal
{
    public class AppDbContext : IdentityDbContext<User, Role, Guid>
    {
        public virtual DbSet<AccountType> AccountTypes { get; set; }
        public virtual DbSet<CurrencyType> CurrencyTypes { get; set; }
        public virtual DbSet<ExpenceCategory> ExpenceCategories { get; set; }
        public virtual DbSet<ExpenceJournal> ExpenceJournals { get; set; }
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
    }
}
