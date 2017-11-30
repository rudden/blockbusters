using System.Collections.Generic;
using System.Threading.Tasks;
using Blockbusters.Entities;

namespace Blockbusters.Services.Contracts
{
	public interface IVideoRepository
	{
		Task<Video> GetVideoAsync(int id);
		//Task<Video> GetVideoOnFilterAsync(VideoFilter filter);
		Task<List<Video>> GetVideosAsync();
	}
}
