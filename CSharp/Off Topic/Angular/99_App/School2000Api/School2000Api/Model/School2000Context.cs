using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace School2000Api.Model
{
    public partial class School2000Context : DbContext
    {
        public School2000Context()
        { }

        public School2000Context(DbContextOptions<School2000Context> options)
            : base(options)
        { }

        public virtual DbSet<gegenstaende> gegenstaende { get; set; }
        public virtual DbSet<klassen> klassen { get; set; }
        public virtual DbSet<lehrer> lehrer { get; set; }
        public virtual DbSet<pruefungen> pruefungen { get; set; }
        public virtual DbSet<raeume> raeume { get; set; }
        public virtual DbSet<schueler> schueler { get; set; }
        public virtual DbSet<stunden> stunden { get; set; }
        public virtual DbSet<vorgesetzte> vorgesetzte { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<gegenstaende>(entity =>
            {
                entity.Property(e => e.G_ID).HasComment("schulübliche ABkürzung des Gegenstands");

                entity.Property(e => e.G_Bez).HasComment("Name Gegenstand");
            });

            modelBuilder.Entity<klassen>(entity =>
            {
                entity.Property(e => e.K_ID).HasComment("kurze schulübliche Klassenbezeichnung");

                entity.Property(e => e.K_Bez).HasComment("Klassenbezeichnung lang");

                entity.Property(e => e.K_L_Klavst).HasComment("FK Lehrer, der Klassenvorstand ist");

                entity.Property(e => e.K_S_Klaspr).HasComment("FK Schueler, der Klassensprecher ist");

                entity.Property(e => e.K_S_Klasprstv).HasComment("FK Schueler, der Klassensprecherstellvertreter ist");

                entity.HasOne(d => d.K_L_KlavstNavigation)
                    .WithMany(p => p.klassen)
                    .HasForeignKey(d => d.K_L_Klavst)
                    .HasConstraintName("FK_klassen_lehrer");

                entity.HasOne(d => d.K_S_KlasprNavigation)
                    .WithMany(p => p.klassenK_S_KlasprNavigation)
                    .HasForeignKey(d => d.K_S_Klaspr)
                    .HasConstraintName("FK_klassen_schueler");

                entity.HasOne(d => d.K_S_KlasprstvNavigation)
                    .WithMany(p => p.klassenK_S_KlasprstvNavigation)
                    .HasForeignKey(d => d.K_S_Klasprstv)
                    .HasConstraintName("FK_klassen_schueler1");
            });

            modelBuilder.Entity<lehrer>(entity =>
            {
                entity.HasIndex(e => e.L_L_Chef)
                    .HasName("lehrer_L_L_Chef");

                entity.Property(e => e.L_ID).HasComment("Lehrernummer = in Schule übliche Kürzel");

                entity.Property(e => e.L_Gebdat).HasComment("Geburtsdatum des Lehrers");

                entity.Property(e => e.L_Gehalt).HasComment("Gehalt (fiktiv) des Lehrers");

                entity.Property(e => e.L_L_Chef).HasComment("FK anderer Lehrer, welcher der dienstrechtliche Vorgesetzte ist");

                entity.Property(e => e.L_Name).HasComment("Zuname des Lehrers");

                entity.Property(e => e.L_Vorname).HasComment("Vorname des Lehrers");

                entity.HasOne(d => d.L_L_ChefNavigation)
                    .WithMany(p => p.InverseL_L_ChefNavigation)
                    .HasForeignKey(d => d.L_L_Chef)
                    .HasConstraintName("FK_lehrer_lehrer");
            });

            modelBuilder.Entity<pruefungen>(entity =>
            {
                entity.HasKey(e => new { e.P_Datum, e.P_S_Kandidat, e.P_L_Pruefer, e.P_G_Fach });

                entity.Property(e => e.P_Datum).HasComment("Datum der Prüfung");

                entity.Property(e => e.P_S_Kandidat).HasComment("FK Schueler, der geprüft wurde");

                entity.Property(e => e.P_L_Pruefer).HasComment("FK Lehrer, der geprüft hat");

                entity.Property(e => e.P_G_Fach).HasComment("FK Fach, das geprüft wurde");

                entity.Property(e => e.P_Art).HasComment("Art der Prüfung (M,s)");

                entity.Property(e => e.P_Note).HasComment("Note (1-5) die vergeben wurde");

                entity.HasOne(d => d.P_G_FachNavigation)
                    .WithMany(p => p.pruefungen)
                    .HasForeignKey(d => d.P_G_Fach)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_pruefungen_gegenstaende");

                entity.HasOne(d => d.P_L_PrueferNavigation)
                    .WithMany(p => p.pruefungen)
                    .HasForeignKey(d => d.P_L_Pruefer)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_pruefungen_lehrer");

                entity.HasOne(d => d.P_S_KandidatNavigation)
                    .WithMany(p => p.pruefungen)
                    .HasForeignKey(d => d.P_S_Kandidat)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_pruefungen_schueler");
            });

            modelBuilder.Entity<raeume>(entity =>
            {
                entity.Property(e => e.R_ID).HasComment("Raumkurzbez. wie sie am Eingang steht");

                entity.Property(e => e.R_Plaetze).HasComment("Anzahl der Plätze im Raum");
            });

            modelBuilder.Entity<schueler>(entity =>
            {
                entity.Property(e => e.S_SCHNR).HasComment("Schülernummer, beliebige nicht klassifizierende Nummer");

                entity.Property(e => e.S_Adresse).HasComment("Anschrift des Schülers (vereinfacht in einem Feld)");

                entity.Property(e => e.S_Gebdat).HasComment("Geburtsdatum des Schülers");

                entity.Property(e => e.S_K_Klasse).HasComment("FK Klasse, die der Schüler derzeit besucht");

                entity.Property(e => e.S_Name).HasComment("Zuname des Schülers");

                entity.Property(e => e.S_Vorname).HasComment("Vorname des Schülers");

                entity.HasOne(d => d.S_K_KlasseNavigation)
                    .WithMany(p => p.schueler)
                    .HasForeignKey(d => d.S_K_Klasse)
                    .HasConstraintName("FK_schueler_klassen");
            });

            modelBuilder.Entity<stunden>(entity =>
            {
                entity.HasKey(e => new { e.ST_K_Klasse, e.ST_Stunde });

                entity.Property(e => e.ST_K_Klasse).HasComment("FK Klasse für die der Stundenplaneintrag gilt");

                entity.Property(e => e.ST_Stunde).HasComment("Wochenstunde (codiert   MO1, MO2, DI4, DO2, ...)");

                entity.Property(e => e.ST_G_Fach).HasComment("FK Gegenstand, der unterrichtet wird");

                entity.Property(e => e.ST_L_Lehrer).HasComment("FK Lehrer, der unterrichtet");

                entity.Property(e => e.ST_R_Raum).HasComment("FK Raum, in dem unterrichte wird");

                entity.HasOne(d => d.ST_G_FachNavigation)
                    .WithMany(p => p.stunden)
                    .HasForeignKey(d => d.ST_G_Fach)
                    .HasConstraintName("FK_stunden_gegenstaende");

                entity.HasOne(d => d.ST_K_KlasseNavigation)
                    .WithMany(p => p.stunden)
                    .HasForeignKey(d => d.ST_K_Klasse)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_stunden_klassen");

                entity.HasOne(d => d.ST_L_LehrerNavigation)
                    .WithMany(p => p.stunden)
                    .HasForeignKey(d => d.ST_L_Lehrer)
                    .HasConstraintName("FK_stunden_lehrer");

                entity.HasOne(d => d.ST_R_RaumNavigation)
                    .WithMany(p => p.stunden)
                    .HasForeignKey(d => d.ST_R_Raum)
                    .HasConstraintName("FK_stunden_raeume");
            });

            modelBuilder.Entity<vorgesetzte>(entity =>
            {
                entity.HasKey(e => new { e.V_L_Vorg, e.V_Art, e.V_L_Unt });

                entity.HasComment("Vorgesetzte bildet beliebige Vorgesetztenbeziehungen zwishcen Lehrer(-datensätzen) ab");

                entity.Property(e => e.V_L_Vorg).HasComment("FK Lehrernummer des jeweiligen vorgesetzten Lehrers");

                entity.Property(e => e.V_Art).HasComment("Art der Vorgesetztenbeziehung");

                entity.Property(e => e.V_L_Unt).HasComment("FK Lehrernummer des jeweils untergebenen Lehrers");

                entity.HasOne(d => d.V_L_UntNavigation)
                    .WithMany(p => p.vorgesetzteV_L_UntNavigation)
                    .HasForeignKey(d => d.V_L_Unt)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_vorgesetzte_lehrer1");

                entity.HasOne(d => d.V_L_VorgNavigation)
                    .WithMany(p => p.vorgesetzteV_L_VorgNavigation)
                    .HasForeignKey(d => d.V_L_Vorg)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_vorgesetzte_lehrer");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
