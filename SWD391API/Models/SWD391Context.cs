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

        public virtual DbSet<Author> Author { get; set; }
        public virtual DbSet<Campaign> Campaign { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<DonateDetail> DonateDetail { get; set; }
        public virtual DbSet<GiftDetail> GiftDetail { get; set; }
        public virtual DbSet<LikeDetail> LikeDetail { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserRole> UserRole { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=SWD391;Trusted_Connection=True; User Id=sa; Password=123456");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RoleId)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Campaign>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

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

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.Campaign)
                    .HasForeignKey(d => d.AuthorId)
                    .HasConstraintName("FK_Campaign_Author");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Campaign)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_Campaign_Category");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<DonateDetail>(entity =>
            {
                entity.HasIndex(e => e.GiftId)
                    .HasName("IX_DonateDetail")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.Description)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Campaign)
                    .WithMany(p => p.DonateDetail)
                    .HasForeignKey(d => d.CampaignId)
                    .HasConstraintName("FK_DonateDetail_Campaign");

                entity.HasOne(d => d.Gift)
                    .WithOne(p => p.DonateDetail)
                    .HasForeignKey<DonateDetail>(d => d.GiftId)
                    .HasConstraintName("FK_DonateDetail_GiftDetail");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.DonateDetail)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_DonateDetail_DonateDetail");
            });

            modelBuilder.Entity<GiftDetail>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.GiftName)
                    .HasMaxLength(300)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<LikeDetail>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.Campaign)
                    .WithMany(p => p.LikeDetail)
                    .HasForeignKey(d => d.CampaignId)
                    .HasConstraintName("FK_LikeDetail_LikeDetail");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.LikeDetail)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_LikeDetail_User");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

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

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_UserRole");
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.RoleName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
