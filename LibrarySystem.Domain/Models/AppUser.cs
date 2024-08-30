using Microsoft.AspNetCore.Identity;

namespace LibrarySystem.Domain.Models
{
    public class AppUser : IdentityUser
    {
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpire { get; set; }
    }
}
