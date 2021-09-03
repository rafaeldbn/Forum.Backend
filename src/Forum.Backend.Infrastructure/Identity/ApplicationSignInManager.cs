using Forum.Backend.Core.Entities.UserAggregate;
using Forum.Backend.Infrastructure.Identity.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Forum.Backend.Infrastructure.Identity
{
    public class ApplicationSignInManager : IApplicationSignInManager
    {
        public (string Token, DateTime ExpireAt) GenerateTokenAndSetIdentity(User user, SigningConfigurations signingConfigurations, TokenConfigurations tokenConfigurations)
        {
            ClaimsIdentity identity = new ClaimsIdentity(
                    new[] {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                        new Claim(JwtRegisteredClaimNames.UniqueName, user.Id.ToString())
                    }
                );

            DateTime creationDate = DateTime.Now;
            DateTime expireDate = creationDate + TimeSpan.FromSeconds(tokenConfigurations.Seconds);

            var handler = new JwtSecurityTokenHandler();
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = tokenConfigurations.Issuer,
                Audience = tokenConfigurations.Audience,
                SigningCredentials = signingConfigurations.SigningCredentials,
                Subject = identity,
                NotBefore = creationDate,
                Expires = expireDate
            });

            return (handler.WriteToken(securityToken), expireDate);
        }
    }
}
