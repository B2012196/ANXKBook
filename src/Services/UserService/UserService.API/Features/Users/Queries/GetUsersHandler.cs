namespace UserService.API.Features.Users.Queries
{
    public record GetUsersQuery(int? pageNumber = 1, int? pageSize = 10) : IQuery<GetUsersResult>;
    public record GetUsersResult(IEnumerable<UserDTO> Users, int TotalCount);
    public class GetUsersHandler(ApplicationDbContext context, ILogger<GetUsersHandler> logger) : IQueryHandler<GetUsersQuery, GetUsersResult>
    {
        public async Task<GetUsersResult> Handle(GetUsersQuery query, CancellationToken cancellationToken)
        {
            var users = context.Users.AsNoTracking();

            users = users.OrderBy(r => r.UserId);

            int totalCount = await users.CountAsync(cancellationToken);
            logger.LogWarning(totalCount + "");

            if (query.pageNumber.HasValue && query.pageSize.HasValue)
            {
                int skip = (query.pageNumber.Value - 1) * query.pageSize.Value;
                users = users.Skip(skip).Take(query.pageSize.Value);
            }

            
            var userDTOs = users.Select(r => new UserDTO
            {
                UserId = r.UserId,
                RoleId = r.RoleId,
                UserName = r.UserName,
                Email = r.Email
            });

            foreach (var user in userDTOs)
            {
                logger.LogWarning("ID: " + user.UserId);
            }

            return new GetUsersResult(await userDTOs.ToListAsync(cancellationToken), totalCount);

        }
    }
}
