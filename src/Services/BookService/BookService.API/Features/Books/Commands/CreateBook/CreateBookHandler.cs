namespace BookService.API.Features.Books.Commands.CreateBook
{
    public record CreateBookCommand(Guid GenreId, string Title, string? Author,
        string? PublishingHouse, int? PublicationYear, int Quatity) : ICommand<CreateBookResult>;
    public record CreateBookResult(Guid BookId);

    public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
    {
        public CreateBookCommandValidator()
        {
            RuleFor(x => x.GenreId).NotEmpty().WithMessage("GenreId is required.")
                .NotEqual(Guid.Empty).WithMessage("GenreId cannot be an empty GUID."); 

            RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required.")
                .MaximumLength(50).WithMessage("Title cannot exceed 50 characters."); 

            RuleFor(x => x.Author).MaximumLength(100).WithMessage("Author cannot exceed 100 characters."); 

            RuleFor(x => x.PublishingHouse).MaximumLength(50).WithMessage("PublishingHouse cannot exceed 50 characters.");

            RuleFor(x => x.PublicationYear).InclusiveBetween(1900, DateTime.Now.Year)
                .WithMessage("PublicationYear must be between 1900 and the current year.");

            RuleFor(x => x.Quatity).InclusiveBetween(1, 100)
                .WithMessage("Quatity must be between 1 and 100");
        }
    }
    public class CreateBookHandler
        (IBookRepository bookRepository) 
        : ICommandHandler<CreateBookCommand, CreateBookResult>
    {
        public async Task<CreateBookResult> Handle(CreateBookCommand command, CancellationToken cancellationToken)
        {

            var book = new Book
            {
                BookId = Guid.NewGuid(),
                GenreId = command.GenreId,
                Title = command.Title,
                Author = command.Author,
                PublishingHouse = command.PublishingHouse,
                PublicationYear = command.PublicationYear,
                Quatity = command.Quatity
            };

            await bookRepository.CreateBook(book, cancellationToken);

            return new CreateBookResult(book.BookId);
        }
    }
}
