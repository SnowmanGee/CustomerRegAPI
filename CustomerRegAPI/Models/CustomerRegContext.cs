using Microsoft.EntityFrameworkCore;

namespace CustomerRegAPI.Models
{
    /// <summary>
    /// Customer Registaration database context class
    /// </summary>
    public partial class CustomerRegContext : DbContext
    {
        /// <summary>
        /// Standard empty constructor
        /// </summary>
        public CustomerRegContext() { }

        /// <summary>
        /// Constructor with context options
        /// </summary>
        /// <param name="options"></param>
        public CustomerRegContext(DbContextOptions<CustomerRegContext> options)
            : base(options) { }

        /// <summary>
        /// DbSet property for managing customerdetails table
        /// </summary>
        public virtual DbSet<CustomerDetail> CustomerDetails { get; set; } = null!;

        /// <summary>
        /// As part of startup looks up and states the connection to the db
        /// </summary>
        /// <param name="optionsBuilder"></param>
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

        /// <summary>
        /// Validation for entity framework model creation
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerDetail>(entity =>
            {
                entity.HasKey(e => e.CustomerID);
                entity.Property(e => e.CustomerID).ValueGeneratedOnAdd();

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