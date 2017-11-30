using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blockbusters.Entities;
using Blockbusters.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Blockbusters.Services
{
	public class CustomerRepository : ICustomerRepository
	{
		private readonly BlockbustersContext _context;

		public CustomerRepository(BlockbustersContext context)
		{
			_context = context;
		}

		public async Task<Customer> GetCustomerAsync(int id)
		{
			return await _context.FindAsync<Customer>(id);
		}

		public async Task<List<Customer>> GetCustomersAsync()
		{
			return await _context.Customers.OrderByDescending(v => v.LastName).ToListAsync();
		}
	}
}
