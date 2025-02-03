namespace BookService.API.Features.Genres.Commands.UpdateGenre
{
    public record UpdateGenreCommand(Guid GenreId, string GenreName) : ICommand<UpdateGenreResult>;
    public record UpdateGenreResult(bool IsSuccess);
    public class UpdateGenreCommandValidator : AbstractValidator<UpdateGenreCommand>
    {
        public UpdateGenreCommandValidator()
        {
            RuleFor(x => x.GenreId).NotEmpty().WithMessage("GenreId is required.")
                .NotEqual(Guid.Empty).WithMessage("GenreId cannot be an empty GUID."); 

            RuleFor(x => x.GenreName).NotEmpty().WithMessage("GenreName is required.")
                .MaximumLength(50).WithMessage("GenreName cannot exceed 50 characters.");
        }
    }
    public class UpdateGenreHandler(ApplicationDbContext context) : ICommandHandler<UpdateGenreCommand, UpdateGenreResult>
    {
        public async Task<UpdateGenreResult> Handle(UpdateGenreCommand command, CancellationToken cancellationToken)
        {
            var genre = await context.Genres.SingleOrDefaultAsync(g => g.GenreId == command.GenreId ,cancellationToken);

            if (genre == null)
            {
                throw new GenreNotFoundException(command.GenreId);
            }

            genre.GenreName = command.GenreName;

            context.Genres.Update(genre);
            await context.SaveChangesAsync(cancellationToken);

            return new UpdateGenreResult(true);
        }
    }
}
