using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Project.domain.models
{
    public partial class ProjectContext : DbContext
    {
        public ProjectContext()
        {
        }

        public ProjectContext(DbContextOptions<ProjectContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AspNetRole> AspNetRoles { get; set; } = null!;
        public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; } = null!;
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; } = null!;
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; } = null!;
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; } = null!;
        public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; } = null!;
        public virtual DbSet<Catagory> Catagories { get; set; } = null!;
        public virtual DbSet<CombinedUser> CombinedUsers { get; set; } = null!;
        public virtual DbSet<Event> Events { get; set; } = null!;
        public virtual DbSet<EventPicture> EventPictures { get; set; } = null!;
        public virtual DbSet<Location> Locations { get; set; } = null!;
        public virtual DbSet<Reaction> Reactions { get; set; } = null!;
        public virtual DbSet<Rso> Rsos { get; set; } = null!;
        public virtual DbSet<RsoMember> RsoMembers { get; set; } = null!;
        public virtual DbSet<Rsoevent> Rsoevents { get; set; } = null!;
        public virtual DbSet<UniPicture> UniPictures { get; set; } = null!;
        public virtual DbSet<University> Universities { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost;Database=Project;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
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

            modelBuilder.Entity<AspNetUser>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);

                entity.HasMany(d => d.Roles)
                    .WithMany(p => p.Users)
                    .UsingEntity<Dictionary<string, object>>(
                        "AspNetUserRole",
                        l => l.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                        r => r.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
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

            modelBuilder.Entity<Catagory>(entity =>
            {
                entity.HasKey(e => e.CId);

                entity.ToTable("Catagory");

                entity.Property(e => e.CId).HasColumnName("c_id");

                entity.Property(e => e.Entered)
                    .HasColumnType("datetime")
                    .HasColumnName("entered")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<CombinedUser>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("CombinedUsers");

                entity.Property(e => e.AspNetUserId)
                    .HasMaxLength(450)
                    .HasColumnName("AspNetUser_id");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("first_name");

                entity.Property(e => e.FullName)
                    .HasMaxLength(101)
                    .IsUnicode(false)
                    .HasColumnName("full_name");

                entity.Property(e => e.IsAdmin).HasColumnName("isAdmin");

                entity.Property(e => e.IsStudent).HasColumnName("isStudent");

                entity.Property(e => e.IsSuperAdmin).HasColumnName("isSuperAdmin");

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("last_name");

                entity.Property(e => e.UniId).HasColumnName("uni_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<Event>(entity =>
            {
                entity.HasKey(e => e.EId)
                    .HasName("PK_event_1");

                entity.ToTable("Event");

                entity.HasIndex(e => e.CId, "IX_Event_Catagory");

                entity.HasIndex(e => e.Name, "IX_Event_Name");

                entity.HasIndex(e => e.UniId, "IX_Event_Uni");

                entity.Property(e => e.EId).HasColumnName("e_id");

                entity.Property(e => e.CId).HasColumnName("c_id");

                entity.Property(e => e.Description)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.EndTime)
                    .HasColumnType("datetime")
                    .HasColumnName("end_time");

                entity.Property(e => e.LocationId).HasColumnName("location_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("name")
                    .IsFixedLength();

                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("phone");

                entity.Property(e => e.RsoId).HasColumnName("rso_id");

                entity.Property(e => e.StartTime)
                    .HasColumnType("datetime")
                    .HasColumnName("start_time");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.UniId).HasColumnName("uni_id");

                entity.Property(e => e.Visibility)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("visibility");

                entity.HasOne(d => d.CIdNavigation)
                    .WithMany(p => p.Events)
                    .HasForeignKey(d => d.CId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_event_Catagory");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.Events)
                    .HasForeignKey(d => d.LocationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_event_Location");

                entity.HasOne(d => d.Rso)
                    .WithMany(p => p.Events)
                    .HasForeignKey(d => d.RsoId)
                    .HasConstraintName("FK_event_RSO1");

                entity.HasOne(d => d.Uni)
                    .WithMany(p => p.Events)
                    .HasForeignKey(d => d.UniId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_event_University");
            });

            modelBuilder.Entity<EventPicture>(entity =>
            {
                entity.HasKey(e => e.PId);

                entity.ToTable("Event_Picture");

                entity.Property(e => e.PId).HasColumnName("p_id");

                entity.Property(e => e.EId).HasColumnName("e_id");

                entity.Property(e => e.Picture).HasColumnName("picture");

                entity.HasOne(d => d.EIdNavigation)
                    .WithMany(p => p.EventPictures)
                    .HasForeignKey(d => d.EId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Event_Picture_Event");
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.ToTable("Location");

                entity.Property(e => e.LocationId).HasColumnName("location_id");

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.Lattitude)
                    .HasColumnType("decimal(18, 6)")
                    .HasColumnName("lattitude");

                entity.Property(e => e.Longitude)
                    .HasColumnType("decimal(18, 6)")
                    .HasColumnName("longitude");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Reaction>(entity =>
            {
                entity.Property(e => e.ReactionId).HasColumnName("reaction_id");

                entity.Property(e => e.Comment)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("comment");

                entity.Property(e => e.EId).HasColumnName("e_id");

                entity.Property(e => e.Rating).HasColumnName("rating");

                entity.Property(e => e.Rsvp).HasColumnName("rsvp");

                entity.Property(e => e.Save).HasColumnName("save");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.EIdNavigation)
                    .WithMany(p => p.Reactions)
                    .HasForeignKey(d => d.EId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Reactions_event");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Reactions)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Reactions_User");
            });

            modelBuilder.Entity<Rso>(entity =>
            {
                entity.ToTable("RSO");

                entity.Property(e => e.RsoId).HasColumnName("rso_id");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.Name)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("name")
                    .IsFixedLength();

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.UniId).HasColumnName("uni_id");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.Rsos)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RSO_User");

                entity.HasOne(d => d.Uni)
                    .WithMany(p => p.Rsos)
                    .HasForeignKey(d => d.UniId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RSO_University");
            });

            modelBuilder.Entity<RsoMember>(entity =>
            {
                entity.HasKey(e => e.MemberId);

                entity.ToTable("RSO_Member");

                entity.Property(e => e.MemberId).HasColumnName("member_id");

                entity.Property(e => e.IsAdmin).HasColumnName("is_admin");

                entity.Property(e => e.RsoId).HasColumnName("rso_id");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Rso)
                    .WithMany(p => p.RsoMembers)
                    .HasForeignKey(d => d.RsoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RSO_Member_RSO");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.RsoMembers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RSO_Member_User");
            });

            modelBuilder.Entity<Rsoevent>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("RSOEvents");

                entity.Property(e => e.CId).HasColumnName("c_id");

                entity.Property(e => e.Description)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.EId).HasColumnName("e_id");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.EndTime)
                    .HasColumnType("datetime")
                    .HasColumnName("end_time");

                entity.Property(e => e.IsAdmin).HasColumnName("is_admin");

                entity.Property(e => e.LocationId).HasColumnName("location_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("name")
                    .IsFixedLength();

                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("phone");

                entity.Property(e => e.RsoId).HasColumnName("rso_id");

                entity.Property(e => e.RsoName)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("rso_name")
                    .IsFixedLength();

                entity.Property(e => e.StartTime)
                    .HasColumnType("datetime")
                    .HasColumnName("start_time");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.Visibility)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("visibility");
            });

            modelBuilder.Entity<UniPicture>(entity =>
            {
                entity.HasKey(e => e.PId);

                entity.ToTable("Uni_Picture");

                entity.Property(e => e.PId).HasColumnName("p_id");

                entity.Property(e => e.Picture).HasColumnName("picture");

                entity.Property(e => e.UniId).HasColumnName("uni_id");

                entity.HasOne(d => d.Uni)
                    .WithMany(p => p.UniPictures)
                    .HasForeignKey(d => d.UniId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Uni_Picture_University");
            });

            modelBuilder.Entity<University>(entity =>
            {
                entity.HasKey(e => e.UniId);

                entity.ToTable("University");

                entity.Property(e => e.UniId).HasColumnName("uni_id");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.Description)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.LocationId).HasColumnName("location_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.NumStudents).HasColumnName("num_students");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.Universities)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SuperAdmin");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.Universities)
                    .HasForeignKey(d => d.LocationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_University_Location");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.HasIndex(e => e.UniId, "IX_User");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.AspNetUserId)
                    .HasMaxLength(450)
                    .HasColumnName("AspNetUser_id");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("first_name");

                entity.Property(e => e.IsAdmin).HasColumnName("isAdmin");

                entity.Property(e => e.IsStudent)
                    .IsRequired()
                    .HasColumnName("isStudent")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsSuperAdmin).HasColumnName("isSuperAdmin");

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("last_name");

                entity.Property(e => e.UniId).HasColumnName("uni_id");

                entity.HasOne(d => d.AspNetUser)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.AspNetUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_AspNetUsers");

                entity.HasOne(d => d.Uni)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.UniId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_University1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
