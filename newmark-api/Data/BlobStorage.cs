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

    public async Task<List<Property>> ReadDataAsync()
    {
        var stsBase64 = _configuration["AppSettings:STS"];
        var bytes  = Convert.FromBase64String(stsBase64);
        var sts = Encoding.UTF8.GetString(bytes);
        
        try 
        {
            string url = new StringBuilder(_configuration["AppSettings:BlobClientServiceName"])
                                .Append(_configuration["AppSettings:BlobContainerName"])
                                .Append(_configuration["AppSettings:BlobName"])
                                .Append(sts)
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
