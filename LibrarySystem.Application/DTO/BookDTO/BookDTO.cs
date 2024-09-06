using System.ComponentModel.DataAnnotations;

namespace LibrarySystem.Application.DTO
{
    public class BookDTO
    {
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
        [Required(ErrorMessage = "Location is required")]
        public string LibraryLocation { get; set; }
    }
}
