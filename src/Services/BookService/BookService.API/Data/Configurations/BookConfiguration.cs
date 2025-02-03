namespace BookService.API.Data.Configurations
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(b => b.BookId);

            builder.Property(b => b.Title).IsRequired();
            builder.Property(b => b.Author);
            builder.Property(b => b.PublishingHouse);
            builder.Property(b => b.PublicationYear);
            builder.Property(b => b.Quatity).IsRequired();
            
            //book - genre
            builder.HasOne(b => b.Genre)
                .WithMany(g => g.Books)
                .HasForeignKey(b => b.GenreId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
