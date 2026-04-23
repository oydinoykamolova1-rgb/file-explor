using FileExploler.Application.FileStorage.Models.Filtering;
using FileExploler.Application.FileStorage.Models.Storage;

namespace FileExploler.Application.FileStorage.Services;

public interface IDirectoryProcessingService
{
    ValueTask<List<IStorageEntry>> GetEntriesAsync(string directoryPath, StorageDirectoryEntryFilterModel filterModel);
}
