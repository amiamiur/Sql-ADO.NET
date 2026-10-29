using AcademyApp.Models;
using Microsoft.EntityFrameworkCore;

namespace AcademyApp.data
{
    public class CollegeDbContext : DbContext
    {
        public CollegeDbContext(DbContextOptions<CollegeDbContext> options) : base(options) {}
        public DbSet<Student> Students { get; set; }
        public DbSet<Group> Groups { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Group>(entity =>
            {
 
                entity.HasKey(g => g.GroupId);

                entity.Property(g => g.GroupName)
                      .IsRequired()         
                      .HasMaxLength(10);   
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(s => s.StudentId);

                entity.Property(s => s.FirstName)
                      .IsRequired()
                      .HasMaxLength(15);

                entity.Property(s => s.LastName)
                      .IsRequired()
                      .HasMaxLength(15);

                entity.Property(s => s.Age)
                      .IsRequired();

                entity.HasOne(s => s.Group)
                      .WithMany(g => g.Students)
                      .HasForeignKey(s => s.GroupId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}