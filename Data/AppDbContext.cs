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

        public DbSet<User> users { get; set; }
        public DbSet<Role> roles { get; set; }
        public DbSet<role_user> role_users { get; set; }
        public DbSet<EducationalLevel> educational_levels { get; set; }
        public DbSet<Models.Program> programs { get; set; }
        public DbSet<AcademicPeriod> academic_periods { get; set; }
        public DbSet<Student> students { get; set; }
        public DbSet<Teacher> teachers { get; set; }
        public DbSet<Class> classes { get; set; }
        public DbSet<Enrollment> enrollments { get; set; }
        public DbSet<Assignment> assignments { get; set; }
        public DbSet<Submission> submissions { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // =========================
            // USER
            // =========================
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.id);

                entity.HasIndex(e => e.email)
                      .IsUnique();

                entity.Property(e => e.email).IsRequired().HasMaxLength(255);
                entity.Property(e => e.password_hash).IsRequired().HasMaxLength(255);
                entity.Property(e => e.first_name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.last_name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.is_active).HasDefaultValue(true);
                entity.Property(e => e.created_at).HasDefaultValueSql("CURRENT_TIMESTAMP");
            });

            // =========================
            // ROLE
            // =========================
            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.id);

                entity.HasIndex(e => e.name)
                      .IsUnique();

                entity.Property(e => e.name)
                      .IsRequired()
                      .HasMaxLength(50);
            });

            // =========================
            // ROLE_USER (N:N)
            // =========================
            modelBuilder.Entity<role_user>(entity =>
            {
                entity.HasKey(e => new { e.user_id, e.role_id });

                entity.HasOne(e => e.user)
                      .WithMany(u => u.role_users)
                      .HasForeignKey(e => e.user_id)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.role)
                      .WithMany(r => r.role_users)
                      .HasForeignKey(e => e.role_id)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // =========================
            // EDUCATIONAL LEVEL
            // =========================
            modelBuilder.Entity<EducationalLevel>(entity =>
            {
                entity.HasKey(e => e.id);

                entity.HasIndex(e => e.name)
                      .IsUnique();

                entity.Property(e => e.name)
                      .IsRequired()
                      .HasMaxLength(100);
            });

            // =========================
            // PROGRAM
            // =========================
            modelBuilder.Entity<Models.Program>(entity =>
            {
                entity.HasKey(e => e.id);

                entity.Property(e => e.name)
                      .IsRequired()
                      .HasMaxLength(150);

                entity.HasOne(e => e.educational_level)
                      .WithMany(l => l.programs)
                      .HasForeignKey(e => e.level_id)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // =========================
            // ACADEMIC PERIOD
            // =========================
            modelBuilder.Entity<AcademicPeriod>(entity =>
            {
                entity.HasKey(e => e.id);

                entity.Property(e => e.name)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(e => e.start_date).IsRequired();
                entity.Property(e => e.end_date).IsRequired();
                entity.Property(e => e.is_active).HasDefaultValue(false);
            });

            // =========================
            // STUDENT
            // =========================
            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(e => e.id);

                entity.HasIndex(e => e.user_id).IsUnique();
                entity.HasIndex(e => e.enrollment_number).IsUnique();

                entity.Property(e => e.enrollment_number)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.Property(e => e.birth_date).IsRequired();

                entity.HasOne(e => e.user)
                      .WithOne(u => u.student)
                      .HasForeignKey<Student>(e => e.user_id)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.program)
                      .WithMany(p => p.students)
                      .HasForeignKey(e => e.program_id)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // =========================
            // TEACHER
            // =========================
            modelBuilder.Entity<Teacher>(entity =>
            {
                entity.HasKey(e => e.id);

                entity.HasIndex(e => e.user_id).IsUnique();
                entity.HasIndex(e => e.employee_number).IsUnique();

                entity.Property(e => e.employee_number)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.HasOne(e => e.user)
                      .WithOne(u => u.teacher)
                      .HasForeignKey<Teacher>(e => e.user_id)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // =========================
            // CLASS
            // =========================
            modelBuilder.Entity<Class>(entity =>
            {
                entity.HasKey(e => e.id);

                entity.Property(e => e.name)
                      .IsRequired()
                      .HasMaxLength(150);

                entity.Property(e => e.schedule_json)
                      .HasColumnType("json");

                entity.HasOne(e => e.academic_period)
                      .WithMany(a => a.classes)
                      .HasForeignKey(e => e.period_id)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.program)
                      .WithMany(p => p.classes)
                      .HasForeignKey(e => e.program_id)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.teacher)
                      .WithMany(t => t.classes)
                      .HasForeignKey(e => e.teacher_id)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // =========================
            // ENROLLMENT (N:N)
            // =========================
            modelBuilder.Entity<Enrollment>(entity =>
            {
                entity.HasKey(e => new { e.student_id, e.class_id });

                entity.Property(e => e.enrolled_at)
                      .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(e => e.student)
                      .WithMany(s => s.enrollments)
                      .HasForeignKey(e => e.student_id)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e._class)
                      .WithMany(c => c.enrollments)
                      .HasForeignKey(e => e.class_id)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // =========================
            // ASSIGNMENT
            // =========================
            modelBuilder.Entity<Assignment>(entity =>
            {
                entity.HasKey(e => e.id);

                entity.Property(e => e.name)
                      .IsRequired()
                      .HasMaxLength(200);

                entity.Property(e => e.points).HasDefaultValue(0);

                entity.HasOne(e => e._class)
                      .WithMany(c => c.assignments)
                      .HasForeignKey(e => e.class_id)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // =========================
            // SUBMISSION
            // =========================
            modelBuilder.Entity<Submission>(entity =>
            {
                entity.HasKey(e => e.id);

                entity.Property(e => e.submitted_at)
                      .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.file_url).HasMaxLength(500);

                entity.HasOne(e => e.assignment)
                      .WithMany(a => a.submissions)
                      .HasForeignKey(e => e.assignment_id)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.student)
                      .WithMany(s => s.submissions)
                      .HasForeignKey(e => e.student_id)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
