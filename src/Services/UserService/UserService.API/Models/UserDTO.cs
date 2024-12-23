namespace UserService.API.Models
{
    public class UserDTO
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}
