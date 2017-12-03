﻿using System;

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
