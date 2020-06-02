using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SWD391API.Models
{
    public partial class SWD391Context : DbContext
    {
        public SWD391Context()
        {
        }

        public SWD391Context(DbContextOptions<SWD391Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Campaigns> Campaigns { get; set; }
        public virtual DbSet<Carelesses> Carelesses { get; set; }
        public virtual DbSet<Categories> Categories { get; set; }
        public virtual DbSet<DonateDetails> DonateDetails { get; set; }
        public virtual DbSet<GiftDetails> GiftDetails { get; set; }
        public virtual DbSet<LoginMethods> LoginMethods { get; set; }
        public virtual DbSet<UserRoles> UserRoles { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=SWD391;Trusted_Connection=True;User Id = sa; Password=123456");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Campaigns>(entity =>
            {
                entity.HasKey(e => e.CampaignId)
                    .HasName("PK_Campaign");

                entity.Property(e => e.CampaignId).ValueGeneratedNever();

                entity.Property(e => e.CampaignName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Campaigns)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_Campaign_Category");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Campaigns)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Campaign_User");
            });

            modelBuilder.Entity<Carelesses>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Campaign)
                    .WithMany(p => p.Carelesses)
                    .HasForeignKey(d => d.CampaignId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Careless_Campaign");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Carelesses)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Careless_User");
            });

            modelBuilder.Entity<Categories>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<DonateDetails>(entity =>
            {
                entity.HasIndex(e => e.GiftId)
                    .HasName("IX_DonateDetail")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.Description)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Campaign)
                    .WithMany(p => p.DonateDetails)
                    .HasForeignKey(d => d.CampaignId)
                    .HasConstraintName("FK_DonateDetail_Campaign");

                entity.HasOne(d => d.Gift)
                    .WithOne(p => p.DonateDetails)
                    .HasForeignKey<DonateDetails>(d => d.GiftId)
                    .HasConstraintName("FK_DonateDetail_GiftDetail");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.DonateDetails)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_DonateDetail_User");
            });

            modelBuilder.Entity<GiftDetails>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.GiftName)
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.HasOne(d => d.Campaign)
                    .WithMany(p => p.GiftDetails)
                    .HasForeignKey(d => d.CampaignId)
                    .HasConstraintName("FK_GiftDetail_Campaign");
            });

            modelBuilder.Entity<LoginMethods>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UserRoles>(entity =>
            {
                entity.HasKey(e => e.RoleId)
                    .HasName("PK_UserRole");

                entity.Property(e => e.RoleId).ValueGeneratedNever();

                entity.Property(e => e.RoleName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK_User");

                entity.Property(e => e.UserId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.LoginMethod)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.LoginMethodId)
                    .HasConstraintName("FK_User_LoginMethod");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_UserRole");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
