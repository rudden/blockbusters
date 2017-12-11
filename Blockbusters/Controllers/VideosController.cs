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
		private const int PageSize = 10;

		private readonly IVideoRepository _repository;

		private static List<TableHeader> TableHeaders { get; set; } = new List<TableHeader>
		{
			new TableHeader { Name = "Title", PropertyName = "Title", SortItem = new SortItem("title") },
			new TableHeader { Name = "From year", PropertyName = "FromYear", SortItem = new SortItem("year") },
			new TableHeader { Name = "Type of video", PropertyName = "VideoType", SortItem = new SortItem("type") },
			new TableHeader { Name = "Length", PropertyName = "LengthInMinutes", SortItem = new SortItem("length") },
			new TableHeader { Name = "Genres", SortItem = null },
			new TableHeader { Name = "Price", PropertyName = "Price", SortItem = new SortItem("price") },
			new TableHeader { Name = "Added date", PropertyName = "Added", SortItem = new SortItem("added") }
		};

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
					Table = new TableData<Video>
					{
						Headers = TableHeaders,
						Data = videos.Take(PageSize)
					},
					NumberOfPages = Convert.ToInt32(Math.Ceiling((double)videos.Count / PageSize)),
					Total = videos.Count
				}
			});
		}

		public async Task<IActionResult> Page(int id, string order, string direction)
		{
			var currentPage = id == 0 ? 1 : id;

			var videos = Mapper.Map<List<Video>>(await _repository.GetVideosAsync());

			videos.Sort(new SortData { Order = order, Direction = direction, TableHeaders = TableHeaders }, out var items, out var headers);

			if (headers != null)
			{
				TableHeaders = headers.ToList();
			}

			var data = items.Skip(PageSize * (currentPage - 1)).Take(PageSize);

			return View("Index", new VideosViewModel
			{
				Header = $"Videos, page {currentPage}",
				Paging = new Paging<Video>
				{
					Table = new TableData<Video>
					{
						Headers = TableHeaders,
						Data = data
					},
					CurrentPage = currentPage,
					NumberOfPages = Convert.ToInt32(Math.Ceiling((double)videos.Count / PageSize)),
					Total = videos.Count
				}
			});
		}
		
		public async Task<IActionResult> Details(int id)
		{
			var video = Mapper.Map<Video>(await _repository.GetVideoAsync(id));
			return View(new VideoViewModel
			{
				Header = video.Title,
				Video = video
			});
		}

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

		public async Task<IActionResult> Delete(int id)
		{
			var video = await _repository.GetVideoAsync(id);
			var result = await _repository.DeleteVideoAsync(video);
			if (result)
			{
				return RedirectToAction("index");
			}
			return View("details", new VideoViewModel
			{
				Video = Mapper.Map<Video>(video)
			});
		}
	}
}