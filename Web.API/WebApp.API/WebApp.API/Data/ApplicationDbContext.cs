using Microsoft.EntityFrameworkCore;
using WebApp.API.Models;



namespace WebApp.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }



        public DbSet<Department> Departments { get; set; }
        public DbSet<Designation> Designations { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeDetail> EmployeeDetails { get; set; }


        //
        /// <summary>
        ///  Remove this   below Dbset  while  migration because it is developed by Database first approached  but   use entityframework  
        /// </summary>

      
        public DbSet<Message> Messages { get; set; }
        public DbSet<Connection> Connections { get; set; }
        public DbSet<Group> Groups { get; set; }



        /// <summary>
        /// this are managed by Dapper   with  databasefirst appraoched      so during  migration    Remove the Dbset
        /// </summary>
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
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








            ///
            //Remove this  below code  for  migration because  this all are done by   database first    this relation are shown  beace we are    implementatition on entityframework with database first approached
            //    relation   is  little bit complex so we   engagged in here  

            // Configure the primary key for the Groups table
            modelBuilder.Entity<Group>()
                .HasKey(g => g.Name);

            // Configure the relationships and other entities
            modelBuilder.Entity<UserRole>()
                .HasKey(ur => new { ur.UserId, ur.RoleId });

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId);

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId);

            modelBuilder.Entity<Message>()
                .HasOne(m => m.Sender)
                .WithMany(u => u.SentMessages)
                .HasForeignKey(m => m.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Message>()
                .HasOne(m => m.Recipient)
                .WithMany(u => u.ReceivedMessages)
                .HasForeignKey(m => m.RecipientId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Connection>()
                .HasOne(c => c.Group)
                .WithMany(g => g.Connections)
                .HasForeignKey(c => c.GroupName)  // Ensure the foreign key is correctly defined
                .OnDelete(DeleteBehavior.SetNull);
        }

    }
}  

