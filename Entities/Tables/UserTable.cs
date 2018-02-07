namespace Entities.Tables
{
    public class UserTable  
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public Enums.Role Role { get; set; }
    }
}
