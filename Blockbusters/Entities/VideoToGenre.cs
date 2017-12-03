using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blockbusters.Entities
{
	public class VideoToGenre
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		
		[ForeignKey("VideoId")]
		public Video Video { get; set; }
		public int VideoId { get; set; }

		[ForeignKey("GenreId")]
		public Genre Genre { get; set; }
		public int GenreId { get; set; }
	}
}
