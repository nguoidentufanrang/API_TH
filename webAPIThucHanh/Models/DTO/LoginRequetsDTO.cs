using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace webAPIThucHanh.Models.DTO
{
	public class LoginRequetsDTO
	{
		public string? Username { get; set; }
		public string? Password { get; set; }
	}
}
