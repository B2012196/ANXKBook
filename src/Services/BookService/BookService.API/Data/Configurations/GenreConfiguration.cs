
namespace BookService.API.Data.Configurations
{
    public class GenreConfiguration : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.HasKey(g => g.GenreId);

            builder.Property(g => g.GenreName).IsRequired();    
        }
    }
}
