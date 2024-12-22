namespace BookService.API.Exceptions
{
    public class StatusNotFoundException : NotFoundException
    {
        public StatusNotFoundException(Guid Id) : base("BookStatus", Id)
        {

        }
    }
}
