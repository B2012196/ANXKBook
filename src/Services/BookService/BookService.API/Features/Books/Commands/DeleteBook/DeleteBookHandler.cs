
namespace BookService.API.Features.Books.Commands.DeleteBook
{
    public record DeleteBookCommand(Guid BookId) : ICommand<DeleteBookResult>;
    public record DeleteBookResult(bool IsSuccess);
    public class DeleteBookCommandValidator : AbstractValidator<DeleteBookCommand>
    {
        public DeleteBookCommandValidator()
        {
            RuleFor(x => x.BookId).NotEmpty().WithMessage("BookId is required.")
                .NotEqual(Guid.Empty).WithMessage("BookId cannot be an empty GUID.");
        }
    }
    public class DeleteBookHandler(ApplicationDbContext context) : ICommandHandler<DeleteBookCommand, DeleteBookResult>
    {
        public async Task<DeleteBookResult> Handle(DeleteBookCommand command, CancellationToken cancellationToken)
        {
            var book = await context.Books.FirstOrDefaultAsync(b => b.BookId == command.BookId, cancellationToken);

            if (book == null) 
            {
                throw new BookNotFoundException(command.BookId);
            }

            context.Books.Remove(book); 
            await context.SaveChangesAsync(cancellationToken);

            return new DeleteBookResult(true);
        }
    }
}
