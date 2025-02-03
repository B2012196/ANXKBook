
namespace BookService.API.Features.Books.Commands.UpdateBook
{
    public record UpdateBookCommand(Guid BookId, Guid GenreId, string Title, string? Author,
        string? PublishingHouse, int? PublicationYear, int Quatity) : ICommand<UpdateBookResult>;
    public record UpdateBookResult(bool IsSuccess);
    public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
    {
        public UpdateBookCommandValidator()
        {
            RuleFor(x => x.BookId).NotEmpty().WithMessage("BookId is required.")
                .NotEqual(Guid.Empty).WithMessage("BookId cannot be an empty GUID.");

            RuleFor(x => x.GenreId).NotEmpty().WithMessage("GenreId is required.")
                .NotEqual(Guid.Empty).WithMessage("GenreId cannot be an empty GUID.");

            RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required.")
                .MaximumLength(50).WithMessage("Title cannot exceed 50 characters.");

            RuleFor(x => x.Author).NotEmpty().WithMessage("Author is required.")
                .MaximumLength(100).WithMessage("Author cannot exceed 100 characters.");

            RuleFor(x => x.PublishingHouse).MaximumLength(50).WithMessage("PublishingHouse cannot exceed 50 characters.");

            RuleFor(x => x.PublicationYear).InclusiveBetween(1900, DateTime.Now.Year)
                .WithMessage("PublicationYear must be between 1900 and the current year.");

            RuleFor(x => x.Quatity).InclusiveBetween(1, 100)
                .WithMessage("Quatity must be between 1 and 100");
        }
    }
    public class UpdateBookHandler(ApplicationDbContext context) : ICommandHandler<UpdateBookCommand, UpdateBookResult>
    {
        public async Task<UpdateBookResult> Handle(UpdateBookCommand command, CancellationToken cancellationToken)
        {
            var book = await context.Books.SingleOrDefaultAsync(b => b.BookId == command.BookId, cancellationToken);
            if(book == null)
            {
                throw new BookNotFoundException(command.BookId);
            }

            book.GenreId = command.GenreId;
            book.Title = command.Title;
            book.Author = command.Author;
            book.PublishingHouse = command.PublishingHouse;
            book.PublicationYear = command.PublicationYear;
            book.Quatity = command.Quatity;

            context.Books.Update(book);
            await context.SaveChangesAsync(cancellationToken);

            return new UpdateBookResult(true);
        }
    }
}
