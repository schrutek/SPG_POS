using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace ConsoleApp1.Models
{
    public partial class TestsAdministratorContext : DbContext
    {
        public TestsAdministratorContext()
        {
        }

        public TestsAdministratorContext(DbContextOptions<TestsAdministratorContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CatAccountState> CatAccountStates { get; set; }
        public virtual DbSet<CatTestState> CatTestStates { get; set; }
        public virtual DbSet<Lesson> Lessons { get; set; }
        public virtual DbSet<Period> Periods { get; set; }
        public virtual DbSet<Pupil> Pupils { get; set; }
        public virtual DbSet<Schoolclass> Schoolclasses { get; set; }
        public virtual DbSet<Teacher> Teachers { get; set; }
        public virtual DbSet<Test> Tests { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=WE20W8WS2000501\\SQLEXPRESS;Initial Catalog=TestsAdministrator;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CatAccountState>(entity =>
            {
                entity.Property(e => e.CatAccountStateId).ValueGeneratedNever();
            });

            modelBuilder.Entity<CatTestState>(entity =>
            {
                entity.Property(e => e.CatTestStateId).ValueGeneratedNever();
            });

            modelBuilder.Entity<Lesson>(entity =>
            {
                entity.Property(e => e.L_Class).IsUnicode(false);

                entity.Property(e => e.L_Day).HasDefaultValueSql("((0))");

                entity.Property(e => e.L_Hour).HasDefaultValueSql("((0))");

                entity.Property(e => e.L_Room).IsUnicode(false);

                entity.Property(e => e.L_Subject).IsUnicode(false);

                entity.Property(e => e.L_Teacher).IsUnicode(false);

                entity.Property(e => e.L_Untis_ID).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.L_ClassNavigation)
                    .WithMany(p => p.Lessons)
                    .HasForeignKey(d => d.L_Class)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.L_TeacherNavigation)
                    .WithMany(p => p.Lessons)
                    .HasForeignKey(d => d.L_Teacher)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Pupil>(entity =>
            {
                entity.Property(e => e.P_Account).IsUnicode(false);

                entity.Property(e => e.P_Class).IsUnicode(false);

                entity.Property(e => e.P_Firstname).IsUnicode(false);

                entity.Property(e => e.P_Lastname).IsUnicode(false);

                entity.HasOne(d => d.P_CatAccountState)
                    .WithMany(p => p.Pupils)
                    .HasForeignKey(d => d.P_CatAccountStateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Pupil_CatAccountState");

                entity.HasOne(d => d.P_ClassNavigation)
                    .WithMany(p => p.Pupils)
                    .HasForeignKey(d => d.P_Class)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Schoolclass>(entity =>
            {
                entity.Property(e => e.C_ID).IsUnicode(false);

                entity.Property(e => e.C_ClassTeacher).IsUnicode(false);

                entity.Property(e => e.C_Department).IsUnicode(false);
            });

            modelBuilder.Entity<Teacher>(entity =>
            {
                entity.Property(e => e.T_ID).IsUnicode(false);

                entity.Property(e => e.T_Account).IsUnicode(false);

                entity.Property(e => e.T_Email).IsUnicode(false);

                entity.Property(e => e.T_Firstname).IsUnicode(false);

                entity.Property(e => e.T_Lastname).IsUnicode(false);

                entity.HasOne(d => d.T_CatAccountState)
                    .WithMany(p => p.Teachers)
                    .HasForeignKey(d => d.T_CatAccountStateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Teacher_CatAccountState");
            });

            modelBuilder.Entity<Test>(entity =>
            {
                entity.Property(e => e.TE_Class).IsUnicode(false);

                entity.Property(e => e.TE_Subject).IsUnicode(false);

                entity.Property(e => e.TE_Teacher).IsUnicode(false);

                entity.HasOne(d => d.TE_CatTestState)
                    .WithMany(p => p.Tests)
                    .HasForeignKey(d => d.TE_CatTestStateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Test_CatTestState");

                entity.HasOne(d => d.TE_ClassNavigation)
                    .WithMany(p => p.Tests)
                    .HasForeignKey(d => d.TE_Class)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.TE_LessonNavigation)
                    .WithMany(p => p.Tests)
                    .HasForeignKey(d => d.TE_Lesson)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.TE_TeacherNavigation)
                    .WithMany(p => p.Tests)
                    .HasForeignKey(d => d.TE_Teacher)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
