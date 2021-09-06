using Ardalis.ApiEndpoints;
using Forum.Backend.Core.Entities.UserAggregate;
using Forum.Backend.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading;
using System.Threading.Tasks;

namespace Forum.Backend.Web.Endpoints.UserEndpoints
{
    public class GetById : BaseAsyncEndpoint
        .WithRequest<GetUserByIdRequest>
        .WithResponse<GetUserByIdResponse>
    {
        private readonly IRepository<User> _repository;

        public GetById(IRepository<User> repository)
        {
            _repository = repository;
        }

        [Authorize("Bearer")]
        [HttpGet(GetUserByIdRequest.Route)]
        [SwaggerOperation(
            Summary = "Gets a single User",
            Description = "Gets a single User by Id",
            OperationId = "Users.GetById",
            Tags = new[] { "UsersEndpoint" })
        ]
        public override async Task<ActionResult<GetUserByIdResponse>> HandleAsync([FromRoute] GetUserByIdRequest request,
            CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.UserId, cancellationToken);
            if (entity == null) return NotFound();

            var response = new GetUserByIdResponse
            {
                Id = entity.Id,
                Name = entity.Name,
                Email = entity.Email
            };
            return Ok(response);
        }
    }
}
