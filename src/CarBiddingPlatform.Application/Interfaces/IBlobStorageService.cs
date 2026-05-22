namespace CarBiddingPlatform.Application.Interfaces;

public interface IBlobStorageService
{
    Task<string> SaveImageAsync(Stream imageStream, string fileName);
}