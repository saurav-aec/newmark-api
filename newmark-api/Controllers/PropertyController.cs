using Microsoft.AspNetCore.Mvc;

namespace newmark_api.Controllers;

[ApiController]
[Route("[controller]")]
public class PropertyController : ControllerBase
{
    private  readonly ILogger<PropertyController> _logger;
    private readonly IStorage _storageApi;
    private readonly IConfiguration _configuration;

    public PropertyController(ILogger<PropertyController> logger,
     IStorage storageApi, IConfiguration configuration)
    {
        _logger = logger;
        _storageApi = storageApi;
        _configuration = configuration;
    }

    [HttpGet]
    [Route("")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<List<Property>> GetProperties()
    {
        var result = await _storageApi.ReadDataAsync();
        return result;
    }

    [HttpGet]
    [Route("Base64")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<string> GetGet64String()
    {
        var bytes = System.Text.Encoding.UTF8.GetBytes(_configuration["AppSettings:STS"]);
        return Convert.ToBase64String(bytes);
    }



}
