using Microsoft.AspNetCore.Mvc;

namespace newmark_api.Controllers;

[ApiController]
[Route("[controller]")]
public class PropertyController : ControllerBase
{
    private  readonly ILogger<PropertyController> _logger;
    private readonly IStorage _storageApi;

    public PropertyController(ILogger<PropertyController> logger, IStorage storageApi)
    {
        _logger = logger;
        _storageApi = storageApi;
    }

    [HttpGet(Name = "")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<List<Property>> GetProperties()
    {
        var result = await _storageApi.ReadDataAsync();
        return result;
    }
}
