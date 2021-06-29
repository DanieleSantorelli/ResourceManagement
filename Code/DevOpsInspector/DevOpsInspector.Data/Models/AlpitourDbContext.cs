using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DevOpsInspector.Data.Models
{
    public partial class AlpitourDbContext : DbContext
    {
        public AlpitourDbContext()
        {
        }

        public AlpitourDbContext(DbContextOptions<AlpitourDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Activities> Activities { get; set; }
        public virtual DbSet<Capacities> Capacities { get; set; }
        public virtual DbSet<DaysOff> DaysOff { get; set; }
        public virtual DbSet<Iterations> Iterations { get; set; }
        public virtual DbSet<Members> Members { get; set; }
        public virtual DbSet<MembersTeams> MembersTeams { get; set; }
        public virtual DbSet<ProjectAreaPath> ProjectAreaPath { get; set; }
        public virtual DbSet<Projects> Projects { get; set; }
        public virtual DbSet<RootClassificationNodes> RootClassificationNodes { get; set; }
        public virtual DbSet<Teams> Teams { get; set; }
        public virtual DbSet<VActivity> VActivity { get; set; }
        public virtual DbSet<VDaysOff> VDaysOff { get; set; }
        public virtual DbSet<VIterations> VIterations { get; set; }
        public virtual DbSet<VTeamMembers> VTeamMembers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#if DEBUG
                optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=ALPITOURDEVOPS;Persist Security Info=True;User ID=jd3v;Password=wmjdd@asd");
#else
                optionsBuilder.UseSqlServer("Server=tcp:databaseresourcemanagement.database.windows.net,1433;Initial Catalog=alpitourdevops;Persist Security Info=False;User ID=devopsadmin;Password=Alpitour2021!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
#endif
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Activities>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.DateCreation)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateModify).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(250);

                entity.HasOne(d => d.Capacity)
                    .WithMany(p => p.Activities)
                    .HasForeignKey(d => d.CapacityId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Activities$Capacities");
            });

            modelBuilder.Entity<Capacities>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.DateCreation)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateModify).HasColumnType("datetime");

                entity.Property(e => e.Url).HasMaxLength(250);

                entity.HasOne(d => d.Iteration)
                    .WithMany(p => p.Capacities)
                    .HasForeignKey(d => d.IterationId)
                    .HasConstraintName("FK_Capacities$Iterations");

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.Capacities)
                    .HasForeignKey(d => d.TeamId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Capacities$Teams");
            });

            modelBuilder.Entity<DaysOff>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.DateCreation)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateModify).HasColumnType("datetime");

                entity.Property(e => e.End).HasColumnType("datetime");

                entity.Property(e => e.Start).HasColumnType("datetime");

                entity.HasOne(d => d.Capacity)
                    .WithMany(p => p.DaysOff)
                    .HasForeignKey(d => d.CapacityId)
                    .HasConstraintName("FK_DaysOff$Capacities");
            });

            modelBuilder.Entity<Iterations>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.DateCreation)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateModify).HasColumnType("datetime");

                entity.Property(e => e.FinishDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(250);

                entity.Property(e => e.Path).HasMaxLength(250);

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.TimeFrame).HasMaxLength(250);

                entity.Property(e => e.Url).HasMaxLength(250);

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.Iterations)
                    .HasForeignKey(d => d.TeamId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Iterations$Teams");
            });

            modelBuilder.Entity<Members>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.AvatarUrl).HasMaxLength(250);

                entity.Property(e => e.DateCreation)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateModify).HasColumnType("datetime");

                entity.Property(e => e.Descriptor).HasMaxLength(250);

                entity.Property(e => e.DisplayName).HasMaxLength(250);

                entity.Property(e => e.ImageUrl).HasMaxLength(250);

                entity.Property(e => e.UniqueName).HasMaxLength(250);

                entity.Property(e => e.Url).HasMaxLength(250);
            });

            modelBuilder.Entity<MembersTeams>(entity =>
            {
                entity.ToTable("Members_Teams");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.DateCreation)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateModify).HasColumnType("datetime");

                entity.Property(e => e.IsInTeam)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.MembersTeams)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_Members_Teams$Members");

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.MembersTeams)
                    .HasForeignKey(d => d.TeamId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Members_Teams$Teams");
            });

            modelBuilder.Entity<ProjectAreaPath>(entity =>
            {
                entity.ToTable("Project_AreaPath");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.DateCreation)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateModify).HasColumnType("datetime");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.ProjectAreaPath)
                    .HasForeignKey(d => d.ProjectId)
                    .HasConstraintName("FK_Project_AreaPath_Project");

                entity.HasOne(d => d.RootClassificationNode)
                    .WithMany(p => p.ProjectAreaPath)
                    .HasForeignKey(d => d.RootClassificationNodeId)
                    .HasConstraintName("FK_Project_AreaPath_RootClassificationNodes");
            });

            modelBuilder.Entity<Projects>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.DateCreation)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateModify).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(250);

                entity.Property(e => e.LastUpdateTime).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(250);

                entity.Property(e => e.State).HasMaxLength(250);

                entity.Property(e => e.Url).HasMaxLength(250);

                entity.Property(e => e.Visibility).HasMaxLength(250);
            });

            modelBuilder.Entity<RootClassificationNodes>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.DateCreation)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateModify).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.Path)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.StructureType).HasMaxLength(150);

                entity.Property(e => e.Url)
                    .IsRequired()
                    .HasMaxLength(150);
            });

            modelBuilder.Entity<Teams>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.DateCreation)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateModify).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(250);

                entity.Property(e => e.IdentityUrl).HasMaxLength(250);

                entity.Property(e => e.Name).HasMaxLength(250);

                entity.Property(e => e.ProjectName).HasMaxLength(250);

                entity.Property(e => e.Url).HasMaxLength(250);

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.Teams)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Teams_Projects");
            });

            modelBuilder.Entity<VActivity>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("V_Activity");

                entity.Property(e => e.Attività).HasMaxLength(250);

                entity.Property(e => e.FineSprint).HasColumnType("date");

                entity.Property(e => e.InizioSprint).HasColumnType("date");

                entity.Property(e => e.NomeUtente).HasMaxLength(250);

                entity.Property(e => e.Progetto).HasMaxLength(250);

                entity.Property(e => e.Sprint).HasMaxLength(250);

                entity.Property(e => e.Team).HasMaxLength(250);

                entity.Property(e => e.TipoSprint).HasMaxLength(250);

                entity.Property(e => e.Utente).HasMaxLength(250);
            });

            modelBuilder.Entity<VDaysOff>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("V_DaysOff");

                entity.Property(e => e.Attivita).HasMaxLength(250);

                entity.Property(e => e.FineDayOff).HasColumnType("date");

                entity.Property(e => e.FineSprint).HasColumnType("date");

                entity.Property(e => e.InizioDayOff).HasColumnType("date");

                entity.Property(e => e.InizioSprint).HasColumnType("date");

                entity.Property(e => e.NomeUtente).HasMaxLength(250);

                entity.Property(e => e.Progetto).HasMaxLength(250);

                entity.Property(e => e.Sprint).HasMaxLength(250);

                entity.Property(e => e.Team).HasMaxLength(250);

                entity.Property(e => e.TipoSprint).HasMaxLength(250);

                entity.Property(e => e.Utente).HasMaxLength(250);
            });

            modelBuilder.Entity<VIterations>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("V_Iterations");

                entity.Property(e => e.Fine)
                    .HasColumnName("FIne")
                    .HasColumnType("date");

                entity.Property(e => e.Inizio).HasColumnType("date");

                entity.Property(e => e.Progetto).HasMaxLength(250);

                entity.Property(e => e.Sprint).HasMaxLength(250);

                entity.Property(e => e.Team).HasMaxLength(250);

                entity.Property(e => e.TipoSprint).HasMaxLength(250);
            });

            modelBuilder.Entity<VTeamMembers>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("V_Team_Members");

                entity.Property(e => e.Membro).HasMaxLength(250);

                entity.Property(e => e.Progetto).HasMaxLength(250);

                entity.Property(e => e.Team).HasMaxLength(250);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
