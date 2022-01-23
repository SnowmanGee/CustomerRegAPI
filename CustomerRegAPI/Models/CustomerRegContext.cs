using Microsoft.EntityFrameworkCore;

namespace CustomerRegAPI.Models
{
    public partial class CustomerRegContext : DbContext
    {
        public CustomerRegContext() { }

        public CustomerRegContext(DbContextOptions<CustomerRegContext> options)
            : base(options) { }

        public virtual DbSet<CustomerDetail> CustomerDetails { get; set; } = null!;

        public static void Configure(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var envName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                IConfigurationRoot configuration = new ConfigurationBuilder()
                     .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
                     .AddJsonFile("appsettings.json", optional: false)
                     .AddJsonFile($"appsettings.{envName}.json", optional: false)
                     .Build();
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("CustomerDatabase"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerDetail>(entity =>
            {
                entity.HasKey(e => e.CustomerId);
                entity.Property(e => e.CustomerId).ValueGeneratedOnAdd();

                entity.Property(e => e.DateOfBirth).HasColumnType("datetime");

                entity.Property(e => e.EmailAddress)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PolicyReference)
                    .HasMaxLength(9)
                    .IsUnicode(false);

                entity.Property(e => e.Surname)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}