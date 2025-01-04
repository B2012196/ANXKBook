namespace UserService.API.Features.Login
{
    public record LoginCommand(string UserName, string Password) : ICommand<LoginResult>;
    public record LoginResult(bool IsSuccess, TokenResponse TokenResponse);
    public class LoginHandler(ApplicationDbContext context) : ICommandHandler<LoginCommand, LoginResult>
    {
        public async Task<LoginResult> Handle(LoginCommand command, CancellationToken cancellationToken)
        {
            var user = await context.Users.SingleOrDefaultAsync(u => u.UserName == command.UserName);
            if(user != null)
            {
                var role = await context.Roles.SingleOrDefaultAsync(r => r.RoleId == user.RoleId)
                ?? throw new RoleNotFoundException(user.RoleId);

                bool IsSuccess = BCrypt.Net.BCrypt.Verify(command.Password, user.Password);

                if (IsSuccess)
                {
                    var tokenResponse = await GenerateJwtToken(user, role.RoleName);
                    return new LoginResult(true, tokenResponse);
                }
            }

            return new LoginResult(false, null);
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
