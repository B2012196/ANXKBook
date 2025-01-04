
namespace BorrowingService.API.Features.Queries.GetRecords
{
    public record GetRecordsQuery() : IQuery<GetRecordsResult>;
    public record GetRecordsResult(IEnumerable<BorrowingRecord> BorrowingRecords);
    public class GetRecordsHandler(IMongoDatabase mongo) : IQueryHandler<GetRecordsQuery, GetRecordsResult>
    {
        public async Task<GetRecordsResult> Handle(GetRecordsQuery query, CancellationToken cancellationToken)
        {
            var collection = mongo.GetCollection<BorrowingRecord>("BorrowingRecords");

            var records = await collection.Find(record => true).ToListAsync(cancellationToken);

            return new GetRecordsResult(records);
        }
    }
}
