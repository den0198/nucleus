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
            var identity = getIdentity(accountBase);
            var token = buildAndGetJwt(identity, authOptions);
            
            return new TokenBase
            {
                Token = token
            };
        }
        
        private static ClaimsIdentity getIdentity(AccountBase account)
        {
            var claims = new List<Claim>
            {
                new(ClaimsIdentity.DefaultNameClaimType, account.Login),
                new(ClaimsIdentity.DefaultRoleClaimType, account.Role)
            };
            
            return new ClaimsIdentity(claims, 
                "Token",
                ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
        }

        private static string buildAndGetJwt(ClaimsIdentity identity, AuthOptions authOptions)
        {
            var jwt = new JwtSecurityToken(
                issuer: authOptions.Issuer,
                audience: authOptions.Audience,
                claims: identity.Claims,
                expires: DateTime.Now.AddMinutes(authOptions.Lifetime),
                signingCredentials: AuthHelper.GetSigningCredentials(authOptions.Key)
            );
            
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
        
        #endregion
    }
}