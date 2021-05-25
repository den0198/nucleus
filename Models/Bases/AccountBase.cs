namespace Models.Bases
{
    public class AccountBase
    {
        public string Login { get; set; }
        public string Password { get; set; }
        
        public string RefreshToken { get; set; }
        public string Role { get; set; }
    }
}