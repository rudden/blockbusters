using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blockbusters.Models
{
	public class Video
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		public string Title { get; set; }
		public int LengthInMinutes { get; set; }
		public VideoType Type { get; set; }
		public string ImageUrl { get; set; }

		public decimal Price { get; set; }

		public DateTime Added { get; set; }
	}

	public enum VideoType
	{
		DVD,
		VHS,
		Blueray
	}
}
