
namespace UserService.API.Data.Configurations
{
    public class TokenConfiguration : IEntityTypeConfiguration<Token>
    {
        public void Configure(EntityTypeBuilder<Token> builder)
        {
            builder.HasKey(t => t.TokenId);

            builder.Property(t => t.UserId).IsRequired();

            builder.Property(t => t.AccessToken).IsRequired();

            builder.Property(t => t.RefreshToken).IsRequired();

            builder.Property(t => t.IssuedAt).IsRequired();

            builder.Property(t => t.ExpiresAt).IsRequired();

            builder.Property(t => t.IsRevoked).IsRequired();

            //token - user
            builder.HasOne(t => t.User)
                .WithMany(u => u.Tokens)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
