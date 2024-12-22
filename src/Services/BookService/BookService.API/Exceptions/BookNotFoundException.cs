namespace BookService.API.Exceptions
{
    public class BookNotFoundException : NotFoundException
    {
        public BookNotFoundException(Guid Id) : base("Book", Id)
        {

        }
    }
}
