using FileExploler.Application.Common.Models.Filtering;

namespace FileExploler.Application.FileStorage.Models.Filtering;

public class StorageFileFilterModel : FilterPagination
{
    public string DirectoryPath { get; set; } = string.Empty;

    public ICollection<StorageFileType> FileTypes { get; set; }
}
