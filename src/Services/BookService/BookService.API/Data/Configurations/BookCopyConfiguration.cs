
namespace BookService.API.Data.Configurations
{
    public class BookCopyConfiguration : IEntityTypeConfiguration<BookCopy>
    {
        public void Configure(EntityTypeBuilder<BookCopy> builder)
        {
            builder.HasKey(bc => bc.BookCopyId);

            //bookcopy - bookstatus
            builder.HasOne(bc => bc.BookStatus)
                .WithMany(s => s.BookCopys)
                .HasForeignKey(bc => bc.BookStatusId)
                .OnDelete(DeleteBehavior.Restrict);

            //bookcopy - book
            builder.HasOne(bc => bc.Book)
                .WithMany(b => b.BookCopys)
                .HasForeignKey(bc => bc.BookId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
