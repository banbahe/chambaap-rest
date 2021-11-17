using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace chambapp.storage.Models
{
    public partial class chamba_storageContext : DbContext
    {
        public chamba_storageContext()
        {
        }

        public chamba_storageContext(DbContextOptions<chamba_storageContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<Interview> Interviews { get; set; }
        public virtual DbSet<Recruiter> Recruiters { get; set; }
        public virtual DbSet<StatusCatalog> StatusCatalogs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                // dev
                // optionsBuilder.UseSqlServer("Server=BNQLD355;Database=chamba_storage;User Id=sa;Password=develop3r;Trusted_Connection=True;");
                // prod
                optionsBuilder.UseSqlServer("Server=chambapp-storage.mssql.somee.com;Database=chambapp-storage;User Id=edelruhe_SQLLogin_1;Password=wbzui9ute7;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Company>(entity =>
            {
                entity.ToTable("companies");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Address)
                    .HasMaxLength(280)
                    .IsUnicode(false)
                    .HasColumnName("address");

                entity.Property(e => e.MapLat)
                    .HasMaxLength(280)
                    .IsUnicode(false)
                    .HasColumnName("map_lat");

                entity.Property(e => e.MapLong)
                    .HasMaxLength(280)
                    .IsUnicode(false)
                    .HasColumnName("map_long");

                entity.Property(e => e.MapRawJson)
                    .IsUnicode(false)
                    .HasColumnName("map_raw_json");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(280)
                    .IsUnicode(false)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Interview>(entity =>
            {
                entity.ToTable("interviews");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.EconomicExpectations)
                    .HasMaxLength(280)
                    .IsUnicode(false)
                    .HasColumnName("economic_expectations");

                entity.Property(e => e.EconomicExpectationsOffered)
                    .HasMaxLength(280)
                    .IsUnicode(false)
                    .HasColumnName("economic_expectations_offered");

                entity.Property(e => e.Idcompany).HasColumnName("idcompany");

                entity.Property(e => e.Idrecruiter).HasColumnName("idrecruiter");

                entity.Property(e => e.Idstatus).HasColumnName("idstatus");

                entity.Property(e => e.InterviewDate).HasColumnName("interview_date");

                entity.Property(e => e.Provider)
                    .HasMaxLength(280)
                    .IsUnicode(false)
                    .HasColumnName("provider");

                entity.Property(e => e.ReplyDate).HasColumnName("reply_date");

                entity.Property(e => e.ShipDate).HasColumnName("ship_date");

                entity.HasOne(d => d.IdcompanyNavigation)
                    .WithMany(p => p.Interviews)
                    .HasForeignKey(d => d.Idcompany)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_interviews_companies");

                entity.HasOne(d => d.IdrecruiterNavigation)
                    .WithMany(p => p.Interviews)
                    .HasForeignKey(d => d.Idrecruiter)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_interviews_recruiters");
            });

            modelBuilder.Entity<Recruiter>(entity =>
            {
                entity.ToTable("recruiters");

                entity.HasIndex(e => e.Email, "unique_recruiters_email")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(280)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(280)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("phone_number");

                entity.Property(e => e.ReplyEmail)
                    .IsUnicode(false)
                    .HasColumnName("reply_email");
            });

            modelBuilder.Entity<StatusCatalog>(entity =>
            {
                entity.ToTable("status_catalog");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(280)
                    .IsUnicode(false)
                    .HasColumnName("description");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
