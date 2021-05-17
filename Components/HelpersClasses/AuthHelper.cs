using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Components.HelpersClasses
{
    public static class AuthHelper
    {
        public static SigningCredentials GetSigningCredentials(string key) =>
            new(GetIssuerSigningKey(key),
                SecurityAlgorithms.HmacSha256);
        public static SymmetricSecurityKey GetIssuerSigningKey(string key) =>
            new(Encoding
                .UTF8
                .GetBytes(key));
    }
}