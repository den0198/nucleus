using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Components.HelpersClasses;
using Models.AppSettings;
using Models.Bases;

namespace BusinessLogic.Handlers
{
    public class AccountHandler
    {
        #region SignIn
        public TokenBase GetSignInResponse(AccountBase accountBase, AuthOptions authOptions)
        {
            var claims = getClaims(accountBase);
            var token = buildAndGetJwt(claims, authOptions);
            
            return new TokenBase
            {
                Token = token
            };
        }

        private static IEnumerable<Claim> getClaims(AccountBase accountBase) =>
            new List<Claim>()
            {
                new(ClaimTypes.Role, accountBase.Role)
            };

        private static string buildAndGetJwt(IEnumerable<Claim> claims, AuthOptions authOptions)
        {
            var jwt = new JwtSecurityToken(
                authOptions.Issuer,
                authOptions.Audience,
                expires: DateTime.Now.AddMinutes(authOptions.Lifetime),
                signingCredentials: AuthHelper.GetSigningCredentials(authOptions.Key),
                claims: claims
            );
            
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
        
        #endregion
    }
}