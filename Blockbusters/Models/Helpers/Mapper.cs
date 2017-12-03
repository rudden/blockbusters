using System.Linq;
using System.Collections.Generic;

namespace Blockbusters.Models.Helpers
{
	public class Mapper
	{
		public IEnumerable<string> MapGenres(ICollection<Entities.VideoToGenre> videoToGenres)
		{
			return videoToGenres.Select(videoToGenre => videoToGenre.Genre.Name).ToList();
		}

		public Customer MapRentalCustomer(Entities.Customer customer)
		{
			return new Customer
			{
				Id = customer.Id,
				FirstName = customer.FirstName,
				LastName = customer.LastName,
				Email = customer.Email
			};
		}

		public string MapCustomerName(Entities.Customer customer)
		{
			return $"{customer.FirstName} {customer.LastName}";
		}
	}
}
