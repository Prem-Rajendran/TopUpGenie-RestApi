using System;
using System.ComponentModel.DataAnnotations;

namespace TopUpGenie_RestApi.Models
{
	public class User
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public string? Name { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}