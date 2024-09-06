using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LibrarySystem.Domain.Models
{
    public class Book
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Category is required")]
        public string Category { get; set; }
        [Required(ErrorMessage = "Author is required")]
        public string Author { get; set; }
        [Required(ErrorMessage = "Publication Year is required")]
        public int PublicationYear { get; set; }
        [Required(ErrorMessage = "Publisher is required")]
        public string Publisher { get; set; }
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
        public int Price { get; set; }
        public DateTime PurchaseDate { get; set; }

        [Required(ErrorMessage = "ISBN is required")]
        public string ISBN { get; set; }
        [Required(ErrorMessage = "ISBN is required")]
        public string LibraryLocation { get; set; }
        public bool DeleteStamp { get; set; }
        public string? DeleteReasoning { get; set; }
        public int AvailableBooks { get; set; }
        public string? Language { get; set; }
        [JsonIgnore]
        public virtual ICollection<Borrowing> Borrows { get; set; } = new List<Borrowing>();
    }
}
