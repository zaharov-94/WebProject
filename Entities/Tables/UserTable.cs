﻿namespace Entities.Tables
{
    public class UserTable  
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public Enums.Role Role { get; set; }
    }
}
