using System;
using System.ComponentModel.DataAnnotations;

namespace TopUpGenie_RestApi.Models
{
	public class Admin : User
	{
		[Required]
		public bool IsAdmin { get; set; }
	}
}