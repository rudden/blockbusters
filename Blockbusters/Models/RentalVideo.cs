using System;

namespace Blockbusters.Models
{
	public class RentalVideo
	{
		public int Id { get; set; }
		public string VideoTitle { get; set; }
		public string CustomerName { get; set; }
		public DateTime RentedAt { get; set; }
		public DateTime ShouldBeReturnedAt { get; set; }
		public DateTime? ReturnedAt { get; set; }
	}
}