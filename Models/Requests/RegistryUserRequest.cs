using Models.Bases;

namespace Models.Requests
{
    public class RegistryUserRequest
    {
        public RegisterUserBase Account { get; set; }
        
        public UserBase User { get; set; }
    }
}