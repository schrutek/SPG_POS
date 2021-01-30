using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Spg.MvcTestsAdmin.Service.Models;

namespace Spg.MvcTestsAdmin.Service.Infrastructure
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

        public virtual DbSet<CatAccountState> CatAccountState { get; set; }
        public virtual DbSet<CatTestState> CatTestState { get; set; }
        public virtual DbSet<Lesson> Lesson { get; set; }
        public virtual DbSet<Period> Period { get; set; }
        public virtual DbSet<Pupil> Pupil { get; set; }
        public virtual DbSet<Schoolclass> Schoolclass { get; set; }
        public virtual DbSet<Teacher> Teacher { get; set; }
        public virtual DbSet<Test> Test { get; set; }

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
                entity.HasIndex(e => e.L_Class)
                    .HasName("SchoolclassLesson");

                entity.HasIndex(e => e.L_Hour)
                    .HasName("PeriodLesson");

                entity.HasIndex(e => e.L_Teacher)
                    .HasName("TeacherLesson");

                entity.HasIndex(e => e.L_Untis_ID)
                    .HasName("idx_L_Untis_ID");

                entity.Property(e => e.L_Class).IsUnicode(false);

                entity.Property(e => e.L_Day).HasDefaultValueSql("((0))");

                entity.Property(e => e.L_Hour).HasDefaultValueSql("((0))");

                entity.Property(e => e.L_Room).IsUnicode(false);

                entity.Property(e => e.L_Subject).IsUnicode(false);

                entity.Property(e => e.L_Teacher).IsUnicode(false);

                entity.Property(e => e.L_Untis_ID).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.L_ClassNavigation)
                    .WithMany(p => p.Lesson)
                    .HasForeignKey(d => d.L_Class)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.L_TeacherNavigation)
                    .WithMany(p => p.Lesson)
                    .HasForeignKey(d => d.L_Teacher)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Pupil>(entity =>
            {
                entity.HasIndex(e => e.P_Account)
                    .HasName("idx_P_Account")
                    .IsUnique();

                entity.HasIndex(e => e.P_Class)
                    .HasName("SchoolclassPupil");

                entity.Property(e => e.P_Account).IsUnicode(false);

                entity.Property(e => e.P_Class).IsUnicode(false);

                entity.Property(e => e.P_Firstname).IsUnicode(false);

                entity.Property(e => e.P_Lastname).IsUnicode(false);

                entity.HasOne(d => d.P_CatAccountState)
                    .WithMany(p => p.Pupil)
                    .HasForeignKey(d => d.P_CatAccountStateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Pupil_CatAccountState");

                entity.HasOne(d => d.P_ClassNavigation)
                    .WithMany(p => p.Pupil)
                    .HasForeignKey(d => d.P_Class)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Schoolclass>(entity =>
            {
                entity.HasIndex(e => e.C_ClassTeacher)
                    .HasName("TeacherSchoolclass");

                entity.Property(e => e.C_ID).IsUnicode(false);

                entity.Property(e => e.C_ClassTeacher).IsUnicode(false);

                entity.Property(e => e.C_Department).IsUnicode(false);
            });

            modelBuilder.Entity<Teacher>(entity =>
            {
                entity.HasIndex(e => e.T_Account)
                    .HasName("idx_T_Account")
                    .IsUnique();

                entity.Property(e => e.T_ID).IsUnicode(false);

                entity.Property(e => e.T_Account).IsUnicode(false);

                entity.Property(e => e.T_Email).IsUnicode(false);

                entity.Property(e => e.T_Firstname).IsUnicode(false);

                entity.Property(e => e.T_Lastname).IsUnicode(false);

                entity.HasOne(d => d.T_CatAccountState)
                    .WithMany(p => p.Teacher)
                    .HasForeignKey(d => d.T_CatAccountStateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Teacher_CatAccountState");
            });

            modelBuilder.Entity<Test>(entity =>
            {
                entity.HasIndex(e => e.TE_Class)
                    .HasName("SchoolclassTest");

                entity.HasIndex(e => e.TE_Lesson)
                    .HasName("PeriodTest");

                entity.HasIndex(e => e.TE_Teacher)
                    .HasName("TeacherTest");

                entity.Property(e => e.TE_Class).IsUnicode(false);

                entity.Property(e => e.TE_Subject).IsUnicode(false);

                entity.Property(e => e.TE_Teacher).IsUnicode(false);

                entity.HasOne(d => d.TE_CatTestState)
                    .WithMany(p => p.Test)
                    .HasForeignKey(d => d.TE_CatTestStateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Test_CatTestState");

                entity.HasOne(d => d.TE_ClassNavigation)
                    .WithMany(p => p.Test)
                    .HasForeignKey(d => d.TE_Class)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.TE_LessonNavigation)
                    .WithMany(p => p.Test)
                    .HasForeignKey(d => d.TE_Lesson)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.TE_TeacherNavigation)
                    .WithMany(p => p.Test)
                    .HasForeignKey(d => d.TE_Teacher)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
