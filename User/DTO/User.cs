namespace User.DTO
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime LastUpdate { get; set; }
        public string Status { get; set; }
    }
}
