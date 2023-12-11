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
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=MATHAPELOMMANOI;Database=GRP-04-51-Thusanang;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            {
                modelBuilder.Entity<AspNetRole>(entity =>
                {
                    entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                        .IsUnique()
                        .HasFilter("([NormalizedName] IS NOT NULL)");

                    entity.Property(e => e.Name).HasMaxLength(256);

                    entity.Property(e => e.NormalizedName).HasMaxLength(256);
                });

                modelBuilder.Entity<AspNetRoleClaim>(entity =>
                {
                    entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

                    entity.HasOne(d => d.Role)
                        .WithMany(p => p.AspNetRoleClaims)
                        .HasForeignKey(d => d.RoleId);
                });

                modelBuilder.Entity<User>(entity =>
                {
                    entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

                    entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                        .IsUnique()
                        .HasFilter("([NormalizedUserName] IS NOT NULL)");

                    entity.Property(e => e.Email).HasMaxLength(256);

                    entity.Property(e => e.FirstName).HasMaxLength(20);

                    entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                    entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                    entity.Property(e => e.Surname).HasMaxLength(30);

                    entity.Property(e => e.UserName).HasMaxLength(256);

                    entity.HasMany(d => d.Roles)
                        .WithMany(p => p.Users)
                        .UsingEntity<Dictionary<string, object>>(
                            "AspNetUserRole",
                            l => l.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                            r => r.HasOne<User>().WithMany().HasForeignKey("UserId"),
                            j =>
                            {
                                j.HasKey("UserId", "RoleId");

                                j.ToTable("AspNetUserRoles");

                                j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                            });
                });

                modelBuilder.Entity<AspNetUserClaim>(entity =>
                {
                    entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

                    entity.HasOne(d => d.User)
                        .WithMany(p => p.AspNetUserClaims)
                        .HasForeignKey(d => d.UserId);
                });

                modelBuilder.Entity<AspNetUserLogin>(entity =>
                {
                    entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                    entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

                    entity.Property(e => e.LoginProvider).HasMaxLength(128);

                    entity.Property(e => e.ProviderKey).HasMaxLength(128);

                    entity.HasOne(d => d.User)
                        .WithMany(p => p.AspNetUserLogins)
                        .HasForeignKey(d => d.UserId);
                });

                modelBuilder.Entity<AspNetUserToken>(entity =>
                {
                    entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                    entity.Property(e => e.LoginProvider).HasMaxLength(128);

                    entity.Property(e => e.Name).HasMaxLength(128);

                    entity.HasOne(d => d.User)
                        .WithMany(p => p.AspNetUserTokens)
                        .HasForeignKey(d => d.UserId);
                });

                modelBuilder.Entity<CareContract>(entity =>
                {
                    entity.HasKey(e => e.ContractId);

                    entity.Property(e => e.AddressLine1).HasMaxLength(25);

                    entity.Property(e => e.AddressLine2).HasMaxLength(25);

                    entity.Property(e => e.ContractDate).HasColumnType("date");

                    entity.Property(e => e.ContractNo)
                        .HasMaxLength(8)
                        .IsUnicode(false)
                        .HasComputedColumnSql("('CON'+right('00000'+CONVERT([varchar](5),[ContractId]),(5)))", true);

                    entity.Property(e => e.EndDate).HasColumnType("date");

                    entity.Property(e => e.Nurse)
                        .HasMaxLength(25)
                        .IsUnicode(false);

                    entity.Property(e => e.StartDate).HasColumnType("date");

                    entity.Property(e => e.WoundDescription).HasMaxLength(225);

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
                    entity.ToTable("CareVisit");

                    entity.Property(e => e.ApproxArriveDate).HasColumnName("Approx.ArriveDate");

                    entity.Property(e => e.Notes).HasMaxLength(225);

                    entity.Property(e => e.VisitDate).HasColumnType("date");

                    entity.Property(e => e.WoundCondition).HasMaxLength(225);

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

                modelBuilder.Entity<ChronicCondition>(entity =>
                {
                    entity.HasKey(e => e.ConditionId);

                    entity.Property(e => e.Name)
                        .HasMaxLength(25)
                        .IsUnicode(false);
                });

                modelBuilder.Entity<City>(entity =>
                {
                    entity.Property(e => e.Abbreviation)
                        .HasMaxLength(3)
                        .IsUnicode(false)
                        .IsFixedLength();

                    entity.Property(e => e.Name).HasMaxLength(25);
                });

                modelBuilder.Entity<ContactUsVM>(entity =>
                {
                    entity.Property(e => e.Email).HasMaxLength(40);

                    entity.Property(e => e.Message).HasMaxLength(255);

                    entity.Property(e => e.Name)
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    entity.Property(e => e.PhoneNumber).HasMaxLength(10);
                });

                modelBuilder.Entity<ContractStatus>(entity =>
                {
                    entity.ToTable("ContractStatus");

                    entity.Property(e => e.Status)
                        .HasMaxLength(1)
                        .IsUnicode(false)
                        .IsFixedLength();
                });

                modelBuilder.Entity<Nurse>(entity =>
                {
                    entity.Property(e => e.Idnumber)
                        .HasMaxLength(13)
                        .HasColumnName("IDNumber");


                    entity.Property(e => e.UserId).HasMaxLength(450);

                    entity.Property(e => e.FirstName).HasMaxLength(20);

                    entity.Property(e => e.Surname).HasMaxLength(30);

                    entity.HasOne(d => d.User)
                        .WithMany(p => p.Nurses)
                        .HasForeignKey(d => d.UserId)
                        .HasConstraintName("FK_Nurses_AspNetUsers");
                });

                modelBuilder.Entity<Patient>(entity =>
                {
                    entity.Property(e => e.AdditionalInformation).HasMaxLength(50);

                    entity.Property(e => e.DoB).HasColumnType("date");

                    entity.Property(e => e.EmergencyContact).HasMaxLength(10);

                    entity.Property(e => e.EmergencyPerson)
                        .HasMaxLength(30)
                        .IsUnicode(false);

                    entity.Property(e => e.FirstName).HasMaxLength(30);

                    entity.Property(e => e.Surname).HasMaxLength(30);

                    entity.Property(e => e.Image).HasColumnType("varbinary(max)");

                    entity.Property(e => e.UserId).HasMaxLength(450);

                    entity.HasOne(d => d.User)
                        .WithMany(p => p.Patients)
                        .HasForeignKey(d => d.UserId)
                        .HasConstraintName("FK_Patients_AspNetUsers");
                });

                modelBuilder.Entity<PatientCondition>(entity =>
                {
                    entity.HasNoKey();

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
                    entity.HasNoKey();

                    entity.ToTable("PreferredSuburb");

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
                    entity.ToTable("Status");

                    entity.Property(e => e.Status1)
                        .HasMaxLength(1)
                        .IsUnicode(false)
                        .HasColumnName("Status")
                        .IsFixedLength();
                });

                modelBuilder.Entity<Suburb>(entity =>
                {
                    entity.Property(e => e.Suburbs)
                        .HasMaxLength(25)
                        .IsUnicode(false)
                        .HasColumnName("Suburb");

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

                    entity.ToTable("ThusanagGallery");

                    entity.Property(e => e.Description).HasMaxLength(50);

                    entity.Property(e => e.Img).HasColumnType("image");
                });

                modelBuilder.Entity<Thusanang>(entity =>
                {
                    entity.HasKey(e => e.OrganizationId)
                        .HasName("PK_Organization Tbl");

                    entity.ToTable("Thusanang");

                    entity.Property(e => e.AddressLine1).HasMaxLength(25);

                    entity.Property(e => e.AddressLine2).HasMaxLength(15);

                    entity.Property(e => e.ContactNumber).HasMaxLength(13);

                    entity.Property(e => e.Email).HasMaxLength(45);

                    entity.Property(e => e.Nponumber)
                        .HasMaxLength(10)
                        .HasColumnName("NPONumber");

                    entity.Property(e => e.OperatingHours).HasMaxLength(30);

                    entity.Property(e => e.OrganizationName).HasMaxLength(30);
                });

                OnModelCreatingPartial(modelBuilder);
            }
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
