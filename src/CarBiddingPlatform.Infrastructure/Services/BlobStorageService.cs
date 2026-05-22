using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using CarBiddingPlatform.Application.Interfaces;
using Microsoft.Extensions.Configuration;

namespace CarBiddingPlatform.Infrastructure.Services;

public class BlobStorageService : IBlobStorageService
{
    private readonly IConfiguration _configuration;

    public BlobStorageService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    

    public async Task<string> SaveImageAsync(Stream imageStream, string fileName)
    {
        var blobServiceClient = new BlobServiceClient(_configuration.GetConnectionString("BlobStorage"));
        var containerClient = blobServiceClient.GetBlobContainerClient("car-images");
        await containerClient.CreateIfNotExistsAsync(PublicAccessType.Blob);
        var fileExtension = Path.GetExtension(fileName);
        var name = Guid.NewGuid() + fileExtension;
        var blobClient = containerClient.GetBlobClient(name);
        await blobClient.UploadAsync(imageStream);
        return blobClient.Uri.ToString();
    }
    
}