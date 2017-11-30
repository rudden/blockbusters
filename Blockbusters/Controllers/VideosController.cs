using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blockbusters.Models;
using Blockbusters.Models.Helpers;
using Blockbusters.Services.Contracts;
using Blockbusters.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Mapper = AutoMapper.Mapper;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Blockbusters.Controllers
{
	public class VideosController : Controller
	{
		private const int PageSize = 10;

		private readonly IVideoRepository _repository;

		public VideosController(IVideoRepository repository)
		{
			_repository = repository;
		}

		// GET: /<controller>/
		public async Task<IActionResult> Index()
		{
			var videos = Mapper.Map<List<Video>>(await _repository.GetVideosAsync());
			return View(new VideosViewModel
			{
				Header = "Videos",
				Paging = new Paging<Video>
				{
					Data = videos.Take(PageSize),
					NumberOfPages = Convert.ToInt32(Math.Ceiling((double)videos.Count() / PageSize)),
					Total = videos.Count()
				}
			});
		}

		// GET: /<controller>/{id}
		[Route("[controller]/{id}")]
		public async Task<IActionResult> Video(int id)
		{
			var video = Mapper.Map<Video>(await _repository.GetVideoAsync(id));
			return View(new VideoViewModel
			{
				Header = video.Title,
				Video = video
			});
		}
	}
}