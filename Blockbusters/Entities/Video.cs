using System;
using System.Collections.Generic;
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

		[ForeignKey("VideoTypeId")]
		public VideoType VideoType { get; set; }
		public int VideoTypeId { get; set; }

		public decimal Price { get; set; }
		public string FromYear { get; set; }
		public DateTime Added { get; set; }

		public ICollection<Rental> Rentals { get; set; }
		public ICollection<VideoToGenre> VideoToGenres { get; set; }
	}
}
