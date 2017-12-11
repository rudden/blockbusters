using System.Collections.Generic;

namespace Blockbusters.Models.Helpers
{
	public class Paging<T>
	{
		public int CurrentPage { get; set; }
		public int NumberOfPages { get; set; }
		public TableData<T> Table { get; set; }
		public int Total { get; set; }
	}

	public class TableData<T>
	{
		public IEnumerable<TableHeader> Headers { get; set; }
		public IEnumerable<T> Data { get; set; }
	}

	public class TableHeader
	{
		public string Name { get; set; }
		public string PropertyName { get; set; }
		public SortItem SortItem { get; set; }
	}

	public class SortItem
	{
		public string Name { get; set; }
		public SortDirection SortDirection { get; set; }

		public SortItem(string name)
		{
			Name = name;
			SortDirection = SortDirection.Desc;
		}
	}

	public enum SortDirection
	{
		Asc,
		Desc
	}

	public class SortData
	{
		public string Order { get; set; }
		public string Direction { get; set; }
		public IEnumerable<TableHeader> TableHeaders { get; set; }
	}
}