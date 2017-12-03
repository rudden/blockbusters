using Blockbusters.Entities;
using Blockbusters.Models;
using Blockbusters.Models.Helpers;
using Blockbusters.Services;
using Blockbusters.Services.Contracts;
using Blockbusters.ViewModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Blockbusters
{
	public class Startup
	{
		private readonly IConfigurationRoot _config;

		public Startup(IHostingEnvironment env)
		{
			var builder = new ConfigurationBuilder()
				.SetBasePath(env.ContentRootPath)
				.AddJsonFile("config.json")
				.AddEnvironmentVariables();

			_config = builder.Build();
		}

		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddSingleton(_config);

			services.AddDbContext<BlockbustersContext>();

			services.AddTransient<Seeder>();

			services.AddTransient<Mapper>();

			services.AddMvc();

			services.AddScoped<IVideoRepository, VideoRepository>();
			services.AddScoped<ICustomerRepository, CustomerRepository>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, Seeder seeder, Mapper mapper)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseStaticFiles();

			AutoMapper.Mapper.Initialize(configure =>
			{
				configure.CreateMap<Entities.Video, Models.Video>()
					.ForMember(dest => dest.VideoType, opt => opt.MapFrom(src => src.VideoType.Name))
					.ForMember(dest => dest.Genres, opt => opt.MapFrom(src => mapper.MapGenres(src.VideoToGenres)));
				configure.CreateMap<Entities.Video, Models.RentalVideo>();
				configure.CreateMap<Entities.Customer, Models.Customer>();
				configure.CreateMap<Entities.Genre, Models.Genre>();
				configure.CreateMap<Entities.VideoType, Models.VideoType>();
				configure.CreateMap<Entities.Rental, Models.Rental>()
					.ForMember(dest => dest.RentedByCustomer, opt => opt.MapFrom(src => mapper.MapRentalCustomer(src.Customer)));
				configure.CreateMap<Entities.VideoToGenre, Models.VideoToGenre>();
				configure.CreateMap<NewVideoViewModel, Entities.Video>()
					.ForSourceMember(x => x.Genres, opt => opt.Ignore())
					.ForSourceMember(x => x.VideoTypes, opt => opt.Ignore());
				configure.CreateMap<NewCustomerViewModel, Entities.Customer>();
				configure.CreateMap<Entities.Rental, Models.RentalVideo>();
				configure.CreateMap<NewRentalViewModel, Entities.Rental>()
					.ForMember(dest => dest.RentedByCustomerId, opt => opt.MapFrom(src => src.CustomerId))
					.ForSourceMember(x => x.Videos, opt => opt.Ignore())
					.ForSourceMember(x => x.Customers, opt => opt.Ignore());
			});

			app.UseMvc(c =>
			{
				c.MapRoute
				(
					name: "default",
					template: "{controller}/{action}/{id?}",
					defaults: new { controller = "home", action = "index" }
				);
			});

			seeder.Seed().Wait();
		}
	}
}
