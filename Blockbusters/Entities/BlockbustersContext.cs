using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Blockbusters.Entities
{
	public sealed class BlockbustersContext : DbContext
	{
		private readonly IConfigurationRoot _config;

		public BlockbustersContext(IConfigurationRoot config, DbContextOptions<BlockbustersContext> options)
			: base(options)
		{
			_config = config;

			Database.Migrate();
		}

		public DbSet<Video> Videos { get; set; }
		public DbSet<Customer> Customers { get; set; }
		public DbSet<Rental> Rentals { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);

			optionsBuilder.UseSqlServer(_config["ConnectionStrings:BlockbustersConnection"]);
		}
	}
}
