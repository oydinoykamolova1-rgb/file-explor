using FileExploler.Application.FileStorage.Models.Filtering;
using FileExploler.Application.FileStorage.Services;
using Microsoft.AspNetCore.Mvc;

namespace FileExploler.Api.Controllers;


[ApiController]
[Route("api/[controller]")]
public class FilesController : ControllerBase
{
    private readonly IWebHostEnvironment _hostEnvironment;
    private readonly IFileProcessingService _fileProcessingService;
    private readonly IFileService _fileService;

    public FilesController(IWebHostEnvironment hostEnvironment, IFileProcessingService fileProcessingService, IFileService fileService)
    {
        _hostEnvironment = hostEnvironment;
        _fileProcessingService = fileProcessingService;
        _fileService = fileService;
    }

    [HttpGet("root/files/filter")]
    public async ValueTask<IActionResult> GetFilesSummary([FromQuery] string? directoryPath)
    {
        var path = string.IsNullOrWhiteSpace(directoryPath) ? (_hostEnvironment.WebRootPath ?? "C:\\") : directoryPath;
        var result = await _fileProcessingService.GetFilterDataModelAsync(path);
        return Ok(result);
    }

    [HttpGet("root/files/by-filter")]
    public async ValueTask<IActionResult> GetFilesByFilter([FromQuery] StorageFileFilterModel filterModel)
    {
        if (string.IsNullOrWhiteSpace(filterModel.DirectoryPath))
            filterModel.DirectoryPath = _hostEnvironment.WebRootPath ?? "C:\\";

        var files = await _fileProcessingService.GetByFilterAsync(filterModel);
        return Ok(files);
    }

    [HttpPost("upload")]
    public async ValueTask<IActionResult> UploadFileAsync([FromForm] IFormFile file, [FromForm] string directoryPath)
    {
        if (file == null || file.Length == 0 || string.IsNullOrWhiteSpace(directoryPath))
            return BadRequest("File and directoryPath are required.");

        if (!Directory.Exists(directoryPath))
            Directory.CreateDirectory(directoryPath);

        var destinationPath = System.IO.Path.Combine(directoryPath, file.FileName);
        using (var stream = new FileStream(destinationPath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        var storageFile = await _fileService.GetFilesByPathAsync(destinationPath);
        return Ok(storageFile);
    }

    [HttpGet("download")]
    public async ValueTask<IActionResult> DownloadFileAsync([FromQuery] string filePath, [FromQuery] bool inline = false)
    {
        if (string.IsNullOrWhiteSpace(filePath) || !System.IO.File.Exists(filePath))
            return NotFound("File not found.");

        var stream = await _fileService.GetFileStreamAsync(filePath);
        var fileName = System.IO.Path.GetFileName(filePath);
        
        // Simple MIME type detection
        var provider = new Microsoft.AspNetCore.StaticFiles.FileExtensionContentTypeProvider();
        if (!provider.TryGetContentType(fileName, out var contentType))
        {
            contentType = "application/octet-stream";
        }

        if (inline)
        {
            var cd = new System.Net.Mime.ContentDisposition
            {
                FileName = fileName,
                Inline = true
            };
            Response.Headers.Append("Content-Disposition", cd.ToString());
            return File(stream, contentType);
        }

        return File(stream, contentType, fileName);
    }

    [HttpDelete]
    public async ValueTask<IActionResult> DeleteFileAsync([FromQuery] string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
            return BadRequest("File path is required.");

        var result = await _fileService.DeleteFileAsync(filePath);
        return Ok(result);
    }
}
