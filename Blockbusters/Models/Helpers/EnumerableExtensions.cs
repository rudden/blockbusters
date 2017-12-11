using System;
using System.Linq;
using System.Collections.Generic;

namespace Blockbusters.Models.Helpers
{
	public static class EnumerableExtensions
	{
		public static void Sort<T>(this IEnumerable<T> items, SortData sortData, out IEnumerable<T> data, out IEnumerable<TableHeader> headers)
		{
			data = items;
			headers = sortData.TableHeaders;

			if (string.IsNullOrEmpty(sortData.Order) || string.IsNullOrEmpty(sortData.Direction))
			{
				return;
			}

			var sortDirection = EnumHelper.FromString<SortDirection>(sortData.Direction);

			var header = sortData.TableHeaders.FirstOrDefault(x => x.SortItem != null && x.SortItem.Name == sortData.Order);

			if (header == null)
			{
				return;
			}

			var prop = typeof(T).GetProperty(header.PropertyName);

			switch (sortDirection)
			{
				case SortDirection.Asc:
					data = data.OrderBy(x => prop.GetValue(x, null));
					headers = sortData.TableHeaders.Select(y =>
					{
						if (string.IsNullOrEmpty(y.PropertyName) || y.PropertyName != header.PropertyName) return y;
						y.SortItem.SortDirection = SortDirection.Desc;
						return y;
					}).ToList();
					break;
				case SortDirection.Desc:
					data = data.OrderByDescending(x => prop.GetValue(x, null));
					headers = sortData.TableHeaders.Select(y =>
					{
						if (string.IsNullOrEmpty(y.PropertyName) || y.PropertyName != header.PropertyName) return y;
						y.SortItem.SortDirection = SortDirection.Asc;
						return y;
					}).ToList();
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(sortDirection), sortDirection, null);
			}
		}
	}
}
