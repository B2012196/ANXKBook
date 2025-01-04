namespace BorrowingService.API.Models
{
    public class BorrowedBook
    {
        [BsonElement("bookId")]
        [BsonGuidRepresentation(MongoDB.Bson.GuidRepresentation.Standard)]
        public Guid BookId { get; set; } // Id của sách

        [BsonElement("title")]
        public string Title { get; set; } // Tên sách
    }
}
