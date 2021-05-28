using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Components.HelpersClasses;
using Microsoft.IdentityModel.Tokens;
using Models.AppSettings;

namespace BusinessLogic.Handlers
{
    public class AccountHandler
    {
        #region SignIn
        
        public string GetAccessToken(IEnumerable<Claim> claims, AuthOptions authOptions)
        {
            var token = buildAndGetJwt(claims, authOptions);

            return token;
        }
        
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
        
        #region NewToken

        public ClaimsPrincipal GetAccountInfoByOldToken(string oldAccessToken, AuthOptions authOptions)
        {
            var tokenValidationParameters = getTokenValidationParameters(authOptions);
            var jwtHandler = new JwtSecurityTokenHandler();
            var accountInfo = jwtHandler.ValidateToken(oldAccessToken,
                tokenValidationParameters, out var securityToken);

            return securityToken is JwtSecurityToken ? accountInfo : null;
        }

        private static TokenValidationParameters getTokenValidationParameters
            (AuthOptions authOptions) =>
                new()
                {
                    ValidIssuer = authOptions.Issuer,
                    ValidateIssuer = true,
                    ValidAudience = authOptions.Audience,
                    ValidateAudience = true,
                    IssuerSigningKey = AuthHelper.GetIssuerSigningKey(authOptions.Key),
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = false
                };

        #endregion
    }
}