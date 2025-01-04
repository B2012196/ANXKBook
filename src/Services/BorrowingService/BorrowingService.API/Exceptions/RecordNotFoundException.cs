namespace BorrowingService.API.Exceptions
{
    public class RecordNotFoundException(Guid Id) : NotFoundException("Record", Id)
    {

    }
}
