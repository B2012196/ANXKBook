namespace BookService.API.Models
{
    public class Status
    {
        public Guid StatusId {  get; set; }
        public string StatusName { get; set; }

        [JsonIgnore]
        public ICollection<Book> Books { get; set; }

        [JsonIgnore]
        public ICollection<BookCopy> BookCopys { get; set; }
    }
}
