using CarBiddingPlatform.Application.DTOs;

namespace CarBiddingPlatform.Application.Interfaces;

public interface ITokenService
{
    string GenerateToken(TokenUserInfo data);
}