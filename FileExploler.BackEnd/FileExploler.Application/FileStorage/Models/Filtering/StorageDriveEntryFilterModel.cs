using FileExploler.Application.Common.Models.Filtering;

namespace FileExploler.Application.FileStorage.Models.Filtering;

public class StorageDriveEntryFilterModel : FilterPagination
{
    public bool IncludeDirectories { get; set; }

    public bool IncludeFiles { get; set; }
}
