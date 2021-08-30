using Ardalis.Specification;
using System.Linq;

namespace Forum.Backend.Core.Entities.UserAggregate.Specifications
{
    public class UserByIdSpec : Specification<User>, ISingleResultSpecification
    {
        public UserByIdSpec(int userId)
        {
            Query
                .Where(user => user.Id == userId);
        }
    }
}
