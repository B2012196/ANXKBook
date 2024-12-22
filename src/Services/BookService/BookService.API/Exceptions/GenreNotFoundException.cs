namespace BookService.API.Exceptions
{
    public class GenreNotFoundException : NotFoundException
    {
        public GenreNotFoundException(Guid Id) : base("Genre", Id)
        {

        }
    }
}
