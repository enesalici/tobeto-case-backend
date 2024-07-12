using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Core.Pipelines.Authorization;

public class AuthorizationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>, ISecuredRequest
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthorizationBehavior(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        //TODO: Implement Roles
        if (!_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            throw new Exception("Giriş yapmadınız.");

        
        TResponse response = await next();
        return response;
    }
}
