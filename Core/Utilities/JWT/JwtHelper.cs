using Core.Utilities.Encryption;
using Microsoft.IdentityModel.Tokens;
using Core.Base.Entities;
using System.IdentityModel.Tokens.Jwt;


namespace Core.Utilities.JWT
{
    public class JwtHelper : ITokenHelper
    {
        private readonly TokenOptions tokenOptions;

        public JwtHelper(TokenOptions tokenOptions)
        {
            this.tokenOptions = tokenOptions;
        }

        public AccessToken CreateToken(BaseEntity user)
        {
            DateTime expirationTime = DateTime.Now.AddMinutes(tokenOptions.ExpirationTime);
            SecurityKey key = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey);

            SigningCredentials signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            JwtSecurityToken jwt = new JwtSecurityToken(
                issuer: tokenOptions.Issuer,
                audience: tokenOptions.Audience,
                claims: null,
                notBefore: DateTime.Now,
                expires: expirationTime,
                signingCredentials: signingCredentials
                );

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new();
            string jwtToken = jwtSecurityTokenHandler.WriteToken(jwt);
            return new AccessToken() { Token = jwtToken, ExpirationTime = expirationTime };
        }
    }
}
