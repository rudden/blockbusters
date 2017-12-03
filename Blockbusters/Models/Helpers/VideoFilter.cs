using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blockbusters.Models.Helpers
{
	public class VideoFilter
	{
		public string VideoName { get; set; }
		public string FromYear { get; set; }
		public decimal MaxPrice { get; set; }
		//public IEnumerable<Genre> Genres { get; set; }
		//public IEnumerable<VideoType> VideoTypes { get; set; }
	}
}
