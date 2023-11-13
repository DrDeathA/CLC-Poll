using Microsoft.EntityFrameworkCore;
using Poll_API.Data.Entities;

namespace Poll_API.Data
{
    public class DataContext : DbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<Poll> Poll { get; set; }
        public DbSet<Question> Question { get; set; }
        public DbSet<Option> Option { get; set; }

        public static readonly DbContextOptions<DataContext> options;

        static DataContext()
        {
            var config = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
#if DEBUG
            .AddJsonFile("appsettings.Development.json")
#else
            .AddJsonFile("appsettings.json")
#endif
            .Build();

            options = new DbContextOptionsBuilder<DataContext>()
               .UseSqlServer(config.GetConnectionString("db"))
               .Options;
        }

        public DataContext() : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<User>()
                .HasIndex(x => x.Email)
                .IsUnique();
        }

        static public void Init()
        {
            using var db = new DataContext();
        }
    }
}
