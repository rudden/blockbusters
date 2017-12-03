using System;

namespace Blockbusters.Models
{
	public class Rental
	{
		public int Id { get; set; }
		public RentalVideo Video { get; set; }
		public Customer RentedByCustomer { get; set; }
		public DateTime RentedAt { get; set; }
		public DateTime ShouldBeReturnedAt { get; set; }
		public DateTime? ReturnedAt { get; set; }
	}
}
