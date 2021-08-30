using System.ComponentModel.DataAnnotations;

namespace Forum.Backend.Web.Endpoints.UserEndpoints
{
    public class CreateUserRequest
    {
        public const string Route = "/Users";

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string TimeZone { get; set; }
    }
}