using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blockbusters.Models;
using Blockbusters.Models.Helpers;
using Blockbusters.Services.Contracts;
using Blockbusters.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mapper = AutoMapper.Mapper;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Blockbusters.Controllers
{
	public class VideosController : Controller
	{
		private const int PageSize = 5;

		private readonly IVideoRepository _repository;

		public VideosController(IVideoRepository repository)
		{
			_repository = repository;
		}

		public async Task<IActionResult> Index()
		{
			var videos = Mapper.Map<List<Video>>(await _repository.GetVideosAsync());
			return View(new VideosViewModel
			{
				Header = "Videos",
				Paging = new Paging<Video>
				{
					Data = videos.Take(PageSize),
					NumberOfPages = Convert.ToInt32(Math.Ceiling((double)videos.Count / PageSize)),
					Total = videos.Count
				}
			});
		}

		[Route("[controller]/page/{pageNumber}")]
		public async Task<IActionResult> Page(int pageNumber)
		{
			var videos = Mapper.Map<List<Video>>(await _repository.GetVideosAsync());
			return View(new VideosViewModel
			{
				Header = $"Videos page {pageNumber}",
				Paging = new Paging<Video>
				{
					Data = videos.Skip(PageSize * (pageNumber - 1)).Take(PageSize),
					CurrentPage = pageNumber,
					NumberOfPages = Convert.ToInt32(Math.Ceiling((double)videos.Count / PageSize)),
					Total = videos.Count
				}
			});
		}

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

		[Route("[controller]/add")]
		public async Task<IActionResult> Add()
		{
			return View(new NewVideoViewModel
			{
				Genres = Mapper.Map<List<Genre>>(await _repository.GetGenresAsync()),
				VideoTypes = Mapper.Map<List<VideoType>>(await _repository.GetVideoTypesAsync())
			});
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Route("[controller]/add")]
		public async Task<IActionResult> Add(NewVideoViewModel model)
		{
			var selectedGenres = model.Genres.Where(x => x.IsChecked).Select(g => g.Id).ToList();
			if (!selectedGenres.Any())
			{
				ModelState.AddModelError("Genres", "Please choose at least one genre for the video");
				return View(model);
			}

			if (ModelState.IsValid)
			{
				try
				{
					var video = Mapper.Map<Entities.Video>(model);
					video.Added = DateTime.Now;

					var result = await _repository.AddVideoAsync(video);

					if (result)
					{
						await _repository.AddGenresToVideo(new KeyValuePair<int, List<int>>(video.Id, selectedGenres));
					}
					else
					{
						ModelState.AddModelError("", "Could not add the video. Please try again!");
					}
				}
				catch (DbUpdateConcurrencyException)
				{
					ModelState.AddModelError("", "Could not add the video. Please try again!");
				}
				return RedirectToAction("index");
			}
			return View(model);
		}
	}
}