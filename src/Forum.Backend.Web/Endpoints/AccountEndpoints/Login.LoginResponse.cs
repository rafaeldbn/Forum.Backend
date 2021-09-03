using System;

namespace Forum.Backend.Web.Endpoints.AccountEndpoints
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public DateTime ExpireAt { get; set; }
    }
}
