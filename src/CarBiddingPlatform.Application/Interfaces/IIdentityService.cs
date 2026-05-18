using CarBiddingPlatform.Application.DTOs;

namespace CarBiddingPlatform.Application.Interfaces;

public interface IIdentityService
{
    Task<IdentityResult> RegisterAsync(string email, string password, string userName);
    Task<IdentityResult> LoginAsync(string email, string password);
}