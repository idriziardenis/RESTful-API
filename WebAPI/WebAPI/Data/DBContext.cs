using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebAPI.Models
{
    public partial class DBContext : DbContext
    {
        public DBContext()
        {
        }

        public DBContext(DbContextOptions<DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Exam> Exams { get; set; }
        public virtual DbSet<Log> Logs { get; set; }
        public virtual DbSet<Professor> Professors { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>(entity =>
            {
                entity.Property(e => e.Comment).IsRequired();

                entity.Property(e => e.DepartmentName).IsRequired();
            });

            modelBuilder.Entity<Exam>(entity =>
            {
                entity.HasIndex(e => e.ProfessorId);

                entity.HasIndex(e => e.StudentId);

                entity.HasIndex(e => e.SubjectId);

                entity.HasOne(d => d.Professor)
                    .WithMany(p => p.Exams)
                    .HasForeignKey(d => d.ProfessorId);

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Exams)
                    .HasForeignKey(d => d.StudentId);

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.Exams)
                    .HasForeignKey(d => d.SubjectId);
            });

            modelBuilder.Entity<Log>(entity =>
            {
                entity.HasKey(e => e.LogId);
            });

            modelBuilder.Entity<Professor>(entity =>
            {
                entity.HasIndex(e => e.SubjectId);

                entity.Property(e => e.ProfessorName).IsRequired();

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.Professors)
                    .HasForeignKey(d => d.SubjectId);
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasIndex(e => e.DepartmentId);

                entity.Property(e => e.Index).IsRequired();

                entity.Property(e => e.Name).IsRequired();

                entity.Property(e => e.Surname).IsRequired();

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.DepartmentId);
            });

            modelBuilder.Entity<Subject>(entity =>
            {
                entity.HasIndex(e => e.DepartmentId);

                entity.Property(e => e.Semester).IsRequired();

                entity.Property(e => e.SubjectName).IsRequired();

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Subjects)
                    .HasForeignKey(d => d.DepartmentId);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.RoleId);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
