using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Travel.Models
{
    public class TravelContext : DbContext
    {
        private IConfigurationRoot config;

        public TravelContext(IConfigurationRoot root, DbContextOptions options) 
            : base(options)
        {
            this.config = root;
        }

        public DbSet<Trip> Trips { get; set; }
        public DbSet<Stop> Stops { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(config["ConnectionStrings:TravelDbContext"]);
        }
    }
}