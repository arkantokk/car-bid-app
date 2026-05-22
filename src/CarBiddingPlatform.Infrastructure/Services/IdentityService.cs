using CarBiddingPlatform.Application.Interfaces;
using Microsoft.AspNetCore.Identity;
using IdentityResult = CarBiddingPlatform.Application.DTOs.IdentityResult;

namespace CarBiddingPlatform.Infrastructure.Services;

public class IdentityService : IIdentityService
{
    private readonly UserManager<IdentityUser> _userManager;

    public IdentityService(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<IdentityResult> RegisterAsync(string email, string password, string userName)
    {
        var candidate = await _userManager.FindByEmailAsync(email);
        if (candidate != null)
            return new IdentityResult(false, null, null, null, ["User with this email already exists."]);
        var user = new IdentityUser
        {
            Email = email,
            UserName = userName,
        };
        var result = await _userManager.CreateAsync(user, password);

        if (result.Succeeded)
        {
            return new IdentityResult
                (true, user.Id, user.Email, user.UserName, []);
        }

        var errors = result.Errors.Select(e => e.Description).ToArray();
        return new IdentityResult(false, "", "", "", errors);
    }

    public async Task<IdentityResult> LoginAsync(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null) return new IdentityResult(false, "", "", "",["There is no such user"]);
        var login = await _userManager.CheckPasswordAsync(user, password);
        if (!login) return new IdentityResult(false, "", "", "",["Wrong password"]);
        return new IdentityResult(true, user.Id, email, user.UserName, []);
    }
}