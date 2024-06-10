using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CityProjects.Core;
using System.Diagnostics;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CityProjects.Data.SqlServerEF
{
    public partial class DataContext : IdentityDbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DataContext()
        {
        }

        public virtual DbSet<CityUserRole> CityUserRoles { get; set; }

        public virtual DbSet<Donations> Donations { get; set; }

        public virtual DbSet<Mandates> Mandates { get; set; }

        public virtual DbSet<Materials> Materials { get; set; }

        public virtual DbSet<Projects> Projects { get; set; }

        public virtual DbSet<Region> Regions { get; set; }

        public virtual DbSet<Transportations> Transportations { get; set; }

        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.;Database=RegionProjectsAPIApp2;User Id=sa;Password=sa123456;Encrypt=True;TrustServerCertificate=True;Trusted_Connection=True;");
            // Enable lazy loading
            optionsBuilder.UseLazyLoadingProxies();
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<CityUserRole>(entity =>
            {
                entity.HasKey(e => e.CityId);

                entity.Property(e => e.CityId).HasColumnName("CityID");
                entity.Property(e => e.RoleName)
                    .HasMaxLength(20)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Donations>(entity =>
            {
                entity.Property(e => e.DonationId).HasColumnName("DonationID");
                entity.Property(e => e.Amount).HasColumnType("decimal(18, 0)");
                entity.Property(e => e.MemberId).HasColumnName("MemberID");
                entity.Property(e => e.ProjectId).HasColumnName("ProjectID");

                entity.HasOne(d => d.Member).WithMany(p => p.Donations)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Donations_Users");

                entity.HasOne(d => d.Project).WithMany(p => p.Donations)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Donations_Projects");
            });

            modelBuilder.Entity<Mandates>(entity =>
            {
                entity.HasIndex(e => e.PresidentId, "uq_Mandate").IsUnique();

                entity.Property(e => e.MandateId).HasColumnName("MandateID");
                entity.Property(e => e.PresidentId).HasColumnName("PresidentID");

                entity.HasOne(d => d.President).WithOne(p => p.Mandate)
                    .HasForeignKey<Mandates>(d => d.PresidentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Mondate_User");
            });

            modelBuilder.Entity<Materials>(entity =>
            {
                entity.Property(e => e.MaterialId).HasColumnName("MaterialID");
                entity.Property(e => e.Cost).HasColumnType("decimal(18, 0)");
                entity.Property(e => e.Description)
                    .HasMaxLength(50)
                    .IsFixedLength();
                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Projects>(entity =>
            {
                entity.Property(e => e.ProjectId).HasColumnName("ProjectID");
                entity.Property(e => e.Budget).HasColumnType("decimal(18, 0)");
                entity.Property(e => e.Description)
                    .HasMaxLength(50)
                    .IsFixedLength();
                entity.Property(e => e.Location)
                    .HasMaxLength(50)
                    .IsFixedLength();
                entity.Property(e => e.MaterialProjectID).HasColumnName("MaterialProjectID");
                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsFixedLength();
                entity.Property(e => e.ProjectManagerId).HasColumnName("ProjectManagerID");
                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.HasOne(d => d.Material).WithMany(p => p.Projects)
                    .HasForeignKey(d => d.MaterialProjectID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Projects_Materials");

                entity.HasMany(d => d.Transportations).WithMany(p => p.Projects)
                    .UsingEntity<Dictionary<string, object>>(
                        "ProjectTransportation",
                        r => r.HasOne<Transportations>().WithMany()
                            .HasForeignKey("TransportationId")
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("FK__ProjectTr__Trans__52593CB8"),
                        l => l.HasOne<Projects>().WithMany()
                            .HasForeignKey("ProjectId")
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("FK__ProjectTr__Proje__5165187F"),
                        j =>
                        {
                            j.HasKey("ProjectId", "TransportationId").HasName("PK__ProjectT__0E64F94526E836A4");
                            j.ToTable("ProjectTransportation");
                            j.IndexerProperty<int>("ProjectId").HasColumnName("ProjectID");
                            j.IndexerProperty<int>("TransportationId").HasColumnName("TransportationID");
                        });
            });

            modelBuilder.Entity<Region>(entity =>
            {
                entity.Property(e => e.RegionId).HasColumnName("RegionID");
                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Transportations>(entity =>
            {
                entity.Property(e => e.TransportationId).HasColumnName("TransportationID");
                entity.Property(e => e.Description)
                    .HasMaxLength(50)
                    .IsFixedLength();
                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.Property(e => e.UserId).HasColumnName("UserID");
                entity.Property(e => e.Adresse)
                    .HasMaxLength(100)
                    .IsFixedLength();
                entity.Property(e => e.AuthenticationUserId)
                    .HasMaxLength(50)
                    .IsFixedLength()
                    .HasColumnName("AuthenticationUserID");
                entity.Property(e => e.CityUserRoleId).HasColumnName("CityUserRoleID");
                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsFixedLength();
                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsFixedLength();
                entity.Property(e => e.Phone)
                    .HasMaxLength(8)
                    .IsFixedLength();
                entity.Property(e => e.RegionId).HasColumnName("RegionID");

                entity.HasOne(d => d.CityUserRole).WithMany(p => p.Users)
                    .HasForeignKey(d => d.CityUserRoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Users_CityUserRoles");

                entity.HasOne(d => d.Region).WithMany(p => p.Users)
                    .HasForeignKey(d => d.RegionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Users_Regions");

                // Prevent AuthenticationUserId from being changed after inserting
                modelBuilder.Entity<Users>()
                    .Property(u => u.AuthenticationUserId)
                    .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Throw);

                // Configure IdentityUserLogin
                modelBuilder.Entity<IdentityUserLogin<string>>(b =>
                {
                    b.HasKey(l => new { l.LoginProvider, l.ProviderKey }); // Clé primaire composite
                    b.ToTable("UserLogins"); // Optionnel : personnaliser le nom de la table
                });

            });

            OnModelCreatingPartial(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
      
    }
}
