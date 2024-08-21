using Microsoft.EntityFrameworkCore;
using WebApp.API.Models;



namespace WebApp.API.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options):base(options)
        {
             
        }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Designation> Designations { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeDetail> EmployeeDetails { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Department - Designation relationship
            modelBuilder.Entity<Designation>()
                .HasOne(d => d.Department)
                .WithMany(dept => dept.Designations)
                .HasForeignKey(d => d.DepartmentId);

            // Set precision for Salary
            modelBuilder.Entity<Designation>()
                .Property(d => d.Salary)
                .HasPrecision(18, 2);

            // EmployeeDetail - Employee relationship
            modelBuilder.Entity<EmployeeDetail>()
                .HasOne(ed => ed.Employee)
                .WithMany(e => e.EmployeeDetails)
                .HasForeignKey(ed => ed.EmployeeId);

            // EmployeeDetail - Department relationship
            modelBuilder.Entity<EmployeeDetail>()
                .HasOne(ed => ed.Department)
                .WithMany(dept => dept.EmployeeDetails)
                .HasForeignKey(ed => ed.DepartmentId);

            // EmployeeDetail - Designation relationship
            modelBuilder.Entity<EmployeeDetail>()
                .HasOne(ed => ed.Designation)
                .WithMany(d => d.EmployeeDetails)
                .HasForeignKey(ed => ed.DesignationId);



            base.OnModelCreating(modelBuilder);
        }



    }
}
