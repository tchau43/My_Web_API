using Microsoft.AspNetCore.Identity;

namespace WebApplication1.Data
{
    public class ApplicationUsers : IdentityUser
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
    }
}
