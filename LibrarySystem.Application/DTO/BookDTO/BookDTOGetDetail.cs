namespace LibrarySystem.Application.DTO
{
    public class BookDTOGetDetail
    {
        public string Category { get; set; }
         public string Title { get; set; }
        public string ISBN { get; set; }
        public string Publisher { get; set; }
        public string Description { get; set; }
        public string LibraryLocation { get; set; }
    }
}