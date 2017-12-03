using System;
using System.Collections.Generic;

namespace Blockbusters.Models
{
	public class Video
	{
		public int Id { get; set; }

		public string Title { get; set; }
		public string Subtitle { get; set; }
		public string Storyline { get; set; }
		public int LengthInMinutes { get; set; }
		public string VideoType { get; set; }
		public IEnumerable<string> Genres { get; set; }
		public IEnumerable<Rental> Rentals { get; set; }
		public decimal Price { get; set; }
		public string FromYear { get; set; }
		public DateTime Added { get; set; }
	}
}
