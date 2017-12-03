using System.ComponentModel.DataAnnotations.Schema;

namespace Blockbusters.Models
{
	public class Genre
	{
		public int Id { get; set; }
		public string Name { get; set; }

		[NotMapped]
		public bool IsChecked { get; set; }
	}
}
