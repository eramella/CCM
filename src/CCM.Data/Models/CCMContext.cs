using CCM.Data.Contracts;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CCM.Data.Models
{
    public class CCMContext : IdentityDbContext<CCMUser>
    {
        private IConfigurationRoot _config;
        private IUserResolverService _userResolver;


        public CCMContext(DbContextOptions options) : base(options)
        {
        }

        public CCMContext(IConfigurationRoot config, IUserResolverService userResolver, DbContextOptions options) : base(options)
        {
            _config = config;
            _userResolver = userResolver;

        }      

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            var appConfig = builder.Entity<AppSettings>();
            appConfig.HasKey(k => k.Id);
            appConfig.Property(k => k.Id).HasDefaultValue(true);
            appConfig.Property(p => p.CampName).HasMaxLength(40);
            appConfig.Property(p => p.TagLine).HasMaxLength(256);
            appConfig.Property(p => p.Pic1ContentType).HasMaxLength(50);
            appConfig.Property(p => p.Pic1FileName).HasMaxLength(200);
            appConfig.Property(p => p.Pic2ContentType).HasMaxLength(50);
            appConfig.Property(p => p.Pic2FileName).HasMaxLength(200);
            appConfig.Property(p => p.Pic3ContentType).HasMaxLength(40);
            appConfig.Property(p => p.Pic3FileName).HasMaxLength(200);
            appConfig.Property(p => p.Pic4ContentType).HasMaxLength(40);
            appConfig.Property(p => p.Pic4FileName).HasMaxLength(200);
            appConfig.Property(p => p.Pic5ContentType).HasMaxLength(40);
            appConfig.Property(p => p.Pic5FileName).HasMaxLength(200);


            var session = builder.Entity<Session>();
            session.HasMany(m => m.TagSessions).WithOne(o => o.Session);
            session.HasOne(o => o.User).WithMany(m => m.Sessions);
            session.Property(p => p.Title).HasMaxLength(500);

            var tagSession = builder.Entity<TagSession>();
            tagSession.HasKey(k => new { k.TagId, k.SessionId });
            tagSession.HasOne(o => o.Session)
                .WithMany(m => m.TagSessions)
                .HasForeignKey(f => f.SessionId);
            tagSession.HasOne(o => o.Tag)
                .WithMany(m => m.TagSessions)
                .HasForeignKey(f => f.TagId);

            var tag = builder.Entity<Tag>();
            tag.HasMany(m => m.TagSessions)
                .WithOne(o => o.Tag);
            tag.Property(p => p.Name).HasMaxLength(64);

            var camp = builder.Entity<Camp>();
            camp.Property(p => p.LocationName).HasMaxLength(128);
            camp.Property(p => p.LocationAddress).HasMaxLength(256);
            camp.Property(p => p.LocationCity).HasMaxLength(64);
            camp.Property(p => p.LocationState).HasMaxLength(2);
            camp.Property(p => p.LocationZip).HasMaxLength(5);

            var user = builder.Entity<CCMUser>();
            user.Property(p => p.FirstName).HasMaxLength(256);
            user.Property(p => p.LastName).HasMaxLength(256);
            user.Property(p => p.AvatarContentType).HasMaxLength(256);
            user.Property(p => p.AvatarFileName).HasMaxLength(512);
            user.Property(p => p.LinkedinUrl).HasMaxLength(256);
            user.Property(p => p.TwitterUrl).HasMaxLength(256);
        }


        public DbSet<Camp> Camps { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<TagSession> TagSessions { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<SponsorType> SponsorTypes { get; set; }
        public DbSet<Sponsor> Sponsors { get; set; }
        public DbSet<AppSettings> AppSettings { get; set; }

        public override int SaveChanges()
        {
            createLog();

            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            createLog();

            return base.SaveChangesAsync(cancellationToken);
        }

        private void createLog()
        {
            var changedEntities = ChangeTracker.Entries().Where(p => p.State == EntityState.Modified).ToList();
            var now = DateTime.UtcNow;

            foreach (var changedEntity in changedEntities)
            {
                var entityName = changedEntity.Entity.GetType().Name;
                var primaryKey = changedEntity.Metadata.FindPrimaryKey();

                foreach (var prop in changedEntity.Metadata.GetProperties())
                {
                    var originalValue = (changedEntity.Property(prop.Name).OriginalValue ?? "").ToString();
                    var currentValue = (changedEntity.Property(prop.Name).CurrentValue ?? "").ToString();
                    if (originalValue != currentValue)
                    {
                        AuditLog log = new AuditLog()
                        {
                            EntityName = entityName,
                            PrimaryKeyValue = primaryKey.ToString(),
                            PropertyName = prop.Name,
                            OldValue = originalValue,
                            NewValue = currentValue,
                            DateChanged = now,
                            UserId = _userResolver.GetUserId()
                        };
                        AuditLogs.Add(log);
                    }
                }
            }
        }
    }
}
