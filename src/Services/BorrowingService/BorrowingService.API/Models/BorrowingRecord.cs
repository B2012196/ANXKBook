namespace BorrowingService.API.Models
{
    public class BorrowingRecord
    {
        [BsonId]
        [BsonGuidRepresentation(MongoDB.Bson.GuidRepresentation.Standard)]
        public Guid RecordId { get; set; }

        [BsonElement("fullName")]
        public string FullName { get; set; }

        [BsonElement("books")]
        public List<BorrowedBook> Books { get; set; }

        [BsonElement("borrowDate")]
        public DateTime? BorrowDate { get; set; }

        [BsonElement("returnDate")]
        public DateTime? ReturnDate { get; set; }

        [BsonElement("status")]
        public BorrowingStatus Status { get; set; } 
    }
}
