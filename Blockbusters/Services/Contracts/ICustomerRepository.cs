using System.Collections.Generic;
using System.Threading.Tasks;
using Blockbusters.Entities;

namespace Blockbusters.Services.Contracts
{
	public interface ICustomerRepository
	{
		Task<Customer> GetCustomerAsync(int id);
		Task<List<Customer>> GetCustomersAsync();
		Task<bool> AddCustomerAsync(Customer customer);
		Task<bool> DeleteCustomerAsync(int id);
	}
}
