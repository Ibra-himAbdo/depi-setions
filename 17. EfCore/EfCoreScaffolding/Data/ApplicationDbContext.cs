using System;
using System.Collections.Generic;
using EfCoreScaffolding.Models;
using Microsoft.EntityFrameworkCore;

namespace EfCoreScaffolding.Data;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<CourseInstructor> CourseInstructors { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Instructor> Instructors { get; set; }

    public virtual DbSet<InstructorSalaryLog> InstructorSalaryLogs { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<StudentCourse> StudentCourses { get; set; }

    public virtual DbSet<TestTable> TestTables { get; set; }

    public virtual DbSet<Topic> Topics { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=UniversityDB;Integrated Security=True;Connect Timeout=5;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.Cid).HasName("PK__Course__C1F8DC59595F25EC");

            entity.Property(e => e.Cid).ValueGeneratedNever();

            entity.HasOne(d => d.TidNavigation).WithMany(p => p.Courses).HasConstraintName("FK_Course_Topic");
        });

        modelBuilder.Entity<CourseInstructor>(entity =>
        {
            entity.HasOne(d => d.CidNavigation).WithMany(p => p.CourseInstructors)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Course_Instructor_Course");

            entity.HasOne(d => d.Ins).WithMany(p => p.CourseInstructors)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Course_Instructor_Instructor");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.Did).HasName("PK__Departme__C036563091002976");

            entity.Property(e => e.Did).ValueGeneratedNever();

            entity.HasOne(d => d.Ins).WithMany(p => p.Departments).HasConstraintName("FK_Department_Instructor");
        });

        modelBuilder.Entity<Instructor>(entity =>
        {
            entity.HasKey(e => e.InsId).HasName("PK__Instruct__9D104D8F772C0161");

            entity.ToTable("Instructor", tb => tb.HasTrigger("trg_AuditInstructorSalary"));

            entity.Property(e => e.InsId).ValueGeneratedNever();

            entity.HasOne(d => d.DidNavigation).WithMany(p => p.Instructors).HasConstraintName("FK_Instructor_Department");
        });

        modelBuilder.Entity<InstructorSalaryLog>(entity =>
        {
            entity.HasKey(e => e.LogId).HasName("PK__Instruct__5E5499A8DD8188EF");

            entity.Property(e => e.ModifiedDate).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.Sid).HasName("PK__Student__CA195970B456A24F");

            entity.Property(e => e.Sid).ValueGeneratedNever();
            entity.Property(e => e.Age).HasDefaultValue(18, "Df_Student_Age");

            entity.HasOne(d => d.DidNavigation).WithMany(p => p.Students).HasConstraintName("FK_Student_Department");

            entity.HasOne(d => d.Super).WithMany(p => p.InverseSuper).HasConstraintName("FK_Student_Super");
        });

        modelBuilder.Entity<StudentCourse>(entity =>
        {
            entity.HasOne(d => d.CidNavigation).WithMany(p => p.StudentCourses)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Student_Course_Course");

            entity.HasOne(d => d.SidNavigation).WithMany(p => p.StudentCourses)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Student_Course_Student");
        });

        modelBuilder.Entity<TestTable>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Test_tab__3214EC074D194A2F");
        });

        modelBuilder.Entity<Topic>(entity =>
        {
            entity.HasKey(e => e.Tid).HasName("PK__Topic__C456D729D369693F");

            entity.Property(e => e.Tid).ValueGeneratedNever();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
