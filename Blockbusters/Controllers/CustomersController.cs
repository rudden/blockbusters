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
	public class CustomersController : Controller
	{
		private const int PageSize = 10;

		private readonly ICustomerRepository _customerRepository;
		private readonly IVideoRepository _videoRepository;

		private static List<TableHeader> TableHeaders { get; set; } = new List<TableHeader>
		{
			new TableHeader { Name = "Firstname", PropertyName = "FirstName", SortItem = new SortItem("firstname") },
			new TableHeader { Name = "Lastname", PropertyName = "LastName", SortItem = new SortItem("lastname") },
			new TableHeader { Name = "Email", PropertyName = "Email", SortItem = new SortItem("email") }
		};

		public CustomersController(ICustomerRepository customerRepository, IVideoRepository videoRepository)
		{
			_customerRepository = customerRepository;
			_videoRepository = videoRepository;
		}

		public async Task<IActionResult> Index()
		{
			var customers = Mapper.Map<List<Customer>>(await _customerRepository.GetCustomersAsync());
			return View(new CustomersViewModel
			{
				Header = "Customers",
				Paging = new Paging<Customer>
				{
					Table = new TableData<Customer>
					{
						Headers = TableHeaders,
						Data = customers.Take(PageSize)
					},
					NumberOfPages = Convert.ToInt32(Math.Ceiling((double)customers.Count / PageSize)),
					Total = customers.Count
				}
			});
		}

		public async Task<IActionResult> Page(int id, string order, string direction)
		{
			var currentPage = id == 0 ? 1 : id;

			var customers = Mapper.Map<List<Customer>>(await _customerRepository.GetCustomersAsync());

			customers.Sort(new SortData { Order = order, Direction = direction, TableHeaders = TableHeaders }, out var items, out var headers);

			if (headers != null)
			{
				TableHeaders = headers.ToList();
			}

			var data = items.Skip(PageSize * (currentPage - 1)).Take(PageSize);

			return View("Index", new CustomersViewModel
			{
				Header = $"Customers, page {currentPage}",
				Paging = new Paging<Customer>
				{
					Table = new TableData<Customer>
					{
						Headers = TableHeaders,
						Data = data
					},
					CurrentPage = currentPage,
					NumberOfPages = Convert.ToInt32(Math.Ceiling((double)customers.Count / PageSize)),
					Total = customers.Count
				}
			});
		}

		public async Task<IActionResult> Details(int id)
		{
			return View(new CustomerViewModel
			{
				Customer = Mapper.Map<Customer>(await _customerRepository.GetCustomerAsync(id)),
				Rentals = Mapper.Map<IEnumerable<CustomerRental>>(await _videoRepository.GetRentalsOnCustomerIdAsync(id))
			});
		}

		public IActionResult Add()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Add(NewCustomerViewModel model)
		{
			if (ModelState.IsValid)
			{
				try
				{
					var customer = Mapper.Map<Entities.Customer>(model);

					var result = await _customerRepository.AddCustomerAsync(customer);

					if (!result)
					{
						ModelState.AddModelError("", "Could not add the customer. Please try again!");
					}
				}
				catch (DbUpdateConcurrencyException)
				{
					ModelState.AddModelError("", "Could not add the customer. Please try again!");
				}
				return RedirectToAction("index");
			}
			return View(model);
		}

		public async Task<IActionResult> Delete(int id)
		{
			var rentals = await _videoRepository.GetRentalsOnCustomerIdAsync(id);
			foreach (var rental in rentals)
			{
				await _videoRepository.DeleteRentalAsync(rental);
			}
			var customer = await _customerRepository.GetCustomerAsync(id);
			var result = await _customerRepository.DeleteCustomerAsync(customer);
			if (result)
			{
				return RedirectToAction("index");
			}
			return View("details", new CustomerViewModel
			{
				Customer = Mapper.Map<Customer>(customer)
			});
		}
	}
}