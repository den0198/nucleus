using Models.Bases;

namespace Models.Requests
{
    public class RegistryUserRequest
    {
        public AccountBase Account { get; set; }
        public string Password { get; set; }
        public UserBase User { get; set; }
    }
}