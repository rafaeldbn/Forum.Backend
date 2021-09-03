using Ardalis.Specification;
using System.Linq;

namespace Forum.Backend.Core.Entities.UserAggregate.Specifications
{
    public class UserByEmailSpec : Specification<User>, ISingleResultSpecification
    {
        public UserByEmailSpec(string email)
        {
            Query
                .Where(user => user.Email == email);
        }
    }
}
