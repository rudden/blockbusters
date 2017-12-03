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
	public class CustomersController : Controller
	{
		private const int PageSize = 10;

		private readonly ICustomerRepository _repository;

		public CustomersController(ICustomerRepository repository)
		{
			_repository = repository;
		}

		public async Task<IActionResult> Index()
		{
			var customers = Mapper.Map<List<Customer>>(await _repository.GetCustomersAsync());
			return View(new CustomersViewModel
			{
				Header = "Customers",
				Paging = new Paging<Customer>
				{
					Data = customers.Take(PageSize),
					NumberOfPages = Convert.ToInt32(Math.Ceiling((double)customers.Count() / PageSize)),
					Total = customers.Count()
				}
			});
		}

		public async Task<IActionResult> Page(int id)
		{
			var customers = Mapper.Map<List<Customer>>(await _repository.GetCustomersAsync());
			return View("Index", new CustomersViewModel
			{
				Header = $"Customers, page {id}",
				Paging = new Paging<Customer>
				{
					Data = customers.Skip(PageSize * (id - 1)).Take(PageSize),
					CurrentPage = id,
					NumberOfPages = Convert.ToInt32(Math.Ceiling((double)customers.Count / PageSize)),
					Total = customers.Count
				}
			});
		}

		public async Task<IActionResult> Details(int id)
		{
			var customer = Mapper.Map<Customer>(await _repository.GetCustomerAsync(id));
			return View(new CustomerViewModel
			{
				Customer = customer
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

					var result = await _repository.AddCustomerAsync(customer);

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
	}
}