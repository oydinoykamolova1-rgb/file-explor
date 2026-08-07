namespace FileExploler.Api.Models.Dtos;

public class CreateDirectoryDto
{
    public string Path { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
}
