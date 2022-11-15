using Microsoft.EntityFrameworkCore;
using Attribute = FutsalManager.Models.Attribute;

namespace FutsalManager.Data
{
    public partial class DataContext : DbContext
    {
        public DataContext()
        {
        }

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Attribute> Attributes { get; set; } = null!;
        public virtual DbSet<Player> Players { get; set; } = null!;
        public virtual DbSet<Position> Positions { get; set; } = null!;
        public virtual DbSet<Team> Teams { get; set; } = null!;
        public virtual DbSet<Transfer> Transfers { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.EnableSensitiveDataLogging();
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-QC0KBSP;Initial Catalog=futsalmanager;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Attribute>(entity =>
            {
                entity.Property(e => e.Dribbling).HasDefaultValueSql("((50))");

                entity.Property(e => e.Juggling).HasDefaultValueSql("((50))");

                entity.Property(e => e.Passing).HasDefaultValueSql("((50))");

                entity.Property(e => e.Positioning).HasDefaultValueSql("((50))");

                entity.Property(e => e.Reaction).HasDefaultValueSql("((50))");

                entity.Property(e => e.Shooting).HasDefaultValueSql("((50))");

                entity.Property(e => e.Tackling).HasDefaultValueSql("((50))");

                entity.Property(e => e.Vision).HasDefaultValueSql("((50))");
            });

            modelBuilder.Entity<Player>(entity =>
            {
                entity.Property(e => e.Birthday).HasColumnType("date");

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(50);
                
                entity.Property(e => e.Nationality).HasMaxLength(50);

                entity.HasOne(d => d.Attribute)
                    .WithMany(p => p.Players)
                    .HasForeignKey(d => d.AttributeId)
                    .HasConstraintName("FK_Players_AttributeId_Attributes_Id");

                entity.HasOne(d => d.Position)
                    .WithMany(p => p.Players)
                    .HasForeignKey(d => d.PositionId)
                    .HasConstraintName("FK_Players_PositionId_Positions_Id");

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.Players)
                    .HasForeignKey(d => d.TeamId)
                    .HasConstraintName("FK_Players_TeamId_Teams_Id");
            });

            modelBuilder.Entity<Position>(entity =>
            {
                entity.Property(e => e.Pos).HasMaxLength(50);
            });

            modelBuilder.Entity<Team>(entity =>
            {
                entity.Property(e => e.City).HasMaxLength(50);

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(50);
                
                entity.Property(e => e.PhoneNumber).HasMaxLength(50);
                
            });

            modelBuilder.Entity<Transfer>(entity =>
            {
                entity.Property(e => e.History).HasColumnType("datetime");

                entity.HasOne(d => d.Player)
                    .WithMany(p => p.Transfers)
                    .HasForeignKey(d => d.PlayerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Transfers_PlayerId_Players_Id");

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.Transfers)
                    .HasForeignKey(d => d.TeamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Transfers_TeamId_Teams_Id");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
