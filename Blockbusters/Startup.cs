using Blockbusters.Entities;
using Blockbusters.Models;
using Blockbusters.Models.Helpers;
using Blockbusters.Services;
using Blockbusters.Services.Contracts;
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
					.ForMember(dest => dest.Genres, opt => opt.MapFrom(src => mapper.MapGenres(src.Genre)))
					.ForMember(dest => dest.VideoType, opt => opt.MapFrom(src => EnumHelper.FromString<VideoType>(src.VideoType)));
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
