using AutoMapper;
using FileExploler.Api.Models.Dtos;
using FileExploler.Application.FileStorage.Services;
using Microsoft.AspNetCore.Mvc;

namespace FileExploler.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DrivesController : ControllerBase
{
    private readonly IMapper _mapper;

    public DrivesController(IMapper mapper)
    {
        _mapper = mapper;
    }

    [HttpGet]
    public async ValueTask<IActionResult> GetAsync([FromServices] IDriveService driveService)
    {
        var data = await driveService.GetAsync();
        var result = _mapper.Map<IEnumerable<StorageDriveDto>>(data);
        return result.Any() ? Ok(result) : NoContent();
    }
}
