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
	public class RentalsController : Controller
	{
		private const int PageSize = 10;

		private readonly IVideoRepository _videoRepository;
		private readonly ICustomerRepository _customerRepository;

		public RentalsController(IVideoRepository videoRepository, ICustomerRepository customerRepository)
		{
			_videoRepository = videoRepository;
			_customerRepository = customerRepository;
		}

		public async Task<IActionResult> Index()
		{
			var rentals = Mapper.Map<List<Rental>>(await _videoRepository.GetRentalsAsync());
			return View(new RentalsViewModel
			{
				Header = "Rental history",
				Paging = new Paging<Rental>
				{
					Data = rentals.Take(PageSize),
					NumberOfPages = Convert.ToInt32(Math.Ceiling((double)rentals.Count / PageSize)),
					Total = rentals.Count
				}
			});
		}

		//[Route("[controller]/{id}")]
		//public async Task<IActionResult> Video(int id)
		//{
		//	var video = Mapper.Map<Video>(await _videoRepository.GetVideoAsync(id));
		//	return View(new VideoViewModel
		//	{
		//		Header = video.Title,
		//		Video = video
		//	});
		//}

		public async Task<IActionResult> Add()
		{
			return View(new NewRentalViewModel
			{
				Customers = Mapper.Map<List<Customer>>(await _customerRepository.GetCustomersAsync()),
				Videos = Mapper.Map<List<Video>>(await _videoRepository.GetVideosAsync())
			});
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Add(NewRentalViewModel model)
		{
			if (ModelState.IsValid)
			{
				try
				{
					var rental = Mapper.Map<Entities.Rental>(model);
					rental.RentedAt = DateTime.Now;
					rental.ShouldBeReturnedAt = DateTime.Now.AddDays(3);

					var result = await _videoRepository.AddRentalAsync(rental);

					if (!result)
					{
						ModelState.AddModelError("", "Could not rent the movie. Please try again!");
					}
				}
				catch (DbUpdateConcurrencyException)
				{
					ModelState.AddModelError("", "Could not rent the movie. Please try again!");
				}
				return RedirectToAction("index");
			}
			return View(model);
		}
	}
}