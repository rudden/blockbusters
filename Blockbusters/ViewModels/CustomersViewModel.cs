using Blockbusters.Models;
using Blockbusters.Models.Helpers;

namespace Blockbusters.ViewModels
{
	public class CustomersViewModel
	{
		public string Header { get; set; }
		public Paging<Customer> Paging { get; set; }
	}
}
