using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TestTaskData.Models;

namespace TestTaskData.DbContexts
{
    public class AppDbContext : IdentityDbContext<Employee>
    {
        public override DbSet<Employee> Users { get; set; }

        public DbSet<Project> Projects { get; set; }

        public DbSet<WorkTime> WorkTimes { get; set; }

        public DbSet<ActivityType> ActivityTypes { get; set; }

        public DbSet<Role> EmployeeRoles { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    @"Server=(localdb)\mssqllocaldb;Database=test_task;Trusted_connection=True;");
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Employee>()
                .HasMany(e => e.Works)
                .WithOne(w => w.Employee)
                .HasForeignKey(w => w.EmployeeId);

            builder
                .Entity<Project>()
                .HasMany(p => p.Works)
                .WithOne(w => w.Project)
                .HasForeignKey(w => w.ProjectId);

            builder
                .Entity<Role>()
                .HasMany(r => r.Works)
                .WithOne(w => w.Role)
                .HasForeignKey(w => w.RoleId);

            builder
                .Entity<ActivityType>()
                .HasMany(a => a.Works)
                .WithOne(w => w.ActivityType)
                .HasForeignKey(w => w.ActivityTypeId);

            base.OnModelCreating(builder);
        }
    }
}
