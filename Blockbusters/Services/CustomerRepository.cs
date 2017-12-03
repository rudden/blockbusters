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
			return await _context.Customers.Include(x => x.Rentals).FirstOrDefaultAsync(x => x.Id == id);
		}

		public async Task<List<Customer>> GetCustomersAsync()
		{
			return await _context.Customers.Include(x => x.Rentals).OrderByDescending(v => v.LastName).ToListAsync();
		}

		public async Task<bool> AddCustomerAsync(Customer customer)
		{
			_context.Customers.Add(customer);
			return await _context.SaveChangesAsync() >= 0;
		}

		public async Task<bool> DeleteCustomerAsync(Customer customer)
		{
			_context.Customers.Remove(customer);
			return await _context.SaveChangesAsync() >= 0;
		}
	}
}
