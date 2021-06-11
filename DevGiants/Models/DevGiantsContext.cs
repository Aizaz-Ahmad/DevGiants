using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace DevGiants.Models
{
    public partial class DevGiantsContext : DbContext
    {
        public DevGiantsContext()
        {
        }

        public DevGiantsContext(DbContextOptions<DevGiantsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<JobApplication> JobApplications { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
               optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Initial Catalog=DevGiants;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Admin>(entity =>
            {
                entity.ToTable("admin");

                entity.Property(e => e.AdminId).HasColumnName("admin_id");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("username");
            });

            modelBuilder.Entity<JobApplication>(entity =>
            {
                entity.HasKey(e => e.ApplicationId)
                    .HasName("PK__tmp_ms_x__3BCBDCF2B403E5EE");

                entity.ToTable("job_application");

                entity.Property(e => e.ApplicationId).HasColumnName("application_id");

                entity.Property(e => e.Ageinyear)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ageinyear");

                entity.Property(e => e.Already).HasColumnName("already");

                entity.Property(e => e.AppliedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("applied_at")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Appliedfor)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("appliedfor");

                entity.Property(e => e.Cnic)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("cnic");

                entity.Property(e => e.Contact)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("contact");

                entity.Property(e => e.Disability).HasColumnName("disability");

                entity.Property(e => e.Dob)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("dob");

                entity.Property(e => e.Domicile)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("domicile");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Fathername)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("fathername");

                entity.Property(e => e.Filename)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("filename");

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("gender");

                entity.Property(e => e.Hafiz).HasColumnName("hafiz");

                entity.Property(e => e.Marital).HasColumnName("marital");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.OtherContact)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("otherContact");

                entity.Property(e => e.PermanentAddress)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("permanentAddress");

                entity.Property(e => e.Placeofposting)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("placeofposting");

                entity.Property(e => e.PostalAddress)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("postalAddress");

                entity.Property(e => e.Religion)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("religion");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
