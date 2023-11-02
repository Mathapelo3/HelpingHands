using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using HelpingHands.Models;

namespace HelpingHands.Data
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AspNetRole> AspNetRoles { get; set; } = null!;
        public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; } = null!;
        public virtual DbSet<User> AspNetUsers { get; set; } = null!;
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; } = null!;
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; } = null!;
        public virtual DbSet<AspNetUserRole> AspNetUserRoles { get; set; } = null!;
        public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; } = null!;
        public virtual DbSet<CareContract> CareContracts { get; set; } = null!;
        public virtual DbSet<CareVisit> CareVisits { get; set; } = null!;
        public virtual DbSet<ChronicCondition> ChronicConditions { get; set; } = null!;
        public virtual DbSet<City> Cities { get; set; } = null!;
        public virtual DbSet<ContactUsVM> ContactUs { get; set; } = null!;
        public virtual DbSet<ContractStatus> ContractStatuses { get; set; } = null!;
        public virtual DbSet<Nurse> Nurses { get; set; } = null!;
        public virtual DbSet<Patient> Patients { get; set; } = null!;
        public virtual DbSet<PatientCondition> PatientConditions { get; set; } = null!;
        public virtual DbSet<PreferredSuburb> PreferredSuburbs { get; set; } = null!;
        public virtual DbSet<Status> Statuses { get; set; } = null!;
        public virtual DbSet<Suburb> Suburbs { get; set; } = null!;
        public virtual DbSet<ThusanagGallery> ThusanagGalleries { get; set; } = null!;
        public virtual DbSet<Thusanang> Thusanangs { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRole>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");
            });

            modelBuilder.Entity<AspNetUserLogin>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });
            });

            modelBuilder.Entity<AspNetUserRole>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });
            });

            modelBuilder.Entity<AspNetUserToken>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });
            });

            modelBuilder.Entity<CareContract>(entity =>
            {
                entity.Property(e => e.ContractNo).HasComputedColumnSql("('CON'+right('00000'+CONVERT([varchar](5),[ContractId]),(5)))", true);

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.CareContracts)
                    .HasForeignKey(d => d.PatientId)
                    .HasConstraintName("FK_CareContracts_Patients");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.CareContracts)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CareContracts_ContractStatus");

                entity.HasOne(d => d.Suburb)
                    .WithMany(p => p.CareContracts)
                    .HasForeignKey(d => d.SuburbId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CareContracts_Suburbs");
            });

            modelBuilder.Entity<CareVisit>(entity =>
            {
                entity.HasOne(d => d.Contract)
                    .WithMany(p => p.CareVisits)
                    .HasForeignKey(d => d.ContractId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CareVisit_CareContracts");

                entity.HasOne(d => d.Nurse)
                    .WithMany(p => p.CareVisits)
                    .HasForeignKey(d => d.NurseId)
                    .HasConstraintName("FK_CareVisit_Nurses");
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.Property(e => e.Abbreviation).IsFixedLength();
            });

            modelBuilder.Entity<ContractStatus>(entity =>
            {
                entity.Property(e => e.Status).IsFixedLength();
            });

            modelBuilder.Entity<Nurse>(entity =>
            {
                entity.Property(e => e.Gender).IsFixedLength();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Nurses)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Nurses_AspNetUsers");
            });

            modelBuilder.Entity<Patient>(entity =>
            {
                entity.Property(e => e.Gender).IsFixedLength();

                entity.HasOne(d => d.Suburb)
                    .WithMany(p => p.Patients)
                    .HasForeignKey(d => d.SuburbId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Patients_Suburbs");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Patients)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Patients_AspNetUsers");
            });

            modelBuilder.Entity<PatientCondition>(entity =>
            {
                entity.HasOne(d => d.Condition)
                    .WithMany()
                    .HasForeignKey(d => d.ConditionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PatientConditions_ChronicConditions");

                entity.HasOne(d => d.Patient)
                    .WithMany()
                    .HasForeignKey(d => d.PatientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PatientConditions_Patients");
            });

            modelBuilder.Entity<PreferredSuburb>(entity =>
            {
                entity.HasOne(d => d.Nurse)
                    .WithMany()
                    .HasForeignKey(d => d.NurseId)
                    .HasConstraintName("FK_PreferredSuburb_Nurses");

                entity.HasOne(d => d.Suburb)
                    .WithMany()
                    .HasForeignKey(d => d.SuburbId)
                    .HasConstraintName("FK_PreferredSuburb_Suburbs");
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.Property(e => e.Status1).IsFixedLength();
            });

            modelBuilder.Entity<Suburb>(entity =>
            {
                entity.HasOne(d => d.City)
                    .WithMany(p => p.Suburbs)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Suburbs_Cities");
            });

            modelBuilder.Entity<ThusanagGallery>(entity =>
            {
                entity.HasKey(e => e.ImageId)
                    .HasName("PK_ThusanagGalary");
            });

            modelBuilder.Entity<Thusanang>(entity =>
            {
                entity.HasKey(e => e.OrganizationId)
                    .HasName("PK_Organization Tbl");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
