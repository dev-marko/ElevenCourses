using ElevenCourses.Models;
using ElevenCourses.Models.Relations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using File = ElevenCourses.Models.File;

namespace ElevenCourses.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Course> Courses { get; set; }
    public DbSet<File> Files { get; set; }
    public DbSet<CourseUser> CourseUsers { get; set; }
    public DbSet<Week> Weeks { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        

        builder.Entity<ApplicationUser>(entity =>
        {
            entity.ToTable("User");
        });
        
        builder.Entity<IdentityRole>(entity =>
        {
            entity.ToTable(name: "Role");
        });
        
        builder.Entity<IdentityUserRole<string>>(entity =>
        {
            entity.ToTable("UserRoles");
        });
        
        builder.Entity<IdentityUserClaim<string>>(entity =>
        {
            entity.ToTable("UserClaims");
        });
        
        builder.Entity<IdentityUserLogin<string>>(entity =>
        {
            entity.ToTable("UserLogins");
        });
        
        builder.Entity<IdentityRoleClaim<string>>(entity =>
        {
            entity.ToTable("RoleClaims");
        });
        
        builder.Entity<IdentityUserToken<string>>(entity =>
        {
            entity.ToTable("UserTokens");
        });

        builder.Entity<ApplicationUser>()
            .HasMany(u => u.CreatedCourses)
            .WithOne(c => c.Creator)
            .HasForeignKey(c => c.CreatorId);

        builder.Entity<ApplicationUser>()
            .HasMany(u => u.CreatedFiles)
            .WithOne(f => f.Creator)
            .HasForeignKey(f => f.CreatorId);

        builder.Entity<CourseUser>()
            .HasOne(cu => cu.Course)
            .WithMany(u => u.EnrolledUsers)
            .HasForeignKey(cu => cu.CourseId);

        builder.Entity<CourseUser>()
            .HasOne(cu => cu.User)
            .WithMany(u => u.EnrolledCourses)
            .HasForeignKey(cu => cu.UserId);

        builder.Entity<Course>()
            .HasMany(c => c.Weeks)
            .WithOne(w => w.Course)
            .HasForeignKey(w => w.CourseId);

        builder.Entity<Week>()
            .HasMany(w => w.Files)
            .WithOne(f => f.Week)
            .HasForeignKey(f => f.WeekId);
    }
}
