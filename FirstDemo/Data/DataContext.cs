using FirstDemo.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FirstDemo.Data
{
    public class DataContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Student> students { get; set; }
        public DbSet<Department> departments { get; set; }
        public DbSet<Course> courses { get; set; }
        public DbSet<StudentCourse> studentCourses { get; set; }
        public DbSet<Instructor> instructors { get; set; }
        public DbSet<Exame> exames { get; set; }    
        public DbSet<Question> questions { get; set; }
        public DbSet<Option> options { get; set; }

        public DataContext()
        {
            
        }
        public DataContext(DbContextOptions option): base(option) 
        {
            
        }
      
       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Student>()
                .HasOne(s => s.Department)
                .WithMany(d => d.Students)
                .HasForeignKey(s => s.DeptNumber);

            modelBuilder.Entity<Instructor>()
                .HasOne(i => i.Department)
                .WithMany(d => d.Instructors)
                .HasForeignKey(i => i.DeptNo);

            modelBuilder.Entity<Department>()
                .HasMany(a => a.Courses)
                .WithMany(a => a.Departments);

            modelBuilder.Entity<StudentCourse>()
                .HasKey(a => new { a.StuId, a.CrsId });

            modelBuilder.Entity<Exame>()
                .HasOne(e => e.Course)
                .WithMany(c => c.Exames)
                .HasForeignKey(f => f.CrsId);

            modelBuilder.Entity<Question>()
                .HasOne(c => c.Exame)
                .WithMany(e => e.Questions)
                .HasForeignKey(f => f.ExameId);

            modelBuilder.Entity<Option>()
                .HasOne(o => o.Question)
                .WithMany(c => c.Options)
                .HasForeignKey(f => f.QuestionId);
          
        }
    }
}
