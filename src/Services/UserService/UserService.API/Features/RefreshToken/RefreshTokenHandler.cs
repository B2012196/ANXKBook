
namespace UserService.API.Features.RefreshToken
{
    public record RefreshTokenCommand(string RefreshToken) : ICommand<RefreshTokenResult>;
    public record RefreshTokenResult(TokenResponse TokenResponse);
    public class RefreshTokenHandler(ApplicationDbContext context) : ICommandHandler<RefreshTokenCommand, RefreshTokenResult>
    {
        public async Task<RefreshTokenResult> Handle(RefreshTokenCommand command, CancellationToken cancellationToken)
        {
            var token = await context.Tokens.SingleOrDefaultAsync(t => t.RefreshToken == command.RefreshToken, cancellationToken);

            if (token == null || token.IsRevoked || token.ExpiresAt < DateTime.UtcNow)
            {
                return new RefreshTokenResult(null);
            }

            token.IsRevoked = true;

            context.Tokens.Update(token);
            await context.SaveChangesAsync(cancellationToken);   

            var user = await context.Users.SingleOrDefaultAsync(u => u.UserId == token.UserId, cancellationToken)
                ?? throw new UserNotFoundException(token.UserId);

            var role = await context.Roles.SingleOrDefaultAsync(r => r.RoleId == user.RoleId, cancellationToken)
                ?? throw new RoleNotFoundException(user.RoleId);

            var tokenResponse = await GenerateJwtToken(user, role.RoleName);

            return new RefreshTokenResult(tokenResponse);   

        }

        public async Task<TokenResponse> GenerateJwtToken(User user, string RoleName)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("YourSuperSecretKeyHotelwebsite14"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("userid", user.UserId.ToString()), // Claim userid (giả sử `user.Id` là Guid)
                new Claim("username", user.UserName), // Claim thêm thông tin username
                new Claim(ClaimTypes.Role, RoleName),
            };

            var token = new JwtSecurityToken(
                issuer: "https://localhost:5056",
                claims: claims,
                expires: DateTime.UtcNow.AddHours(10),
                signingCredentials: credentials);

            // Tạo token string
            var accessToken = new JwtSecurityTokenHandler().WriteToken(token);

            // Tạo refresh token
            var refreshToken = Guid.NewGuid().ToString();


            var tokenEntity = new Token
            {
                TokenId = Guid.NewGuid(),
                UserId = user.UserId,
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                IssuedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddDays(7),
                IsRevoked = false
            };

            context.Tokens.Add(tokenEntity);
            await context.SaveChangesAsync();


            // Trả về thông tin token cho người dùng
            var tokenResponse = new TokenResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                ExpiresAt = tokenEntity.ExpiresAt
            };

            //Console.WriteLine(tokens.Access_token);
            return tokenResponse;
        }
    }
}
