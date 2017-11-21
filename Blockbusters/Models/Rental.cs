using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blockbusters.Models
{
	public class Rental
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		[ForeignKey("VideoId")]
		public Video Video { get; set; }
		public int VideoId { get; set; }

		[ForeignKey("RentedByCustomerId")]
		public Customer Customer { get; set; }
		public int RentedByCustomerId { get; set; }

		public DateTime RentedAt { get; set; }
		public DateTime ShouldBeReturnedAt { get; set; }
		public DateTime? ReturnedAt { get; set; }
	}
}
