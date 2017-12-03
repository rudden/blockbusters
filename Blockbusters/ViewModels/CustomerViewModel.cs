using System.Collections.Generic;
using Blockbusters.Models;

namespace Blockbusters.ViewModels
{
	public class CustomerViewModel
	{
		public Customer Customer { get; set; }
		public IEnumerable<CustomerRental> Rentals { get; set; }
	}
}
