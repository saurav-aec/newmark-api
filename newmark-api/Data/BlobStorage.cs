using System;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System.Text;
using System.Text.Json;

class BlobStorage : IStorage
{
    private readonly ILogger<BlobStorage> _logger;
    private readonly IConfiguration _configuration;


    public BlobStorage(ILogger<BlobStorage> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    private readonly string blobServiceClientName = @"https://nmrkpidev.blob.core.windows.net";
    private readonly string blobContainerName = "/dev-test";
    private readonly string blobName = "/dev-test.json";
    private readonly string sts = "?sp=r&st=2024-10-28T10:35:48Z&se=2025-10-28T18:35:48Z&spr=https&sv=2022-11-02&sr=b&sig=bdeoPWtefikVgUGFCUs4ihsl22ZhQGu4%2B4cAfoMwd4k%3D";

    public async Task<List<Property>> ReadDataAsync()
    {
        
        try 
        {
            string url = new StringBuilder(_configuration["AppSettings:BlobClientServiceName"])
                                .Append(_configuration["AppSettings:BlobContainerName"])
                                .Append(_configuration["AppSettings:BlobName"])
                                .Append(_configuration["AppSettings:STS"])
                                .ToString();

            
            using(var client = new HttpClient())
            {
                var result = await client.GetStringAsync(url);
                return JsonSerializer.Deserialize<List<Property>>(result);  
            }
        }
        catch {
            _logger.LogInformation("Failed to fetch Property details");
            throw;
        }
    }
}
