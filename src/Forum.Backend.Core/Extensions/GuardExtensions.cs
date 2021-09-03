using Ardalis.GuardClauses;
using Forum.Backend.Core.Entities.UserAggregate;
using Forum.Backend.Core.Exceptions;

namespace Forum.Backend.Core.Extensions
{
    public static class GuardExtensions
    {
        public static void NullUser(this IGuardClause guardClause, User user)
        {
            if (user == null)
                throw new UserNotFoundException();
        }
    }
}
