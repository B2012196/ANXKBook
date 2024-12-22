namespace BookService.API.Data.Configurations
{
    public class StatusConfiguration : IEntityTypeConfiguration<Status>
    {
        public void Configure(EntityTypeBuilder<Status> builder)
        {
            builder.HasKey(s => s.StatusId);

            builder.Property(s => s.StatusName).IsRequired();   
        }
    }
}
