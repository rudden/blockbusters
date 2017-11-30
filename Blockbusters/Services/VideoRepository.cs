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
			return await _context.FindAsync<Video>(id);
		}

		public async Task<List<Video>> GetVideosAsync()
		{
			return await _context.Videos.OrderByDescending(v => v.Added).ToListAsync();
		}
	}
}
