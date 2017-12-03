namespace Blockbusters.Models
{
	public class VideoToGenre
	{
		public int Id { get; set; }
		public Video Video { get; set; }
		public Genre Genre { get; set; }
	}
}
