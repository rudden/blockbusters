using System.Collections.Generic;

namespace Blockbusters.Models.Helpers
{
	public class Paging<T>
	{
		public int CurrentPage { get; set; }
		public int NumberOfPages { get; set; }
		public IEnumerable<T> Data { get; set; }
		public int Total { get; set; }
	}
}