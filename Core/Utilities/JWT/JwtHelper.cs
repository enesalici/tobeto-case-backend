using Core.Utilities.Encryption;
using Microsoft.IdentityModel.Tokens;
using Core.Base.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;


namespace Core.Utilities.JWT
{
    public class JwtHelper : ITokenHelper
    {
        private readonly TokenOptions tokenOptions;

        public JwtHelper(TokenOptions tokenOptions)
        {
            this.tokenOptions = tokenOptions;
        }

        public AccessToken CreateToken(BaseEntity<Guid> entity)
        {
            DateTime expirationTime = DateTime.Now.AddMinutes(tokenOptions.ExpirationTime);
            SecurityKey key = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey);

            SigningCredentials signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var claims = new[]
                       {
                new Claim(ClaimTypes.NameIdentifier, entity.ID.ToString()), 
            };

            JwtSecurityToken jwt = new JwtSecurityToken(
                issuer: tokenOptions.Issuer,
                audience: tokenOptions.Audience,
                claims: claims,
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
