namespace BookService.API.Models
{
    public class Genre
    {
        public Guid GenreId { get; set; }
        public string GenreName { get; set; }

        [JsonIgnore]
        public ICollection<Book> Books { get; set; }
    }
}
