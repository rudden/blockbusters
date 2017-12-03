using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Blockbusters.Models;

namespace Blockbusters.ViewModels
{
	public class NewRentalViewModel
	{
		[DisplayName("Customer")]
		[Required(ErrorMessage = "Please choose a customer")]
		public string CustomerId { get; set; }

		[DisplayName("Video")]
		[Required(ErrorMessage = "Please choose a video")]
		public string VideoId { get; set; }

		public List<Customer> Customers { get; set; }
		public List<Video> Videos { get; set; }
	}
}
