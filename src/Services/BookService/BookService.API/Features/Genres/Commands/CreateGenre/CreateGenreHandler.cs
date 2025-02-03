namespace BookService.API.Features.Genres.Commands.CreateGenre
{
    public record CreateGenreCommand(string GenreName) : ICommand<CreateGenreResult>;
    public record CreateGenreResult(Guid GenreId);
    public class CreateGenreCommandValidator : AbstractValidator<CreateGenreCommand>
    {
        public CreateGenreCommandValidator()
        {
            RuleFor(x => x.GenreName).NotEmpty().WithMessage("GenreName is required.")
                .MaximumLength(50).WithMessage("GenreName cannot exceed 50 characters.");
        }
    }
    public class CreateGenreHandler(ApplicationDbContext context) : ICommandHandler<CreateGenreCommand, CreateGenreResult>
    {
        public async Task<CreateGenreResult> Handle(CreateGenreCommand command, CancellationToken cancellationToken)
        {
            var genre = new Genre
            {
                GenreId = Guid.NewGuid(),
                GenreName = command.GenreName,
            };

            context.Genres.Add(genre);
            await context.SaveChangesAsync(cancellationToken);

            return new CreateGenreResult(genre.GenreId);

        }
    }
}
