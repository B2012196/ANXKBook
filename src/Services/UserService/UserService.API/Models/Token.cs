namespace UserService.API.Models
{
    public class Token
    {
        public Guid TokenId { get; set; }
        public Guid UserId { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime IssuedAt {  get; set; }
        public DateTime ExpiresAt { get; set; }
        public bool IsRevoked { get; set; }

        public User User { get; set; }
    }
}
