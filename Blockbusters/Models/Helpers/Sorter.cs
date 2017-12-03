using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;

namespace Blockbusters.Models.Helpers
{
	public static class Sorter
	{
		public static List<TableHeader> ApplySorting<T>(int id, string order, string direction, List<TableHeader> headers, IEnumerable<T> items, out IEnumerable<T> data)
		{
			data = items;

			if (string.IsNullOrEmpty(order) || string.IsNullOrEmpty(direction))
			{
				return headers;
			}

			var sortDirection = EnumHelper.FromString<SortDirection>(direction);

			var tableHeaders = headers as IList<TableHeader> ?? headers.ToList();

			foreach (var header in tableHeaders.Where(x => x.SortItem != null))
			{
				if (header.SortItem.Name != order) continue;

				var prop = typeof(T).GetProperty(header.PropertyName);

				switch (sortDirection)
				{
					case SortDirection.Asc:
						data = data.OrderBy(x => prop.GetValue(x, null));
						return tableHeaders.Select(y =>
						{
							if (string.IsNullOrEmpty(y.PropertyName) || y.PropertyName != header.PropertyName) return y;
							y.SortItem.SortDirection = SortDirection.Desc;
							return y;
						}).ToList();
					case SortDirection.Desc:
						data = data.OrderByDescending(x => prop.GetValue(x, null));
						return tableHeaders.Select(y =>
						{
							if (string.IsNullOrEmpty(y.PropertyName) || y.PropertyName != header.PropertyName) return y;
							y.SortItem.SortDirection = SortDirection.Asc;
							return y;
						}).ToList();
					default:
						throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
				}
			}
			return null;
		}
	}
}
