using Forum.Backend.Core.Entities.UserAggregate;
using System;

namespace Forum.Backend.Infrastructure.Identity.Interfaces
{
    public interface IApplicationSignInManager
    {
        (string Token, DateTime ExpireAt) GenerateTokenAndSetIdentity(User user, SigningConfigurations signingConfigurations, TokenConfigurations tokenConfigurations);
    }
}
