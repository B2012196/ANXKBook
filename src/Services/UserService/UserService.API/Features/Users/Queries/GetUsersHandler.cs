
namespace UserService.API.Features.Users.Queries
{
    public record GetUsersQuery(int? pageNumber = 1, int? pageSize = 10) : IQuery<GetUsersResult>;
    public record GetUsersResult(IEnumerable<UserDTO> UserDTOs, int totalCount);
    public class GetUsersHandler(ApplicationDbContext context) : IQueryHandler<GetUsersQuery, GetUsersResult>
    {
        public async Task<GetUsersResult> Handle(GetUsersQuery query, CancellationToken cancellationToken)
        {

            var users = context.Users.AsNoTracking();

            users = users.OrderBy(r => r.UserId);

            int totalCount = await users.CountAsync(cancellationToken);

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

            return new GetUsersResult(await userDTOs.ToListAsync(cancellationToken), totalCount);

        }
    }
}
