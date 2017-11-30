using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blockbusters.Models.Helpers
{
	public class Mapper
	{
		public IEnumerable<Genre> MapGenres(string genres)
		{
			return genres.Split(",").Select(EnumHelper.FromString<Genre>).ToList();
		}
	}
}
