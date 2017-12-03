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
		Task<Genre> GetGenreAsync(int id);
		Task<IEnumerable<Genre>> GetGenresAsync();
		Task<IEnumerable<VideoType>> GetVideoTypesAsync();
		Task<bool> AddVideoAsync(Video video);
		Task<bool> AddGenresToVideo(KeyValuePair<int, List<int>> keyValuePair);
		Task<Rental> GetRentalAsync(int id);
		Task<IEnumerable<Rental>> GetRentalsAsync();
		Task<IEnumerable<Rental>> GetRentalsOnCustomerIdAsync(int id);
		Task<bool> AddRentalAsync(Rental rental);
		Task<bool> ReturnRentalAsync(Rental rental);
	}
}
