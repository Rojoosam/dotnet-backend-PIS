using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SIADAL.Models;

namespace SIADAL.Data;

public partial class siadalContext : DbContext
{
    public siadalContext(DbContextOptions<siadalContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Class> classes { get; set; }

    public virtual DbSet<Academic_term> academic_terms { get; set; }

    public virtual DbSet<Activity> activities { get; set; }

    public virtual DbSet<Course> courses { get; set; }

    public virtual DbSet<Group> groups { get; set; }

    public virtual DbSet<Group_class> group_classes { get; set; }

    public virtual DbSet<Role> roles { get; set; }

    public virtual DbSet<role_user> role_users { get; set; }

    public virtual DbSet<Student> students { get; set; }

    public virtual DbSet<Teacher> teachers { get; set; }

    public virtual DbSet<User> users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Class>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity.UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.academic_term_id, "classes_academic_term_id_foreign");

            entity.HasIndex(e => e.course_id, "classes_course_id_foreign");

            entity.HasIndex(e => e.teacher_id, "classes_teacher_id_foreign");

            entity.Property(e => e.created_at).HasColumnType("timestamp");
            entity.Property(e => e.room).HasMaxLength(255);
            entity.Property(e => e.schedule).UseCollation("utf8mb4_bin");
            entity.Property(e => e.updated_at).HasColumnType("timestamp");

            entity.HasOne(d => d.academic_term).WithMany(p => p._classes)
                .HasForeignKey(d => d.academic_term_id)
                .HasConstraintName("classes_academic_term_id_foreign");

            entity.HasOne(d => d.course).WithMany(p => p._classes)
                .HasForeignKey(d => d.course_id)
                .HasConstraintName("classes_course_id_foreign");

            entity.HasOne(d => d.teacher).WithMany(p => p._classes)
                .HasForeignKey(d => d.teacher_id)
                .HasConstraintName("classes_teacher_id_foreign");
        });

        modelBuilder.Entity<Academic_term>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity.UseCollation("utf8mb4_unicode_ci");

            entity.Property(e => e.created_at).HasColumnType("timestamp");
            entity.Property(e => e.is_active)
                .IsRequired()
                .HasDefaultValueSql("'1'");
            entity.Property(e => e.name).HasMaxLength(255);
            entity.Property(e => e.updated_at).HasColumnType("timestamp");
        });

        modelBuilder.Entity<Activity>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity.UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.class_id, "activities_class_id_foreign");

            entity.HasIndex(e => e.student_id, "activities_student_id_foreign");

            entity.Property(e => e.created_at).HasColumnType("timestamp");
            entity.Property(e => e.grade).HasPrecision(8, 2);
            entity.Property(e => e.name).HasMaxLength(255);
            entity.Property(e => e.porcentage).HasPrecision(8, 2);
            entity.Property(e => e.status).HasMaxLength(255);
            entity.Property(e => e.updated_at).HasColumnType("timestamp");

            entity.HasOne(d => d._class).WithMany(p => p.activities)
                .HasForeignKey(d => d.class_id)
                .HasConstraintName("activities_class_id_foreign");

            entity.HasOne(d => d.student).WithMany(p => p.activities)
                .HasForeignKey(d => d.student_id)
                .HasConstraintName("activities_student_id_foreign");
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity.UseCollation("utf8mb4_unicode_ci");

            entity.Property(e => e.code).HasMaxLength(255);
            entity.Property(e => e.created_at).HasColumnType("timestamp");
            entity.Property(e => e.desciption).HasMaxLength(255);
            entity.Property(e => e.name).HasMaxLength(255);
            entity.Property(e => e.updated_at).HasColumnType("timestamp");
        });

        modelBuilder.Entity<Group>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity.UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.academic_term_id, "groups_academic_term_id_foreign");

            entity.Property(e => e.created_at).HasColumnType("timestamp");
            entity.Property(e => e.name).HasMaxLength(255);
            entity.Property(e => e.updated_at).HasColumnType("timestamp");

            entity.HasOne(d => d.academic_term).WithMany(p => p.groups)
                .HasForeignKey(d => d.academic_term_id)
                .HasConstraintName("groups_academic_term_id_foreign");
        });

        modelBuilder.Entity<Group_class>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity
                .ToTable("group_class")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.class_id, "group_class_class_id_foreign");

            entity.HasIndex(e => e.group_id, "group_class_group_id_foreign");

            entity.Property(e => e.created_at).HasColumnType("timestamp");
            entity.Property(e => e.updated_at).HasColumnType("timestamp");

            entity.HasOne(d => d._class).WithMany(p => p.group_classes)
                .HasForeignKey(d => d.class_id)
                .HasConstraintName("group_class_class_id_foreign");

            entity.HasOne(d => d.group).WithMany(p => p.group_classes)
                .HasForeignKey(d => d.group_id)
                .HasConstraintName("group_class_group_id_foreign");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity.UseCollation("utf8mb4_unicode_ci");

            entity.Property(e => e.created_at).HasColumnType("timestamp");
            entity.Property(e => e.name).HasMaxLength(255);
            entity.Property(e => e.updated_at).HasColumnType("timestamp");
        });

        modelBuilder.Entity<role_user>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("role_user")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.role_id, "role_user_role_id_foreign");

            entity.HasIndex(e => e.user_id, "role_user_user_id_foreign");

            entity.HasOne(d => d.role).WithMany()
                .HasForeignKey(d => d.role_id)
                .HasConstraintName("role_user_role_id_foreign");

            entity.HasOne(d => d.user).WithMany()
                .HasForeignKey(d => d.user_id)
                .HasConstraintName("role_user_user_id_foreign");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity.UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.enrollment_number, "students_enrollment_number_unique").IsUnique();

            entity.HasIndex(e => e.user_id, "students_user_id_foreign");

            entity.Property(e => e.created_at).HasColumnType("timestamp");
            entity.Property(e => e.updated_at).HasColumnType("timestamp");

            entity.HasOne(d => d.user).WithMany(p => p.students)
                .HasForeignKey(d => d.user_id)
                .HasConstraintName("students_user_id_foreign");
        });

        modelBuilder.Entity<Teacher>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity.UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.employee_number, "teachers_employee_number_unique").IsUnique();

            entity.HasIndex(e => e.user_id, "teachers_user_id_foreign");

            entity.Property(e => e.created_at).HasColumnType("timestamp");
            entity.Property(e => e.updated_at).HasColumnType("timestamp");

            entity.HasOne(d => d.user).WithMany(p => p.teachers)
                .HasForeignKey(d => d.user_id)
                .HasConstraintName("teachers_user_id_foreign");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity.UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.email, "users_email_unique").IsUnique();

            entity.Property(e => e.created_at).HasColumnType("timestamp");
            entity.Property(e => e.email_verified_at).HasColumnType("timestamp");
            entity.Property(e => e.first_name).HasMaxLength(255);
            entity.Property(e => e.is_active)
                .IsRequired()
                .HasDefaultValueSql("'1'");
            entity.Property(e => e.last_name).HasMaxLength(255);
            entity.Property(e => e.password).HasMaxLength(255);
            entity.Property(e => e.updated_at).HasColumnType("timestamp");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
