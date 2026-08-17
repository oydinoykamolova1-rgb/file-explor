using AutoMapper;
using FileExploler.Api.Models.Dtos;
using FileExploler.Application.FileStorage.Models.Filtering;
using FileExploler.Application.FileStorage.Services;
using Microsoft.AspNetCore.Mvc;

namespace FileExploler.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DirectoriesController : ControllerBase
{
    private readonly IDirectoryService _directoryService;
    private readonly IDirectoryProcessingService _directoryProcessingService;
    private readonly IMapper _mapper;

    public DirectoriesController(IDirectoryService directoryService, IDirectoryProcessingService directoryProcessingService, IMapper mapper)
    {
        _directoryService = directoryService;
        _directoryProcessingService = directoryProcessingService;
        _mapper = mapper;
    }

    [HttpGet("entries")]
    public async ValueTask<IActionResult> GetDirectoryEntriesAsync(
        [FromQuery] string? directoryPath,
        [FromQuery] StorageDirectoryEntryFilterModel filterModel,
        [FromServices] IWebHostEnvironment environment
    )
    {
        var targetPath = string.IsNullOrWhiteSpace(directoryPath) ? (environment.WebRootPath ?? "C:\\") : directoryPath;
        var data = await _directoryProcessingService.GetEntriesAsync(targetPath, filterModel);
        return Ok(data);
    }

    [HttpGet("root/entries")]
    public async ValueTask<IActionResult> GetRootEntriesAsync(
        [FromQuery] StorageDirectoryEntryFilterModel filterModel,
        [FromServices] IWebHostEnvironment environment
    )
    {
        var targetPath = string.IsNullOrWhiteSpace(environment.WebRootPath) ? "C:\\" : environment.WebRootPath;
        var data = await _directoryProcessingService.GetEntriesAsync(targetPath, filterModel);
        return Ok(data);
    }

    [HttpGet("search")]
    public async ValueTask<IActionResult> SearchEntriesAsync(
        [FromQuery] string directoryPath,
        [FromQuery] string query
    )
    {
        if (string.IsNullOrWhiteSpace(directoryPath) || string.IsNullOrWhiteSpace(query))
            return Ok(new List<object>());

        var data = await _directoryProcessingService.SearchEntriesAsync(directoryPath, query);
        return Ok(data);
    }

    [HttpPost]
    public async ValueTask<IActionResult> CreateDirectoryAsync([FromBody] CreateDirectoryDto dto)
    {
        if (dto == null || string.IsNullOrWhiteSpace(dto.Path) || string.IsNullOrWhiteSpace(dto.Name))
            return BadRequest("Path and Name are required.");

        var basePath = dto.Path.EndsWith(":") ? dto.Path + "\\" : dto.Path;
        var fullPath = System.IO.Path.Combine(basePath, dto.Name);
        var created = await _directoryService.CreateDirectoryAsync(fullPath);
        return Ok(created);
    }

    [HttpDelete]
    public async ValueTask<IActionResult> DeleteDirectoryAsync([FromQuery] string directoryPath)
    {
        if (string.IsNullOrWhiteSpace(directoryPath))
            return BadRequest("Directory path is required.");

        var result = await _directoryService.DeleteDirectoryAsync(directoryPath, true);
        return Ok(result);
    }
}
