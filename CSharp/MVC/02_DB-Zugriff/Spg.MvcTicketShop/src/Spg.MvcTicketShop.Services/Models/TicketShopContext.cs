using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Spg.MvcTicketShop.Services.Models
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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=.\\SQLEXPRESS;Initial Catalog=TicketShop;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bookings>(entity =>
            {
                entity.Property(e => e.BookingId).ValueGeneratedNever();
            });

            modelBuilder.Entity<CatEventStates>(entity =>
            {
                entity.Property(e => e.CatEventStateId).ValueGeneratedNever();
            });

            modelBuilder.Entity<Contingents>(entity =>
            {
                entity.Property(e => e.ContingentId).ValueGeneratedNever();
            });

            modelBuilder.Entity<Events>(entity =>
            {
                entity.Property(e => e.EventId).ValueGeneratedNever();
            });

            modelBuilder.Entity<Prices>(entity =>
            {
                entity.Property(e => e.PriceId).ValueGeneratedNever();
            });

            modelBuilder.Entity<Shows>(entity =>
            {
                entity.Property(e => e.ShowId).ValueGeneratedNever();
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.Property(e => e.UserId).ValueGeneratedNever();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
