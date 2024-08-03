using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TopUpGenie_RestApi.Models
{
	public class LoginSession
	{
        [Key]
        public int Id { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

		public string? AccessToken { get; set; }

		public string? RefreshToken { get; set; }

        public DateTime ExpirationDateTime { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public User? User { get; set; }
    }
}
