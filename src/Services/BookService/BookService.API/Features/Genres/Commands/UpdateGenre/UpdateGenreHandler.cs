namespace BookService.API.Features.Genres.Commands.UpdateGenre
{
    public record UpdateGenreCommand(Guid GenreId, string GenreName) : ICommand<UpdateGenreResult>;
    public record UpdateGenreResult(bool IsSuccess);
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
