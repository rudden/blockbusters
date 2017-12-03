using System;

namespace Blockbusters.Models
{
	public class CustomerRental
	{
		public int Id { get; set; }
		public Video Video { get; set; }
		public DateTime RentedAt { get; set; }
		public DateTime ShouldBeReturnedAt { get; set; }
		public DateTime? ReturnedAt { get; set; }
	}
}
