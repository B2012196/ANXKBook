namespace BookWeb.Services
{
    public interface IBookService
    {
        [Get("/books/books")]
        Task<GetBooksResponse> GetBooks(int pageNumber, int pageSize, Guid filterGenreId);

        [Get("/books/books/available/{BookId}")]
        Task<CheckAvaResponse> CheckAvaBook(Guid BookId);

        [Get("/books/genres")]
        Task<GetGenresResponse> GetGenres();
    }
}
