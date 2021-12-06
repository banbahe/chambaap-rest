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
        public virtual DbSet<StatusCatalog> StatusCatalogs { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                //optionsBuilder.UseSqlServer("Server=BNQLD355;Database=chamba_storage;User Id=sa; Password= develop3r;");
                optionsBuilder.UseSqlServer("Server=chambapp-storage.mssql.somee.com;Database=chambapp-storage ;User Id=edelruhe_SQLLogin_1; Password=wbzui9ute7;");

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

                entity.Property(e => e.Idcandidate).HasColumnName("idcandidate");

                entity.Property(e => e.Idcompany).HasColumnName("idcompany");

                entity.Property(e => e.Idrecruiter).HasColumnName("idrecruiter");

                entity.Property(e => e.IdstatusCatalog).HasColumnName("idstatus_catalog");

                entity.Property(e => e.InterviewDate).HasColumnName("interview_date");

                entity.Property(e => e.Provider)
                    .HasMaxLength(280)
                    .IsUnicode(false)
                    .HasColumnName("provider");

                entity.Property(e => e.ReplyDate).HasColumnName("reply_date");

                entity.Property(e => e.ShipDate).HasColumnName("ship_date");

                entity.HasOne(d => d.IdcandidateNavigation)
                    .WithMany(p => p.InterviewIdcandidateNavigations)
                    .HasForeignKey(d => d.Idcandidate)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_interviews_CANDIDATES");

                entity.HasOne(d => d.IdcompanyNavigation)
                    .WithMany(p => p.Interviews)
                    .HasForeignKey(d => d.Idcompany)
                    .HasConstraintName("FK_interviews_COMPANIES");

                entity.HasOne(d => d.IdrecruiterNavigation)
                    .WithMany(p => p.InterviewIdrecruiterNavigations)
                    .HasForeignKey(d => d.Idrecruiter)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_interviews_RECRUITERS");
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

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.HasIndex(e => new { e.Email, e.IdstatusCatalog }, "unique_index_users")
                    .IsUnique();

                entity.HasIndex(e => new { e.Email, e.IdstatusCatalog }, "uq_users")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ConfigurationEmail)
                    .HasMaxLength(560)
                    .IsUnicode(false)
                    .HasColumnName("configuration_email");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(280)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.IdstatusCatalog).HasColumnName("idstatus_catalog");

                entity.Property(e => e.KeywordsEmail)
                    .IsUnicode(false)
                    .HasColumnName("keywords_email");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(280)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("phone_number");

                entity.Property(e => e.Pwd)
                    .HasMaxLength(280)
                    .IsUnicode(false)
                    .HasColumnName("pwd");

                entity.Property(e => e.ReplyEmail)
                    .HasColumnType("text")
                    .HasColumnName("reply_email");

                entity.Property(e => e.Subject)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("subject");

                entity.Property(e => e.TemplateEmail)
                    .HasColumnType("text")
                    .HasColumnName("template_email");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
