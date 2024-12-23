﻿namespace UserService.API.Exceptions
{
    public class RoleNotFoundException : NotFoundException
    {
        public RoleNotFoundException(Guid Id) : base("Role", Id)
        {

        }
    }
}