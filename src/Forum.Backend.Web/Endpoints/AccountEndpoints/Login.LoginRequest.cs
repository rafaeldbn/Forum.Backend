using System.ComponentModel.DataAnnotations;

namespace Forum.Backend.Web.Endpoints.AccountEndpoints
{
    public class LoginRequest
    {
        public const string Route = "/Accounts";

        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
