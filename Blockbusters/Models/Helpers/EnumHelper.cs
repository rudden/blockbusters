using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blockbusters.Models.Helpers
{
	public class EnumHelper
	{
		public static T FromString<T>(string value)
		{
			return (T)Enum.Parse(typeof(T), value);
		}
	}
}
