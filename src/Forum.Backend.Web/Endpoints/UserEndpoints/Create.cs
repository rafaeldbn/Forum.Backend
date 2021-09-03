using Ardalis.ApiEndpoints;
using Forum.Backend.Core.Exceptions;
using Forum.Backend.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading;
using System.Threading.Tasks;

namespace Forum.Backend.Web.Endpoints.UserEndpoints
{
    public class Create : BaseAsyncEndpoint
        .WithRequest<CreateUserRequest>
        .WithResponse<CreateUserResponse>
    {
        private readonly IUserService _userService;

        public Create(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("/Users")]
        [SwaggerOperation(
            Summary = "Creates a new User",
            Description = "Creates a new User",
            OperationId = "User.Create",
            Tags = new[] { "UsersEndpoint" })
        ]
        public override async Task<ActionResult<CreateUserResponse>> HandleAsync(CreateUserRequest request,
            CancellationToken cancellationToken)
        {

            try
            {
                var user = await _userService.AddNewUserAsync(request.Name, request.Email, request.Password, request.TimeZone, cancellationToken);

                return Ok(new CreateUserResponse
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    TimeZone = user.TimeZone
                });
            }
            catch (DuplicateEmailException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
