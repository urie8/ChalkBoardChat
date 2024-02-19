using System.ComponentModel.DataAnnotations;

namespace ChalkboardChat.Data.Database.Models
{
	public class MessageModel
	{
		[Key]
		public int Id { get; set; }
		public DateTime Date { get; set; }
		public string Message { get; set; } = null!;
		//public string Username { get; set; }
		public string UserId { get; set; } = null!;
		public ApplicationUser? User { get; set; }
	}
}
