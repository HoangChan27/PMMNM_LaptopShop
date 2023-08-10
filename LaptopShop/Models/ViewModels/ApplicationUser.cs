using Microsoft.AspNetCore.Identity;

namespace LaptopShop.Models.ViewModels
{
	public class ApplicationUser : IdentityUser
	{
		public string? FirstName { get; set; }
		public string? LastName { get; set; }
		public string? ProfilePicture { get; set; }
	}
}
