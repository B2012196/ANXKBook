namespace BookService.API.Features.Genres.Commands.DeleteGenre
{
    public record DeleteGenreCommand(Guid GenreId) : ICommand<DeleteGenreResult>;
    public record DeleteGenreResult(bool IsSuccess);
    public class DeleteGenreCommandValidator : AbstractValidator<DeleteGenreCommand>
    {
        public DeleteGenreCommandValidator()
        {
            RuleFor(x => x.GenreId).NotEmpty().WithMessage("GenreId is required.")
                .NotEqual(Guid.Empty).WithMessage("GenreId cannot be an empty GUID."); ;
        }
    }
    public class DeleteGenreHandler(ApplicationDbContext context) : ICommandHandler<DeleteGenreCommand, DeleteGenreResult>
    {
        public async Task<DeleteGenreResult> Handle(DeleteGenreCommand command, CancellationToken cancellationToken)
        {
            var genre = await context.Genres.SingleOrDefaultAsync(g => g.GenreId == command.GenreId, cancellationToken);
            if (genre == null)
            {
                throw new GenreNotFoundException(command.GenreId);
            }

            context.Genres.Remove(genre);
            await context.SaveChangesAsync(cancellationToken);

            return new DeleteGenreResult(true);
        }
    }
}
