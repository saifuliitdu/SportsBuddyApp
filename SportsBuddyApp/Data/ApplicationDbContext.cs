using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SportsBuddy.Data;
using SportsBuddy.Models;

namespace SportsBuddy.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<RecretionalActivity> Activities { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<UserActivityRanking> UserActivityRankings { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ApplicationUser>(user =>
            {
                user.HasIndex(u => u.UserName).IsUnique();
            });

            builder.Entity<RecretionalActivity>()
           .HasKey(p => new { p.ActivityId });

            builder.Entity<RecretionalActivity>()
                .Property(e => e.ActivityId)
                .ValueGeneratedOnAdd();

            builder.Entity<Region>()
           .HasKey(p => new { p.RegionId });

            builder.Entity<Region>()
                .Property(e => e.RegionId)
                .ValueGeneratedOnAdd();

            builder.Entity<UserActivityRanking>().HasKey(sc => new { sc.ActivityId, sc.UserId });

            builder.Entity<UserActivityRanking>()
                .HasOne<RecretionalActivity>(u => u.Activity)
                .WithMany(o => o.UserActivityRankings)
                .HasForeignKey(o => o.ActivityId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.Entity<UserActivityRanking>()
                .HasOne<ApplicationUser>(o => o.User)
                .WithMany(oi => oi.UserActivityRankings)
                .HasForeignKey(oi => oi.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
