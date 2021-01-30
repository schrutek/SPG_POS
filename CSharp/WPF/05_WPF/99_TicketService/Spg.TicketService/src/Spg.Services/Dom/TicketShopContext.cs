using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Spg.Services.Dom
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
        public virtual DbSet<Contingents> Contingents { get; set; }
        public virtual DbSet<Events> Events { get; set; }
        public virtual DbSet<Prices> Prices { get; set; }
        public virtual DbSet<Shows> Shows { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=WE20W8WS2000501\\SQLEXPRESS;Initial Catalog=TicketShop;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bookings>(entity =>
            {
                entity.HasIndex(x => x.ContingentId);

                entity.HasIndex(x => x.LaseChangeUserIdId);

                entity.HasIndex(x => x.UserId);

                entity.Property(e => e.BookingId).ValueGeneratedNever();
            });

            modelBuilder.Entity<Contingents>(entity =>
            {
                entity.HasIndex(x => x.LaseChangeUserIdId);

                entity.HasIndex(x => x.ShowId);

                entity.Property(e => e.ContingentId).ValueGeneratedNever();
            });

            modelBuilder.Entity<Events>(entity =>
            {
                entity.HasIndex(x => x.LaseChangeUserIdId);

                entity.Property(e => e.EventId).ValueGeneratedNever();
            });

            modelBuilder.Entity<Prices>(entity =>
            {
                entity.HasIndex(x => x.ContingentId);

                entity.HasIndex(x => x.LaseChangeUserIdId);

                entity.Property(e => e.PriceId).ValueGeneratedNever();
            });

            modelBuilder.Entity<Shows>(entity =>
            {
                entity.HasIndex(x => x.EventId);

                entity.HasIndex(x => x.LaseChangeUserIdId);

                entity.Property(e => e.ShowId).ValueGeneratedNever();
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasIndex(x => x.LaseChangeUserIdId);

                entity.Property(e => e.UserId).ValueGeneratedNever();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
