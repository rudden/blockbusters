using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Blockbusters.ViewModels
{
	public class NewCustomerViewModel
	{
		[DisplayName("Firstname")]
		[Required(ErrorMessage = "Please enter a firstname")]
		public string FirstName { get; set; }

		[DisplayName("Lastname")]
		[Required(ErrorMessage = "Please enter a lastname")]
		public string LastName { get; set; }

		[DisplayName("Email")]
		[Required(ErrorMessage = "Please enter an email")]
		[EmailAddress(ErrorMessage = "Please enter a valid email address")]
		public string Email { get; set; }
	}
}
