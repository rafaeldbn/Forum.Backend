namespace Forum.Backend.Web.Endpoints.UserEndpoints
{
    public class CreateUserResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string TimeZone { get; set; }
    }
}