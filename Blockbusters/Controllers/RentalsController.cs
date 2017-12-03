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
				Header = "Rentals",
				Paging = new Paging<Rental>
				{
					Data = rentals.Take(PageSize),
					NumberOfPages = Convert.ToInt32(Math.Ceiling((double)rentals.Count / PageSize)),
					Total = rentals.Count
				}
			});
		}

		public async Task<IActionResult> Page(int id)
		{
			var rentals = Mapper.Map<List<Rental>>(await _videoRepository.GetRentalsAsync());
			return View("Index", new RentalsViewModel
			{
				Header = $"Rentals, page {id}",
				Paging = new Paging<Rental>
				{
					Data = rentals.Skip(PageSize * (id - 1)).Take(PageSize),
					CurrentPage = id,
					NumberOfPages = Convert.ToInt32(Math.Ceiling((double)rentals.Count / PageSize)),
					Total = rentals.Count
				}
			});
		}

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