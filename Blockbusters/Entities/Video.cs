using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blockbusters.Entities
{
	public class Video
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		public string Title { get; set; }
		public string Subtitle { get; set; }
		public string Storyline { get; set; }
		public int LengthInMinutes { get; set; }
		public string VideoType { get; set; }
		public string Genre { get; set; }
		public string ImageUrl { get; set; }

		public decimal Price { get; set; }
		public string FromYear { get; set; }
		public DateTime Added { get; set; }
	}
}
