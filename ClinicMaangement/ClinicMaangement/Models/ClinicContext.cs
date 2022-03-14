using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ClinicMaangement.Models
{
    public partial class ClinicContext : DbContext
    {
        public ClinicContext()
        {
        }

        public ClinicContext(DbContextOptions<ClinicContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Bill> Bills { get; set; } = null!;
        public virtual DbSet<PatientMedicalInfo> PatientMedicalInfos { get; set; } = null!;
        public virtual DbSet<RegisterPatient> RegisterPatients { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=Clinic;Integrated Security=SSPI");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bill>(entity =>
            {
                entity.HasKey(e => e.Billno)
                    .HasName("PK__Bill__11F3883199625ED3");

                entity.ToTable("Bill");

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(CONVERT([date],getdate()))");

                entity.Property(e => e.Name)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.HasOne(d => d.NoNavigation)
                    .WithMany(p => p.Bills)
                    .HasForeignKey(d => d.No)
                    .HasConstraintName("FK__Bill__No__73BA3083");
            });

            modelBuilder.Entity<PatientMedicalInfo>(entity =>
            {
                entity.ToTable("PatientMedicalInfo");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AppointmentDate).HasColumnType("datetime");

                entity.Property(e => e.Bp).HasColumnName("BP");

                entity.Property(e => e.CholestrolHdl).HasColumnName("Cholestrol_HDL");

                entity.Property(e => e.CholestrolLdc).HasColumnName("Cholestrol_LDC");

                entity.Property(e => e.MedicineSubscription)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.SugarFast).HasColumnName("Sugar_Fast");

                entity.Property(e => e.SugarPost).HasColumnName("Sugar_Post");

                entity.HasOne(d => d.NoNavigation)
                    .WithMany(p => p.PatientMedicalInfos)
                    .HasForeignKey(d => d.No)
                    .HasConstraintName("FK__PatientMedic__No__68487DD7");
            });

            modelBuilder.Entity<RegisterPatient>(entity =>
            {
                entity.HasKey(e => e.No)
                    .HasName("PK__Register__3214D4A80861F340");

                entity.ToTable("RegisterPatient");

                entity.Property(e => e.Address)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
