using Blockbusters.Models;
using Blockbusters.Models.Helpers;

namespace Blockbusters.ViewModels
{
	public class RentalsViewModel
	{
		public string Header { get; set; }
		public Paging<RentalVideo> Paging { get; set; }
	}
}
