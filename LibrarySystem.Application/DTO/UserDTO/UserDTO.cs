using System.ComponentModel.DataAnnotations;

namespace LibrarySystem.Application.DTO
{
    public class UserDTO
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Position { get; set; }
        public string Privilege { get; set; }
    }
}
