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

		private static List<TableHeader> TableHeaders { get; set; } = new List<TableHeader>
		{
			new TableHeader { Name = "Video", PropertyName = "VideoTitle", SortItem = new SortItem("title") },
			new TableHeader { Name = "Customer name", PropertyName = "CustomerName", SortItem = new SortItem("customer") },
			new TableHeader { Name = "Rented at", PropertyName = "RentedAt", SortItem = new SortItem("rentedat") },
			new TableHeader { Name = "Returned at", PropertyName = "ReturnedAt", SortItem = new SortItem("returnedat") },
		};

		public RentalsController(IVideoRepository videoRepository, ICustomerRepository customerRepository)
		{
			_videoRepository = videoRepository;
			_customerRepository = customerRepository;
		}

		public async Task<IActionResult> Index()
		{
			var rentals = Mapper.Map<List<RentalVideo>>(await _videoRepository.GetRentalsAsync());
			return View(new RentalsViewModel
			{
				Header = "Rentals",
				Paging = new Paging<RentalVideo>
				{
					Table = new TableData<RentalVideo>
					{
						Headers = TableHeaders,
						Data = rentals.Take(PageSize)
					},
					NumberOfPages = Convert.ToInt32(Math.Ceiling((double)rentals.Count / PageSize)),
					Total = rentals.Count
				}
			});
		}

		public async Task<IActionResult> Details(int id)
		{
			var rental = Mapper.Map<Rental>(await _videoRepository.GetRentalAsync(id));
			return View(new RentalViewModel
			{
				Rental = rental
			});
		}

		public async Task<IActionResult> Page(int id, string order, string direction)
		{
			var currentPage = id == 0 ? 1 : id;

			var rentals = Mapper.Map<List<RentalVideo>>(await _videoRepository.GetRentalsAsync());

			var adjustedHeaders = Sorter.ApplySorting(id, order, direction, TableHeaders, rentals, out var items);
			if (adjustedHeaders != null)
			{
				TableHeaders = adjustedHeaders;
			}

			var data = items.Skip(PageSize * (currentPage - 1)).Take(PageSize);

			return View("Index", new RentalsViewModel
			{
				Header = $"Rentals, page {currentPage}",
				Paging = new Paging<RentalVideo>
				{
					Table = new TableData<RentalVideo>
					{
						Headers = TableHeaders,
						Data = data
					},
					CurrentPage = currentPage,
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

		public async Task<IActionResult> Return(int id)
		{
			var rental = await _videoRepository.GetRentalAsync(id);
			var result = await _videoRepository.ReturnRentalAsync(rental);
			if (result)
			{
				return RedirectToAction("index");
			}
			return View("details", new RentalViewModel
			{
				Rental = Mapper.Map<Rental>(rental)
			});
		}
	}
}