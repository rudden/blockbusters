using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blockbusters.Entities;
using Blockbusters.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Blockbusters.Services
{
	public class VideoRepository : IVideoRepository
	{
		private readonly BlockbustersContext _context;

		public VideoRepository(BlockbustersContext context)
		{
			_context = context;
		}

		public async Task<Video> GetVideoAsync(int id)
		{
			return await _context.Videos
				.Include(x => x.VideoType)
				.Include(x => x.VideoToGenres)
					.ThenInclude(x => x.Genre)
				.Include(x => x.Rentals)
					.ThenInclude(x => x.Customer)
				.FirstOrDefaultAsync(x => x.Id == id);
		}

		public async Task<List<Video>> GetVideosAsync()
		{
			return await _context.Videos
				.Include(x => x.VideoType)
				.Include(x => x.VideoToGenres)
					.ThenInclude(x => x.Genre)
				.Include(x => x.Rentals)
					.ThenInclude(x => x.Customer)
				.OrderByDescending(v => v.Added).ToListAsync();
		}

		public async Task<Genre> GetGenreAsync(int id)
		{
			return await _context.FindAsync<Genre>(id);
		}

		public async Task<IEnumerable<Genre>> GetGenresAsync()
		{
			return await _context.Genres.ToListAsync();
		}

		public async Task<IEnumerable<VideoType>> GetVideoTypesAsync()
		{
			return await _context.VideoTypes.ToListAsync();
		}

		public async Task<bool> AddVideoAsync(Video video)
		{
			_context.Videos.Add(video);
			return await _context.SaveChangesAsync() >= 0;
		}

		public async Task<bool> AddGenresToVideo(KeyValuePair<int, List<int>> keyValuePair)
		{
			foreach (var genreId in keyValuePair.Value)
			{
				_context.VideoToGenres.Add(new VideoToGenre
				{
					VideoId = keyValuePair.Key,
					GenreId = genreId
				});
			}
			return await _context.SaveChangesAsync() >= 0;
		}

		public async Task<Rental> GetRentalAsync(int id)
		{
			return await _context.Rentals
				.Include(x => x.Customer)
				.Include(x => x.Video)
				.FirstOrDefaultAsync(x => x.Id == id);
		}

		public async Task<IEnumerable<Rental>> GetRentalsAsync()
		{
			return await _context.Rentals
				.Include(x => x.Customer)
				.Include(x => x.Video)
				.OrderByDescending(x => x.RentedAt).ToListAsync();
		}

		public async Task<IEnumerable<Rental>> GetRentalsOnCustomerIdAsync(int id)
		{
			return await _context.Rentals
				.Include(x => x.Video)
				.Where(c => c.RentedByCustomerId == id)
				.OrderByDescending(x => x.RentedAt).ToListAsync();
		}

		public async Task<bool> AddRentalAsync(Rental rental)
		{
			_context.Rentals.Add(rental);
			return await _context.SaveChangesAsync() >= 0;
		}

		public async Task<bool> ReturnRentalAsync(Rental rental)
		{
			rental.ReturnedAt = DateTime.Now;
			_context.Rentals.Update(rental);
			return await _context.SaveChangesAsync() >= 0;
		}
	}
}
