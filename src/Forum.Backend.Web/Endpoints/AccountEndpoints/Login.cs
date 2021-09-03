using Ardalis.ApiEndpoints;
using Forum.Backend.Core.Exceptions;
using Forum.Backend.Core.Interfaces;
using Forum.Backend.Infrastructure.Identity;
using Forum.Backend.Infrastructure.Identity.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Forum.Backend.Web.Endpoints.AccountEndpoints
{
    public class Login : BaseAsyncEndpoint
        .WithRequest<LoginRequest>
        .WithResponse<LoginResponse>
    {

        private readonly IUserService _userService;
        private readonly IApplicationSignInManager _signManager;

        public Login(IUserService userService, IApplicationSignInManager signManager)
        {
            _userService = userService;
            _signManager = signManager;
        }


        [HttpPost("/Accounts")]
        [SwaggerOperation(
            Summary = "Authenticate with e-mail and password",
            Description = "Authenticate with e-mail and password. Returns JWT token.",
            OperationId = "Account.Login",
            Tags = new[] { "AccountsEndpoints" })
        ]
        public override async Task<ActionResult<LoginResponse>> HandleAsync(LoginRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                var signinOptions = (SigningConfigurations)HttpContext.RequestServices.GetService(typeof(SigningConfigurations));
                var tokenConfigurations = (TokenConfigurations)HttpContext.RequestServices.GetService(typeof(TokenConfigurations));

                var user = await _userService.AuthenticationByEmailAndPasswordAsync(request.Email, request.Password, cancellationToken);
                var (token, expireAt) = _signManager.GenerateTokenAndSetIdentity(user, signinOptions, tokenConfigurations);

                return Ok(new LoginResponse
                {
                    ExpireAt = expireAt,
                    Token = token
                });
            }
            catch (Exception ex) when (ex is InvalidPasswordException || ex is UserNotFoundException)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
