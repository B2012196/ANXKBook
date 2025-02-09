﻿namespace UserService.API.Models
{
    public class User
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        [JsonIgnore]
        public Role Role { get; set; }
        [JsonIgnore]
        public ICollection<Token> Tokens { get; set; }
    }
}
