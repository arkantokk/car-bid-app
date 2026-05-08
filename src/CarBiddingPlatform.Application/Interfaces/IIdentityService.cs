using CarBiddingPlatform.Application.DTOs;

namespace CarBiddingPlatform.Application.Interfaces;

public interface IIdentityService
{
    Task<AuthResponse> Register(string email, string password);
    Task<AuthResponse> Login(string email, string password);
}