using Microsoft.AspNetCore.Mvc;
using Famecipe.Services;

namespace Famecipe.Microservice.Metadata.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces(System.Net.Mime.MediaTypeNames.Application.Json)]
public class MetadataController : ControllerBase
{
    private readonly MetadataService _metadataService;

    public MetadataController(MetadataService metadataService)
    {
        _metadataService = metadataService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _metadataService.GetMetadata());
    }

    [HttpGet("{key}")]
    public async Task<IActionResult> GetByIdentifier(string key)
    {
        var recipe = await _metadataService.GetMetadata(key);
        if (recipe == null) {
            return NotFound();
        }
        else {
            return Ok(recipe);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Post(Famecipe.Models.Metadata metadata)
    {
        var obj = await _metadataService.CreateMetadata(metadata);
        return Created($"/metadata/{obj.Key}", obj);
    }

    [HttpPut("{key}")]
    public async Task<IActionResult> Put(string key, Famecipe.Models.Metadata metadata)
    {
        var foundItem = await _metadataService.GetMetadata(key);
        if (foundItem == null)
        {
            return NotFound();
        }
        else
        {
            await _metadataService.UpdateMetadata(key, metadata);
            return NoContent();
        }
    }

    [HttpDelete("{key}")]
    public async Task<IActionResult> Delete(string key)
    {
        var foundItem = await _metadataService.GetMetadata(key);
        if (foundItem == null) {
            return NotFound();
        }
        else {
            await this._metadataService.DeleteMetadata(key);
            return NoContent();
        }
    }
}