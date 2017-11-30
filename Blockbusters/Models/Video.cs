﻿using System;
using System.Collections.Generic;

namespace Blockbusters.Models
{
	public class Video
	{
		public int Id { get; set; }

		public string Title { get; set; }
		public string Subtitle { get; set; }
		public string Storyline { get; set; }
		public int LengthInMinutes { get; set; }
		public VideoType VideoType { get; set; }
		public IEnumerable<Genre> Genres { get; set; }

		public decimal Price { get; set; }
		public string FromYear { get; set; }
		public DateTime Added { get; set; }
	}

	public enum VideoType
	{
		DVD,
		VHS,
		Bluray
	}

	public enum Genre
	{
		Action,
		Drama,
		Anime,
		Thriller,
		Horror,
		Comedy,
		Crime,
		Fantasy,
		Family,
		Adventure,
		SciFi
	}
}
