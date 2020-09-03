using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Spg.TicketShop.Services.Models
{
    public partial class TicketShopContext : DbContext
    {
        public TicketShopContext()
        {
        }

        public TicketShopContext(DbContextOptions<TicketShopContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Bookings> Bookings { get; set; }
        public virtual DbSet<CatEventStates> CatEventStates { get; set; }
        public virtual DbSet<Contingents> Contingents { get; set; }
        public virtual DbSet<Events> Events { get; set; }
        public virtual DbSet<Prices> Prices { get; set; }
        public virtual DbSet<Shows> Shows { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bookings>(entity =>
            {
                entity.HasIndex(e => e.ContingentId);

                entity.HasIndex(e => e.LaseChangeUserId);

                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.BookingId).ValueGeneratedNever();
            });

            modelBuilder.Entity<CatEventStates>(entity =>
            {
                entity.HasIndex(e => e.LaseChangeUserId);

                entity.Property(e => e.CatEventStateId).ValueGeneratedNever();
            });

            modelBuilder.Entity<Contingents>(entity =>
            {
                entity.HasIndex(e => e.LaseChangeUserId);

                entity.HasIndex(e => e.ShowId);

                entity.Property(e => e.ContingentId).ValueGeneratedNever();
            });

            modelBuilder.Entity<Events>(entity =>
            {
                entity.HasIndex(e => e.CatEventStateId);

                entity.HasIndex(e => e.LaseChangeUserId);

                entity.Property(e => e.EventId).ValueGeneratedNever();
            });

            modelBuilder.Entity<Prices>(entity =>
            {
                entity.HasIndex(e => e.ContingentId);

                entity.HasIndex(e => e.LaseChangeUserId);

                entity.Property(e => e.PriceId).ValueGeneratedNever();
            });

            modelBuilder.Entity<Shows>(entity =>
            {
                entity.HasIndex(e => e.EventId);

                entity.HasIndex(e => e.LaseChangeUserId);

                entity.Property(e => e.ShowId).ValueGeneratedNever();
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasIndex(e => e.LaseChangeUserId);

                entity.Property(e => e.UserId).ValueGeneratedNever();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
