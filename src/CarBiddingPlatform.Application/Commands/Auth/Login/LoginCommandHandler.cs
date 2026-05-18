using CarBiddingPlatform.Application.DTOs;
using CarBiddingPlatform.Application.Interfaces;
using MediatR;

namespace CarBiddingPlatform.Application.Commands.Auth.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthResponse>
{
    private readonly IIdentityService _identityService;
    private readonly ITokenService _tokenService;
    
    public LoginCommandHandler(IIdentityService identityService, ITokenService tokenService)
    {
        _identityService = identityService;
        _tokenService = tokenService;
    }

    public async Task<AuthResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _identityService.LoginAsync(request.Email, request.Password);
        if (!user.IsSuccess) return new AuthResponse("", user.Errors);
        var token = _tokenService.GenerateToken(new TokenUserInfo(user.Email!, user.UserId!, user.UserName!));
        return new AuthResponse(token, []);
    }
}