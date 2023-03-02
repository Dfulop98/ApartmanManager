using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace DataAccessLayer.DbAccess
{
    public class AMDbContextFactory : IDesignTimeDbContextFactory<AMDbContext>
    {

        public AMDbContext CreateDbContext(string[] args)
        {

            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../ApartmanManagerApi"))
            .AddJsonFile("appsettings.json")
            .Build();

            var optionsBuilder = new DbContextOptionsBuilder<AMDbContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string is null or empty.");

            optionsBuilder.UseNpgsql(connectionString);

            return new AMDbContext(optionsBuilder.Options);
        }

    }
}
