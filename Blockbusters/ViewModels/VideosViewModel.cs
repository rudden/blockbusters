using Blockbusters.Models;
using Blockbusters.Models.Helpers;

namespace Blockbusters.ViewModels
{
	public class VideosViewModel
	{
		public string Header { get; set; }
		public Paging<Video> Paging { get; set; }
	}
}
