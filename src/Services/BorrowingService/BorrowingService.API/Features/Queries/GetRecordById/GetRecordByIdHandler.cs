
namespace BorrowingService.API.Features.Queries.GetRecordById
{
    public record GetRecordByIdQuery(Guid RecordId) : IQuery<GetRecordByIdResult>;
    public record GetRecordByIdResult(BorrowingRecord BorrowingRecord);
    public class GetRecordByIdHandler(IMongoDatabase mongo) : IQueryHandler<GetRecordByIdQuery, GetRecordByIdResult>
    {
        public async Task<GetRecordByIdResult> Handle(GetRecordByIdQuery query, CancellationToken cancellationToken)
        {
            var collection = mongo.GetCollection<BorrowingRecord>("BorrowingRecords");

            var filter = Builders<BorrowingRecord>.Filter.Eq(record => record.RecordId, query.RecordId);

            var record = await collection.Find(filter).SingleOrDefaultAsync(cancellationToken);

            return new GetRecordByIdResult(record);
        }
    }
}
