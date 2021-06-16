using Microsoft.EntityFrameworkCore;
using TimeSheets.Models;

namespace TimeSheets.Data.EntityConfiguration
{
    public class TimeSheetDbContext: DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Sheet> Sheets { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Invoice> Invoices { get; set; }

        public TimeSheetDbContext(DbContextOptions<TimeSheetDbContext> options):base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }
    }
}