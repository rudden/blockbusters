using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Blockbusters.Models;

namespace Blockbusters.ViewModels
{
	public class NewVideoViewModel
	{
		[Required(ErrorMessage = "Please enter a title for the video")]
		public string Title { get; set; }

		[Required(ErrorMessage = "Please enter a subtitle for the video")]
		public string Subtitle { get; set; }

		[Required(ErrorMessage = "Please enter a storyline for the video")]
		public string Storyline { get; set; }

		[DisplayName("Created year")]
		[Required(ErrorMessage = "Please enter the year when the video was created")]
		[Range(1900, 2018, ErrorMessage = "Please enter a year between 1900 and 2018")]
		public int FromYear { get; set; }

		[DisplayName("Length in number of minutes")]
		[Required(ErrorMessage = "Please enter the length of the video in minutes")]
		[Range(30, 300, ErrorMessage = "Please enter a length in minutes between 30 and 300")]
		public int LengthInMinutes { get; set; }

		[DisplayName("Rental price")]
		[Required(ErrorMessage = "Please enter the price for the video")]
		[Range(5, 100, ErrorMessage = "Please enter a price between 5 and 100")]
		public decimal Price { get; set; }

		[DisplayName("Type of video")]
		[Required(ErrorMessage = "Please choose what type the video is")]
		public string VideoTypeId { get; set; }

		[DisplayName("Genres")]
		public List<Genre> Genres { get; set; }

		[Required]
		public List<VideoType> VideoTypes { get; set; }
	}
}
