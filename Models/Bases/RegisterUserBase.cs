namespace Models.Bases
{
    public class RegisterUserBase : AuthBase
    {
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}