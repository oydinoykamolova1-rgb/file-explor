using FileExploler.Application.Common.Models.Filtering;

namespace FileExploler.Application.FileStorage.Models.Filtering;

public class StorageFileFilterDataModel
{
    public List<StorageFilesSummary> FilterData { get; set; } = new();
}
