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
	public class CustomersController : Controller
	{
		private const int PageSize = 10;

		private readonly ICustomerRepository _repository;

		public CustomersController(ICustomerRepository repository)
		{
			_repository = repository;
		}

		// GET: /<controller>/
		public async Task<IActionResult> Index()
		{
			var customers = Mapper.Map<List<Customer>>(await _repository.GetCustomersAsync());
			return View(new CustomersViewModel
			{
				Header = "Videos",
				Paging = new Paging<Customer>
				{
					Data = customers.Take(PageSize),
					NumberOfPages = Convert.ToInt32(Math.Ceiling((double)customers.Count() / PageSize)),
					Total = customers.Count()
				}
			});
		}

		// GET: /<controller>/{id}
		[Route("[controller]/{id}")]
		public async Task<IActionResult> Customer(int id)
		{
			var customer = Mapper.Map<Customer>(await _repository.GetCustomerAsync(id));
			return View(new CustomerViewModel
			{
				Customer = customer
			});
		}
	}
}