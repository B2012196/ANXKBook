namespace BookService.API.Models
{
    public class BookCopy
    {
        public Guid BookCopyId { get; set; }
        public Guid BookId { get; set; }
        public Guid BookStatusId { get; set; }

        [JsonIgnore]
        public Book Book { get; set; }

        [JsonIgnore]
        public Status BookStatus { get; set; }

    }
}
