using Microsoft.EntityFrameworkCore;
using SIADAL.Models;

namespace SIADAL.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        #region DbSets

        public DbSet<user> users { get; set; }
        public DbSet<student> students { get; set; }
        public DbSet<teacher> teachers { get; set; }
        public DbSet<role> roles { get; set; }
        public DbSet<role_user> role_users { get; set; }
        public DbSet<course> courses { get; set; }
        public DbSet<_class> classes { get; set; }
        public DbSet<academic_term> academic_terms { get; set; }
        public DbSet<activity> activities { get; set; }
        public DbSet<group> groups { get; set; }
        public DbSet<group_class> group_classes { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // =========================
            // USER
            // =========================
            modelBuilder.Entity<user>(entity =>
            {
                entity.HasKey(e => e.id);

                entity.HasIndex(e => e.email)
                      .IsUnique();

                entity.Property(e => e.email).IsRequired().HasMaxLength(255);
                entity.Property(e => e.password).IsRequired();
                entity.Property(e => e.first_name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.last_name).IsRequired().HasMaxLength(100);
            });

            // =========================
            // STUDENT
            // =========================
            modelBuilder.Entity<student>(entity =>
            {
                entity.HasKey(e => e.id);

                entity.HasOne(e => e.user)
                      .WithMany(u => u.students)
                      .HasForeignKey(e => e.user_id)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // =========================
            // TEACHER
            // =========================
            modelBuilder.Entity<teacher>(entity =>
            {
                entity.HasKey(e => e.id);

                entity.HasOne(e => e.user)
                      .WithMany(u => u.teachers)
                      .HasForeignKey(e => e.user_id)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // =========================
            // ROLE
            // =========================
            modelBuilder.Entity<role>(entity =>
            {
                entity.HasKey(e => e.id);

                entity.HasIndex(e => e.name)
                      .IsUnique();

                entity.Property(e => e.name)
                      .IsRequired()
                      .HasMaxLength(100);
            });

            // =========================
            // ROLE_USER (N:N)
            // =========================
            modelBuilder.Entity<role_user>(entity =>
            {
                entity.HasKey(e => new { e.user_id, e.role_id });

                entity.HasOne(e => e.user)
                      .WithMany()
                      .HasForeignKey(e => e.user_id);

                entity.HasOne(e => e.role)
                      .WithMany()
                      .HasForeignKey(e => e.role_id);
            });

            // =========================
            // COURSE
            // =========================
            modelBuilder.Entity<course>(entity =>
            {
                entity.HasKey(e => e.id);

                entity.Property(e => e.name).IsRequired();
                entity.Property(e => e.code).IsRequired();
            });

            // =========================
            // ACADEMIC TERM
            // =========================
            modelBuilder.Entity<academic_term>(entity =>
            {
                entity.HasKey(e => e.id);

                entity.Property(e => e.name).IsRequired();

                entity.HasMany(e => e._classes)
                      .WithOne(c => c.academic_term)
                      .HasForeignKey(c => c.academic_term_id);

                entity.HasMany(e => e.groups)
                      .WithOne(g => g.academic_term)
                      .HasForeignKey(g => g.academic_term_id);
            });

            // =========================
            // CLASS
            // =========================
            modelBuilder.Entity<_class>(entity =>
            {
                entity.HasKey(e => e.id);

                entity.HasOne(e => e.course)
                      .WithMany(c => c._classes)
                      .HasForeignKey(e => e.course_id);

                entity.HasOne(e => e.teacher)
                      .WithMany(t => t._classes)
                      .HasForeignKey(e => e.teacher_id);

                entity.HasOne(e => e.academic_term)
                      .WithMany(a => a._classes)
                      .HasForeignKey(e => e.academic_term_id);
            });

            // =========================
            // ACTIVITY
            // =========================
            modelBuilder.Entity<activity>(entity =>
            {
                entity.HasKey(e => e.id);

                entity.HasOne(e => e._class)
                      .WithMany(c => c.activities)
                      .HasForeignKey(e => e.class_id);

                entity.HasOne(e => e.student)
                      .WithMany(s => s.activities)
                      .HasForeignKey(e => e.student_id);
            });

            // =========================
            // GROUP
            // =========================
            modelBuilder.Entity<group>(entity =>
            {
                entity.HasKey(e => e.id);

                entity.HasOne(e => e.academic_term)
                      .WithMany(a => a.groups)
                      .HasForeignKey(e => e.academic_term_id);
            });

            // =========================
            // GROUP_CLASS
            // =========================
            modelBuilder.Entity<group_class>(entity =>
            {
                entity.HasKey(e => e.id);

                entity.HasOne(e => e.group)
                      .WithMany(g => g.group_classes)
                      .HasForeignKey(e => e.group_id);

                entity.HasOne(e => e._class)
                      .WithMany(c => c.group_classes)
                      .HasForeignKey(e => e.class_id);
            });
        }
    }
}
