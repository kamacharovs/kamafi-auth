using Microsoft.EntityFrameworkCore;
using kamafi.auth.data.models;

namespace kamafi.auth.data
{
    public class AuthDbContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserApiKey> UserApiKeys { get; set; }
        public virtual DbSet<Role> Roles { get; set; }

        public AuthDbContext(DbContextOptions<AuthDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(e =>
            {
                e.ToTable("user");

                e.HasKey(x => x.UserId);

                e.HasIndex(x => x.Email).IsUnique();
                e.HasIndex(x => x.PublicKey);

                e.HasQueryFilter(x => !x.IsDeleted);

                e.Property(x => x.UserId).ValueGeneratedOnAdd().IsRequired();
                e.Property(x => x.PublicKey).ValueGeneratedOnAdd().HasDefaultValueSql("gen_random_uuid()").IsRequired();
                e.Property(x => x.FirstName).HasMaxLength(200).IsRequired();
                e.Property(x => x.LastName).HasMaxLength(200).IsRequired();
                e.Property(x => x.Email).HasMaxLength(200).IsRequired();
                e.Property(x => x.Password).HasMaxLength(200).IsRequired();
                e.Property(x => x.PasswordSalt).HasMaxLength(200).IsRequired();
                e.Property(x => x.RoleName).HasMaxLength(100).IsRequired();
                e.Property(x => x.Created).ValueGeneratedOnAdd().HasDefaultValueSql("current_timestamp").IsRequired();
                e.Property(x => x.Updated).ValueGeneratedOnUpdate().HasDefaultValueSql("current_timestamp").IsRequired();
                e.Property(x => x.IsDeleted).IsRequired();

                e.HasOne(x => x.Role)
                    .WithMany()
                    .HasForeignKey(x => x.RoleName)
                    .OnDelete(DeleteBehavior.NoAction);

                e.HasMany(x => x.ApiKeys)
                    .WithOne()
                    .HasForeignKey(x => x.UserId)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<UserApiKey>(e =>
            {
                e.ToTable("user_api_key");

                e.HasKey(x => new { x.UserId, x.ApiKey });

                e.HasQueryFilter(x => x.IsEnabled);

                e.Property(x => x.UserId).IsRequired();
                e.Property(x => x.ApiKey).HasMaxLength(200).IsRequired();
                e.Property(x => x.Created).ValueGeneratedOnAdd().HasDefaultValueSql("current_timestamp").IsRequired();
                e.Property(x => x.Updated).ValueGeneratedOnUpdate().HasDefaultValueSql("current_timestamp").IsRequired();
                e.Property(x => x.IsEnabled).IsRequired();
            });

            modelBuilder.Entity<Role>(e =>
            {
                e.ToTable("role");

                e.HasKey(x => x.RoleName);
                e.HasIndex(x => x.PublicKey);

                e.Property(x => x.RoleName).HasMaxLength(100).IsRequired();
                e.Property(x => x.PublicKey).ValueGeneratedOnAdd().HasDefaultValueSql("gen_random_uuid()").IsRequired();
                e.Property(x => x.Created).ValueGeneratedOnAdd().HasDefaultValueSql("current_timestamp").IsRequired();
                e.Property(x => x.Updated).ValueGeneratedOnUpdate().HasDefaultValueSql("current_timestamp").IsRequired();
            });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder.UseSnakeCaseNamingConvention();
    }
}
