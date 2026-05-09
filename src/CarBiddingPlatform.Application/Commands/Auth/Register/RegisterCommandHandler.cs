using CarBiddingPlatform.Application.DTOs;
using CarBiddingPlatform.Application.Interfaces;
using MediatR;

namespace CarBiddingPlatform.Application.Commands.Auth.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AuthResponse>
{
    private readonly IIdentityService _identityService;
    private readonly ITokenService _tokenService;
    
    public RegisterCommandHandler(IIdentityService identityService, ITokenService tokenService)
    {
        _identityService = identityService;
        _tokenService = tokenService;
    }
    

    public async Task<AuthResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var user = await _identityService.RegisterAsync(request.Email, request.Password);
        if (!user.IsSuccess) return new AuthResponse("", user.Errors);
        var token = _tokenService.GenerateToken(new TokenUserInfo(user.Email!, user.UserId!));
        return new AuthResponse(token, []);
    }
}