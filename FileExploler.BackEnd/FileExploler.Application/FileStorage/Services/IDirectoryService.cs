using FileExploler.Application.Common.Models.Filtering;
using FileExploler.Application.FileStorage.Models.Storage;

namespace FileExploler.Application.FileStorage.Services;

public interface IDirectoryService
{
    IEnumerable<string> GetDirectoriesPath(string directoryPath, FilterPagination paginationOptions);

    IEnumerable<string> GetFilesPath(string directoryPath, FilterPagination paginationOptions);

    ValueTask<IList<StorageDirectory>> GetDirectoriesAsync(string directoryPath, FilterPagination paginationOptions);

    ValueTask<StorageDirectory?> GetByPathAsync(string directoryPath);
}
