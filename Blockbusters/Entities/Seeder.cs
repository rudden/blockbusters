﻿using System;
using System.Linq;
using System.Threading.Tasks;

namespace Blockbusters.Entities
{
	public class Seeder
	{
		private readonly BlockbustersContext _context;

		public Seeder(BlockbustersContext context)
		{
			_context = context;
		}

		public async Task Seed()
		{
			// add some customers
			if (!_context.Customers.Any())
			{
				_context.Customers.Add(new Customer { FirstName = "Simon", LastName = "Rudén", Email = "ruden.simon@gmail.com" });
				_context.Customers.Add(new Customer { FirstName = "Billy", LastName = "Lindfalk", Email = "blindfalk@gmail.com" });
				_context.Customers.Add(new Customer { FirstName = "Carl", LastName = "Svensson", Email = "calle_svensson@hotmail.com" });
				_context.Customers.Add(new Customer { FirstName = "Johanna", LastName = "Frändberg", Email = "johanna_81@gmail.com" });
				_context.Customers.Add(new Customer { FirstName = "Katarina", LastName = "Johannesson", Email = "johannesson_kattis@live.com" });
				_context.Customers.Add(new Customer { FirstName = "Claes", LastName = "Åkesson", Email = "classe_63_akesson@gmail.com" });
				_context.Customers.Add(new Customer { FirstName = "Maria", LastName = "Green", Email = "maria.e.green@hotmail.com" });
				_context.Customers.Add(new Customer { FirstName = "Ludvig", LastName = "Rudén", Email = "luddekudde@gmail.com" });
				_context.Customers.Add(new Customer { FirstName = "Moa", LastName = "Tolleqvist", Email = "qvistmoa@live.com" });
				_context.Customers.Add(new Customer { FirstName = "Yvonne", LastName = "Malmberg", Email = "yvomalm62@gmail.com" });
				_context.Customers.Add(new Customer { FirstName = "Claes", LastName = "Claesson", Email = "claesclaes@gmail.com" });
				_context.Customers.Add(new Customer { FirstName = "Jennie", LastName = "Jönsson", Email = "jjsson@hotmail.com" });
			}

			// add some genres
			if (!_context.Genres.Any())
			{
				_context.Genres.Add(new Genre { Name = "Action" });
				_context.Genres.Add(new Genre { Name = "Drama" });
				_context.Genres.Add(new Genre { Name = "Anime" });
				_context.Genres.Add(new Genre { Name = "Thriller" });
				_context.Genres.Add(new Genre { Name = "Horror" });
				_context.Genres.Add(new Genre { Name = "Comedy" });
				_context.Genres.Add(new Genre { Name = "Crime" });
				_context.Genres.Add(new Genre { Name = "Fantasy" });
				_context.Genres.Add(new Genre { Name = "Family" });
				_context.Genres.Add(new Genre { Name = "Adventure" });
				_context.Genres.Add(new Genre { Name = "SciFi" });
			}

			// add some videotypes
			if (!_context.VideoTypes.Any())
			{
				_context.VideoTypes.Add(new VideoType { Name = "VHS" });
				_context.VideoTypes.Add(new VideoType { Name = "DVD" });
				_context.VideoTypes.Add(new VideoType { Name = "Bluray" });
			}

			await _context.SaveChangesAsync();

			// add some videos
			if (!_context.Videos.Any())
			{
				_context.Videos.Add(new Video
				{
					Title = "Pulp Fiction",
					Subtitle = "The lives of two mob hit men, a boxer, a gangster's wife, and a pair of diner bandits intertwine in four tales of violence and redemption.",
					Storyline = "Jules Winnfield (Samuel L. Jackson) and Vincent Vega (John Travolta) are two hit men who are out to retrieve a suitcase stolen from their employer, mob boss Marsellus Wallace (Ving Rhames). Wallace has also asked Vincent to take his wife Mia (Uma Thurman) out a few days later when Wallace himself will be out of town. Butch Coolidge (Bruce Willis) is an aging boxer who is paid by Wallace to lose his fight. The lives of these seemingly unrelated people are woven together comprising of a series of funny, bizarre and uncalled-for incidents.",
					LengthInMinutes = 154,
					VideoTypeId = _context.VideoTypes.FirstOrDefault(x => x.Name == "DVD").Id,
					Price = 39,
					FromYear = "1994",
					Added = DateTime.Now.AddDays(-30)
				});
				_context.Videos.Add(new Video
				{
					Title = "Spirited Away",
					Subtitle = "During her family's move to the suburbs, a sullen 10-year-old girl wanders into a world ruled by gods, witches, and spirits, and where humans are changed into beasts.",
					Storyline = "Chihiro and her parents are moving to a small Japanese town in the countryside, much to Chihiro's dismay. On the way to their new home, Chihiro's father makes a wrong turn and drives down a lonely one-lane road which dead-ends in front of a tunnel. Her parents decide to stop the car and explore the area. They go through the tunnel and find an abandoned amusement park on the other side, with its own little town. When her parents see a restaurant with great-smelling food but no staff, they decide to eat and pay later. However, Chihiro refuses to eat and decides to explore the theme park a bit more. She meets a boy named Haku who tells her that Chihiro and her parents are in danger, and they must leave immediately. She runs to the restaurant and finds that her parents have turned into pigs. In addition, the theme park turns out to be a town inhabited by demons, spirits, and evil gods. At the center of the town is a bathhouse where these creatures go to relax. The owner of the bathhouse is the evil witch Yubaba, who is intent on keeping all trespassers as captive workers, including Chihiro. Chihiro must rely on Haku to save her parents in hopes of returning to their world.",
					LengthInMinutes = 125,
					VideoTypeId = _context.VideoTypes.FirstOrDefault(x => x.Name == "DVD").Id,
					Price = 29,
					FromYear = "2001",
					Added = DateTime.Now.AddDays(-100)
				});
				_context.Videos.Add(new Video
				{
					Title = "Taxi Driver",
					Subtitle = "A mentally unstable Vietnam War veteran works as a nighttime taxi driver in New York City, where the perceived decadence and sleaze feeds his urge for violent action, while attempting to save a twelve-year-old prostitute in the process.",
					Storyline = "Travis Bickle is an ex-Marine and Vietnam War veteran living in New York City. As he suffers from insomnia, he spends his time working as a taxi driver at night, watching porn movies at seedy cinemas during the day, or thinking about how the world, New York in particular, has deteriorated into a cesspool. He's a loner who has strong opinions about what is right and wrong with mankind. For him, the one bright spot in New York humanity is Betsy, a worker on the presidential nomination campaign of Senator Charles Palantine. He becomes obsessed with her. After an incident with her, he believes he has to do whatever he needs to make the world a better place in his opinion. One of his priorities is to be the savior for Iris, a twelve-year-old runaway and prostitute who he believes wants out of the profession and under the thumb of her pimp and lover Matthew.",
					LengthInMinutes = 113,
					VideoTypeId = _context.VideoTypes.FirstOrDefault(x => x.Name == "VHS").Id,
					Price = 29,
					FromYear = "1976",
					Added = DateTime.Now.AddDays(-59)
				});
				_context.Videos.Add(new Video
				{
					Title = "Dunkirk",
					Subtitle = "Allied soldiers from Belgium, the British Empire and France are surrounded by the German Army, and evacuated during a fierce battle in World War II.",
					Storyline = "May/June 1940. 400,000 British soldiers are holed up in the French port town of Dunkirk. The only way out is via sea, and the Germans have air superiority, bombing the British soldiers and ships without much opposition. The situation looks dire and, in desperation, Britain sends civilian boats to try to evacuate the beleaguered forces. This is that story, seen through the eyes of a soldier amongst those trapped forces, two RAF fighter pilots and a group of civilians on their boat, part of the evacuation fleet.",
					LengthInMinutes = 106,
					VideoTypeId = _context.VideoTypes.FirstOrDefault(x => x.Name == "Bluray").Id,
					Price = 59,
					FromYear = "2017",
					Added = DateTime.Now.AddDays(-5)
				});
				_context.Videos.Add(new Video
				{
					Title = "Jurassic Park",
					Subtitle = "During a preview tour, a theme park suffers a major power breakdown that allows its cloned dinosaur exhibits to run amok.",
					Storyline = "Huge advancements in scientific technology have enabled a mogul to create an island full of living dinosaurs. John Hammond has invited four individuals, along with his two grandchildren, to join him at Jurassic Park. But will everything go according to plan? A park employee attempts to steal dinosaur embryos, critical security systems are shut down and it now becomes a race for survival with dinosaurs roaming freely over the island.",
					LengthInMinutes = 127,
					VideoTypeId = _context.VideoTypes.FirstOrDefault(x => x.Name == "VHS").Id,
					Price = 29,
					FromYear = "1993",
					Added = DateTime.Now.AddDays(-70)
				});
				_context.Videos.Add(new Video
				{
					Title = "Toy Story",
					Subtitle = "A cowboy doll is profoundly threatened and jealous when a new spaceman figure supplants him as top toy in a boy's room.",
					Storyline = "A little boy named Andy loves to be in his room, playing with his toys, especially his doll named \"Woody\". But, what do the toys do when Andy is not with them, they come to life. Woody believes that he has life (as a toy) good. However, he must worry about Andy's family moving, and what Woody does not know is about Andy's birthday party. Woody does not realize that Andy's mother gave him an action figure known as Buzz Lightyear, who does not believe that he is a toy, and quickly becomes Andy's new favorite toy. Woody, who is now consumed with jealousy, tries to get rid of Buzz. Then, both Woody and Buzz are now lost. They must find a way to get back to Andy before he moves without them, but they will have to pass through a ruthless toy killer, Sid Phillips.",
					LengthInMinutes = 81,
					VideoTypeId = _context.VideoTypes.FirstOrDefault(x => x.Name == "DVD").Id,
					Price = 29,
					FromYear = "1993",
					Added = DateTime.Now.AddDays(-62)
				});
				_context.Videos.Add(new Video
				{
					Title = "The Wolf of Wall Street",
					Subtitle = "Based on the true story of Jordan Belfort, from his rise to a wealthy stock-broker living the high life to his fall involving crime, corruption and the federal government.",
					Storyline = "Jordan Belfort is a Long Island penny stockbroker who served 22 months in prison for defrauding investors in a massive 1990s securities scam that involved widespread corruption on Wall Street and in the corporate banking world, including shoe designer Steve Madden.",
					LengthInMinutes = 180,
					VideoTypeId = _context.VideoTypes.FirstOrDefault(x => x.Name == "Bluray").Id,
					Price = 49,
					FromYear = "2013",
					Added = DateTime.Now.AddDays(-20)
				});
				_context.Videos.Add(new Video
				{
					Title = "Fargo",
					Subtitle = "Jerry Lundegaard's inept crime falls apart due to his and his henchmen's bungling and the persistent police work of the quite pregnant Marge Gunderson.",
					Storyline = "Jerry works in his father-in-law's car dealership and has gotten himself in financial problems. He tries various schemes to come up with money needed for a reason that is never really explained. It has to be assumed that his huge embezzlement of money from the dealership is about to be discovered by father-in-law. When all else falls through, plans he set in motion earlier for two men to kidnap his wife for ransom to be paid by her wealthy father (who doesn't seem to have the time of day for son-in-law). From the moment of the kidnapping, things go wrong and what was supposed to be a non-violent affair turns bloody with more blood added by the minute. Jerry is upset at the bloodshed, which turns loose a pregnant sheriff from Brainerd, MN who is tenacious in attempting to solve the three murders in her jurisdiction.",
					LengthInMinutes = 98,
					VideoTypeId = _context.VideoTypes.FirstOrDefault(x => x.Name == "DVD").Id,
					Price = 19,
					FromYear = "1993",
					Added = DateTime.Now.AddDays(-47)
				});
				_context.Videos.Add(new Video
				{
					Title = "Into The Wild",
					Subtitle = "After graduating from Emory University, top student and athlete Christopher McCandless abandons his possessions, gives his entire $24,000 savings account to charity and hitchhikes to Alaska to live in the wilderness. Along the way, Christopher encounters a series of characters that shape his life.",
					Storyline = "A young man bravely sets out alone on what turns into a majestic journey to explore the beauty and wonder of the world. Throughout his travels, which ultimatelty lead him into the wild and wilderness, he seeks and ultimately finds pleasure and joy along with a sense of truth and purpose he has been yearning for all his life. Along the way he meets and deeply touches a cast of others who are all in their own ways also looking to escape or move on from the past and enjoy life again.",
					LengthInMinutes = 148,
					VideoTypeId = _context.VideoTypes.FirstOrDefault(x => x.Name == "DVD").Id,
					Price = 29,
					FromYear = "2007",
					Added = DateTime.Now.AddDays(-33)
				});
				_context.Videos.Add(new Video
				{
					Title = "The Great Escape",
					Subtitle = " Allied prisoners of war plan for several hundred of their number to escape from a German camp during World War II.",
					Storyline = "Based on a true story, a group of allied escape artist-type prisoners-of-war (POWs) are all put in an 'escape proof' camp. Their leader decides to try to take out several hundred all at once. The first half of the film is played for comedy as the prisoners mostly outwit their jailers to dig the escape tunnel. The second half is high adventure as they use boats and trains and planes to get out of occupied Europe.",
					LengthInMinutes = 172,
					VideoTypeId = _context.VideoTypes.FirstOrDefault(x => x.Name == "VHS").Id,
					Price = 19,
					FromYear = "1963",
					Added = DateTime.Now.AddDays(-13)
				});
				_context.Videos.Add(new Video
				{
					Title = "Up",
					Subtitle = "  Seventy-eight year old Carl Fredricksen travels to Paradise Falls in his home equipped with balloons, inadvertently taking a young stowaway. ",
					Storyline = "Carl Fredricksen as a boy wanted to explore South America and find the forbidden Paradise Falls. About 64 years later he gets to begin his journey along with a Boy Scout named Russel with help from 500 balloons.On their journey they discover many new friends including a talking dog and Carl and Russel figure out that someone evil plans.Carl soon realizes that this evildoer is his childhood idol.Will they be able to defeat him and will they find Paradise Falls?",
					LengthInMinutes = 96,
					VideoTypeId = _context.VideoTypes.FirstOrDefault(x => x.Name == "DVD").Id,
					Price = 29,
					FromYear = "2009",
					Added = DateTime.Now.AddDays(-20)
				});
			}

			await _context.SaveChangesAsync();

			// add some videotogenres
			if (!_context.VideoToGenres.Any())
			{
				_context.VideoToGenres.Add(new VideoToGenre
				{
					VideoId = _context.Videos.FirstOrDefault(x => x.Title == "Pulp Fiction").Id,
					GenreId = _context.Genres.FirstOrDefault(x => x.Name == "Crime").Id
				});
				_context.VideoToGenres.Add(new VideoToGenre
				{
					VideoId = _context.Videos.FirstOrDefault(x => x.Title == "Pulp Fiction").Id,
					GenreId = _context.Genres.FirstOrDefault(x => x.Name == "Drama").Id
				});
				_context.VideoToGenres.Add(new VideoToGenre
				{
					VideoId = _context.Videos.FirstOrDefault(x => x.Title == "Spirited Away").Id,
					GenreId = _context.Genres.FirstOrDefault(x => x.Name == "Anime").Id
				});
				_context.VideoToGenres.Add(new VideoToGenre
				{
					VideoId = _context.Videos.FirstOrDefault(x => x.Title == "Spirited Away").Id,
					GenreId = _context.Genres.FirstOrDefault(x => x.Name == "Fantasy").Id
				});
				_context.VideoToGenres.Add(new VideoToGenre
				{
					VideoId = _context.Videos.FirstOrDefault(x => x.Title == "Spirited Away").Id,
					GenreId = _context.Genres.FirstOrDefault(x => x.Name == "Family").Id
				});
				_context.VideoToGenres.Add(new VideoToGenre
				{
					VideoId = _context.Videos.FirstOrDefault(x => x.Title == "Taxi Driver").Id,
					GenreId = _context.Genres.FirstOrDefault(x => x.Name == "Crime").Id
				});
				_context.VideoToGenres.Add(new VideoToGenre
				{
					VideoId = _context.Videos.FirstOrDefault(x => x.Title == "Taxi Driver").Id,
					GenreId = _context.Genres.FirstOrDefault(x => x.Name == "Drama").Id
				});
				_context.VideoToGenres.Add(new VideoToGenre
				{
					VideoId = _context.Videos.FirstOrDefault(x => x.Title == "Dunkirk").Id,
					GenreId = _context.Genres.FirstOrDefault(x => x.Name == "Action").Id
				});
				_context.VideoToGenres.Add(new VideoToGenre
				{
					VideoId = _context.Videos.FirstOrDefault(x => x.Title == "Dunkirk").Id,
					GenreId = _context.Genres.FirstOrDefault(x => x.Name == "Drama").Id
				});
				_context.VideoToGenres.Add(new VideoToGenre
				{
					VideoId = _context.Videos.FirstOrDefault(x => x.Title == "Dunkirk").Id,
					GenreId = _context.Genres.FirstOrDefault(x => x.Name == "Thriller").Id
				});
				_context.VideoToGenres.Add(new VideoToGenre
				{
					VideoId = _context.Videos.FirstOrDefault(x => x.Title == "Jurassic Park").Id,
					GenreId = _context.Genres.FirstOrDefault(x => x.Name == "Thriller").Id
				});
				_context.VideoToGenres.Add(new VideoToGenre
				{
					VideoId = _context.Videos.FirstOrDefault(x => x.Title == "Jurassic Park").Id,
					GenreId = _context.Genres.FirstOrDefault(x => x.Name == "Adventure").Id
				});
				_context.VideoToGenres.Add(new VideoToGenre
				{
					VideoId = _context.Videos.FirstOrDefault(x => x.Title == "Jurassic Park").Id,
					GenreId = _context.Genres.FirstOrDefault(x => x.Name == "SciFi").Id
				});
				_context.VideoToGenres.Add(new VideoToGenre
				{
					VideoId = _context.Videos.FirstOrDefault(x => x.Title == "Toy Story").Id,
					GenreId = _context.Genres.FirstOrDefault(x => x.Name == "Comedy").Id
				});
				_context.VideoToGenres.Add(new VideoToGenre
				{
					VideoId = _context.Videos.FirstOrDefault(x => x.Title == "Toy Story").Id,
					GenreId = _context.Genres.FirstOrDefault(x => x.Name == "Fantasy").Id
				});
				_context.VideoToGenres.Add(new VideoToGenre
				{
					VideoId = _context.Videos.FirstOrDefault(x => x.Title == "Toy Story").Id,
					GenreId = _context.Genres.FirstOrDefault(x => x.Name == "Family").Id
				});
				_context.VideoToGenres.Add(new VideoToGenre
				{
					VideoId = _context.Videos.FirstOrDefault(x => x.Title == "Toy Story").Id,
					GenreId = _context.Genres.FirstOrDefault(x => x.Name == "Adventure").Id
				});
				_context.VideoToGenres.Add(new VideoToGenre
				{
					VideoId = _context.Videos.FirstOrDefault(x => x.Title == "The Wolf of Wall Street").Id,
					GenreId = _context.Genres.FirstOrDefault(x => x.Name == "Comedy").Id
				});
				_context.VideoToGenres.Add(new VideoToGenre
				{
					VideoId = _context.Videos.FirstOrDefault(x => x.Title == "The Wolf of Wall Street").Id,
					GenreId = _context.Genres.FirstOrDefault(x => x.Name == "Drama").Id
				});
				_context.VideoToGenres.Add(new VideoToGenre
				{
					VideoId = _context.Videos.FirstOrDefault(x => x.Title == "The Wolf of Wall Street").Id,
					GenreId = _context.Genres.FirstOrDefault(x => x.Name == "Crime").Id
				});
				_context.VideoToGenres.Add(new VideoToGenre
				{
					VideoId = _context.Videos.FirstOrDefault(x => x.Title == "Fargo").Id,
					GenreId = _context.Genres.FirstOrDefault(x => x.Name == "Thriller").Id
				});
				_context.VideoToGenres.Add(new VideoToGenre
				{
					VideoId = _context.Videos.FirstOrDefault(x => x.Title == "Fargo").Id,
					GenreId = _context.Genres.FirstOrDefault(x => x.Name == "Drama").Id
				});
				_context.VideoToGenres.Add(new VideoToGenre
				{
					VideoId = _context.Videos.FirstOrDefault(x => x.Title == "Fargo").Id,
					GenreId = _context.Genres.FirstOrDefault(x => x.Name == "Crime").Id
				});
				_context.VideoToGenres.Add(new VideoToGenre
				{
					VideoId = _context.Videos.FirstOrDefault(x => x.Title == "Into The Wild").Id,
					GenreId = _context.Genres.FirstOrDefault(x => x.Name == "Adventure").Id
				});
				_context.VideoToGenres.Add(new VideoToGenre
				{
					VideoId = _context.Videos.FirstOrDefault(x => x.Title == "Into The Wild").Id,
					GenreId = _context.Genres.FirstOrDefault(x => x.Name == "Drama").Id
				});
				_context.VideoToGenres.Add(new VideoToGenre
				{
					VideoId = _context.Videos.FirstOrDefault(x => x.Title == "The Great Escape").Id,
					GenreId = _context.Genres.FirstOrDefault(x => x.Name == "Drama").Id
				});
				_context.VideoToGenres.Add(new VideoToGenre
				{
					VideoId = _context.Videos.FirstOrDefault(x => x.Title == "The Great Escape").Id,
					GenreId = _context.Genres.FirstOrDefault(x => x.Name == "Adventure").Id
				});
				_context.VideoToGenres.Add(new VideoToGenre
				{
					VideoId = _context.Videos.FirstOrDefault(x => x.Title == "The Great Escape").Id,
					GenreId = _context.Genres.FirstOrDefault(x => x.Name == "Thriller").Id
				});
				_context.VideoToGenres.Add(new VideoToGenre
				{
					VideoId = _context.Videos.FirstOrDefault(x => x.Title == "Up").Id,
					GenreId = _context.Genres.FirstOrDefault(x => x.Name == "Family").Id
				});
				_context.VideoToGenres.Add(new VideoToGenre
				{
					VideoId = _context.Videos.FirstOrDefault(x => x.Title == "Up").Id,
					GenreId = _context.Genres.FirstOrDefault(x => x.Name == "Adventure").Id
				});
				_context.VideoToGenres.Add(new VideoToGenre
				{
					VideoId = _context.Videos.FirstOrDefault(x => x.Title == "Up").Id,
					GenreId = _context.Genres.FirstOrDefault(x => x.Name == "Comedy").Id
				});
			}

			// add some rentals
			if (!_context.Rentals.Any())
			{
				_context.Rentals.Add(new Rental { RentedByCustomerId = _context.Customers.FirstOrDefault(x => x.Email == "ruden.simon@gmail.com").Id, VideoId = _context.Videos.FirstOrDefault(x => x.Title == "Pulp Fiction").Id, RentedAt = DateTime.Now.AddDays(-33), ShouldBeReturnedAt = DateTime.Now.AddDays(-30), ReturnedAt = DateTime.Now.AddDays(-30) });
				_context.Rentals.Add(new Rental { RentedByCustomerId = _context.Customers.FirstOrDefault(x => x.Email == "jjsson@hotmail.com").Id, VideoId = _context.Videos.FirstOrDefault(x => x.Title == "The Wolf of Wall Street").Id, RentedAt = DateTime.Now.AddDays(-12), ShouldBeReturnedAt = DateTime.Now.AddDays(-9), ReturnedAt = null });
				_context.Rentals.Add(new Rental { RentedByCustomerId = _context.Customers.FirstOrDefault(x => x.Email == "ruden.simon@gmail.com").Id, VideoId = _context.Videos.FirstOrDefault(x => x.Title == "Dunkirk").Id, RentedAt = DateTime.Now.AddDays(-3), ShouldBeReturnedAt = DateTime.Now, ReturnedAt = null });
				_context.Rentals.Add(new Rental { RentedByCustomerId = _context.Customers.FirstOrDefault(x => x.Email == "maria.e.green@hotmail.com").Id, VideoId = _context.Videos.FirstOrDefault(x => x.Title == "Toy Story").Id, RentedAt = DateTime.Now.AddDays(-50), ShouldBeReturnedAt = DateTime.Now.AddDays(-47), ReturnedAt = DateTime.Now.AddDays(-43) });
				_context.Rentals.Add(new Rental { RentedByCustomerId = _context.Customers.FirstOrDefault(x => x.Email == "maria.e.green@hotmail.com").Id, VideoId = _context.Videos.FirstOrDefault(x => x.Title == "Pulp Fiction").Id, RentedAt = DateTime.Now.AddDays(-36), ShouldBeReturnedAt = DateTime.Now.AddDays(-33), ReturnedAt = DateTime.Now.AddDays(-33) });
				_context.Rentals.Add(new Rental { RentedByCustomerId = _context.Customers.FirstOrDefault(x => x.Email == "luddekudde@gmail.com").Id, VideoId = _context.Videos.FirstOrDefault(x => x.Title == "Taxi Driver").Id, RentedAt = DateTime.Now.AddDays(-29), ShouldBeReturnedAt = DateTime.Now.AddDays(-26), ReturnedAt = null });
				_context.Rentals.Add(new Rental { RentedByCustomerId = _context.Customers.FirstOrDefault(x => x.Email == "blindfalk@gmail.com").Id, VideoId = _context.Videos.FirstOrDefault(x => x.Title == "Toy Story").Id, RentedAt = DateTime.Now.AddDays(-100), ShouldBeReturnedAt = DateTime.Now.AddDays(-97), ReturnedAt = DateTime.Now.AddDays(-96) });
				_context.Rentals.Add(new Rental { RentedByCustomerId = _context.Customers.FirstOrDefault(x => x.Email == "classe_63_akesson@gmail.com").Id, VideoId = _context.Videos.FirstOrDefault(x => x.Title == "The Great Escape").Id, RentedAt = DateTime.Now.AddDays(-10), ShouldBeReturnedAt = DateTime.Now.AddDays(-7), ReturnedAt = DateTime.Now.AddDays(-7) });
				_context.Rentals.Add(new Rental { RentedByCustomerId = _context.Customers.FirstOrDefault(x => x.Email == "classe_63_akesson@gmail.com").Id, VideoId = _context.Videos.FirstOrDefault(x => x.Title == "Spirited Away").Id, RentedAt = DateTime.Now.AddDays(-2), ShouldBeReturnedAt = DateTime.Now.AddDays(1), ReturnedAt = null });
				_context.Rentals.Add(new Rental { RentedByCustomerId = _context.Customers.FirstOrDefault(x => x.Email == "classe_63_akesson@gmail.com").Id, VideoId = _context.Videos.FirstOrDefault(x => x.Title == "Dunkirk").Id, RentedAt = DateTime.Now.AddDays(-5), ShouldBeReturnedAt = DateTime.Now.AddDays(-2), ReturnedAt = null });
			}

			await _context.SaveChangesAsync();
		}
	}
}
