﻿namespace Identity.Microservice.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = "";
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
        public bool IsActive { get; set; } = true;

        public Profile? Profile { get; set; }
        public ICollection<UserRole> UserRoles { get; set; } = [];
    }
}
