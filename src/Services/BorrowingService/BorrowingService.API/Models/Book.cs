namespace BorrowingService.API.Models
{
    public class Book
    {
        [BsonElement("bookId")]
        [BsonGuidRepresentation(MongoDB.Bson.GuidRepresentation.Standard)]
        public Guid BookId { get; set; }

        [BsonElement("bookStatusId")]
        public Guid BookStatusId { get; set; }
    }
}
