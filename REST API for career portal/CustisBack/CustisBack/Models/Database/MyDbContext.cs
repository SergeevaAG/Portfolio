using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CustisBack.Models.Database
{
    public partial class MyDbContext : DbContext
    {
        public MyDbContext()
        {
        }

        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CareerTestQuestion> CareerTestQuestions { get; set; } = null!;
        public virtual DbSet<Client> Clients { get; set; } = null!;
        public virtual DbSet<Country> Countries { get; set; } = null!;
        public virtual DbSet<Direction> Directions { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<Faq> Faqs { get; set; } = null!;
        public virtual DbSet<Image> Images { get; set; } = null!;
        public virtual DbSet<ListStackVacancy> ListStackVacancies { get; set; } = null!;
        public virtual DbSet<LoginsArchive> LoginsArchives { get; set; } = null!;
        public virtual DbSet<Office> Offices { get; set; } = null!;
        public virtual DbSet<Possibility> Possibilities { get; set; } = null!;
        public virtual DbSet<Project> Projects { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Social> Socials { get; set; } = null!;
        public virtual DbSet<Stack> Stacks { get; set; } = null!;
        public virtual DbSet<Tech> Teches { get; set; } = null!;
        public virtual DbSet<Text> Texts { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserLogin> UserLogins { get; set; } = null!;
        public virtual DbSet<Vacancy> Vacancies { get; set; } = null!;
        public virtual DbSet<WorkersStory> WorkersStories { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=88.99.68.68;User Id=sqluser;Password=sqldiplom21;Database=Gos_Kamasheva;TrustServerCertificate=true;Encrypt=false");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CareerTestQuestion>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Case1)
                    .IsUnicode(false)
                    .HasColumnName("case1");

                entity.Property(e => e.Case2)
                    .IsUnicode(false)
                    .HasColumnName("case2");

                entity.Property(e => e.Position)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("position");

                entity.Property(e => e.WayId1).HasColumnName("way_id1");

                entity.Property(e => e.WayId2).HasColumnName("way_id2");

                entity.HasOne(d => d.WayId1Navigation)
                    .WithMany(p => p.CareerTestQuestionWayId1Navigations)
                    .HasForeignKey(d => d.WayId1)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("CareerTestQuestions_Directions_id_fk");

                entity.HasOne(d => d.WayId2Navigation)
                    .WithMany(p => p.CareerTestQuestionWayId2Navigations)
                    .HasForeignKey(d => d.WayId2)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("CareerTestQuestions_Directions_id_fk_2");
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.CompanyName).HasColumnName("company_name");

                entity.Property(e => e.ImageId).HasColumnName("image_id");

                entity.HasOne(d => d.Image)
                    .WithMany(p => p.Clients)
                    .HasForeignKey(d => d.ImageId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_Clients_Images");
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Direction>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Name).HasColumnName("name");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Login).HasColumnName("login");

                entity.Property(e => e.Password).HasColumnName("password");

                entity.Property(e => e.Role).HasColumnName("role");
            });

            modelBuilder.Entity<Faq>(entity =>
            {
                entity.ToTable("Faq");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Answer).HasColumnName("answer");

                entity.Property(e => e.Question).HasColumnName("question");
            });

            modelBuilder.Entity<Image>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.ContentType)
                    .HasMaxLength(63)
                    .IsUnicode(false)
                    .HasColumnName("content_type");

                entity.Property(e => e.Desc)
                    .HasMaxLength(127)
                    .IsUnicode(false)
                    .HasColumnName("desc");

                entity.Property(e => e.Image1).HasColumnName("image");

                entity.Property(e => e.ShowGallery).HasColumnName("show_gallery");
            });

            modelBuilder.Entity<ListStackVacancy>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.StackId).HasColumnName("stack_id");

                entity.Property(e => e.VacancyId).HasColumnName("vacancy_id");

                entity.HasOne(d => d.Stack)
                    .WithMany(p => p.ListStackVacancies)
                    .HasForeignKey(d => d.StackId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_ListStackVacancies_Stack");

                entity.HasOne(d => d.Vacancy)
                    .WithMany(p => p.ListStackVacancies)
                    .HasForeignKey(d => d.VacancyId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_ListStackVacancies_Vacancies");
            });

            modelBuilder.Entity<LoginsArchive>(entity =>
            {
                entity.ToTable("LoginsArchive");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ArchiveDate).HasMaxLength(50);
            });

            modelBuilder.Entity<Office>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Contact).HasMaxLength(250);

                entity.Property(e => e.CountryId).HasColumnName("CountryID");

                entity.Property(e => e.Phone).HasMaxLength(50);

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Offices)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Office_Country");
            });

            modelBuilder.Entity<Possibility>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Headline).HasColumnName("headline");

                entity.Property(e => e.ImageId).HasColumnName("image_id");

                entity.HasOne(d => d.Image)
                    .WithMany(p => p.Possibilities)
                    .HasForeignKey(d => d.ImageId)
                    .HasConstraintName("FK_Possibilities_Images");
            });

            modelBuilder.Entity<Project>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Headline).HasColumnName("headline");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.Title).HasMaxLength(50);
            });

            modelBuilder.Entity<Social>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.ImageId).HasColumnName("image_id");

                entity.Property(e => e.Link).HasColumnName("link");

                entity.Property(e => e.ShowSocial).HasColumnName("show_social");

                entity.HasOne(d => d.Image)
                    .WithMany(p => p.Socials)
                    .HasForeignKey(d => d.ImageId)
                    .HasConstraintName("FK_Socials_Images");
            });

            modelBuilder.Entity<Stack>(entity =>
            {
                entity.ToTable("Stack");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Color)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("color")
                    .HasDefaultValueSql("((100000))")
                    .IsFixedLength();

                entity.Property(e => e.Name).HasColumnName("name");
            });

            modelBuilder.Entity<Tech>(entity =>
            {
                entity.HasKey(e => e.Key)
                    .HasName("Tech_pk");

                entity.ToTable("Tech");

                entity.Property(e => e.Key)
                    .HasMaxLength(31)
                    .IsUnicode(false)
                    .HasColumnName("key");

                entity.Property(e => e.Value)
                    .HasMaxLength(254)
                    .IsUnicode(false)
                    .HasColumnName("value");
            });

            modelBuilder.Entity<Text>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Page)
                    .HasMaxLength(50)
                    .HasColumnName("page");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Birthdate).HasColumnType("date");

                entity.Property(e => e.Email).HasMaxLength(150);

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.OfficeId).HasColumnName("OfficeID");

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.HasOne(d => d.Office)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.OfficeId)
                    .HasConstraintName("FK_Users_Offices");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Users_Roles");
            });

            modelBuilder.Entity<UserLogin>(entity =>
            {
                entity.Property(e => e.DateLogin).HasMaxLength(50);

                entity.Property(e => e.DateLogout).HasMaxLength(50);

                entity.Property(e => e.Reason).HasMaxLength(50);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserLogins)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserLogins_Users");
            });

            modelBuilder.Entity<Vacancy>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Comments).HasColumnName("comments");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.DirectionId).HasColumnName("direction_id");

                entity.Property(e => e.Experience).HasColumnName("experience");

                entity.Property(e => e.Geo).HasColumnName("geo");

                entity.Property(e => e.Name).HasColumnName("name");

                entity.Property(e => e.Pluses).HasColumnName("pluses");

                entity.Property(e => e.Requirements).HasColumnName("requirements");

                entity.Property(e => e.Schedule).HasColumnName("schedule");

                entity.Property(e => e.Show).HasColumnName("show");

                entity.Property(e => e.Tasks).HasColumnName("tasks");

                entity.Property(e => e.WorkingConditions).HasColumnName("working_conditions");

                entity.HasOne(d => d.Direction)
                    .WithMany(p => p.Vacancies)
                    .HasForeignKey(d => d.DirectionId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_Vacancies_Directions");
            });

            modelBuilder.Entity<WorkersStory>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Contribution).HasColumnName("contribution");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.ImageId).HasColumnName("image_id");

                entity.Property(e => e.Name).HasColumnName("name");

                entity.Property(e => e.Position).HasColumnName("position");

                entity.HasOne(d => d.Image)
                    .WithMany(p => p.WorkersStories)
                    .HasForeignKey(d => d.ImageId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_WorkersStories_Images");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
