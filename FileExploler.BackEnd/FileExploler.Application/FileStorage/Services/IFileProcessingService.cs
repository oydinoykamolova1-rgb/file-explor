using FileExploler.Application.FileStorage.Models.Filtering;
using FileExploler.Application.FileStorage.Models.Storage;

namespace FileExploler.Application.FileStorage.Services;

public interface IFileProcessingService
{
    ValueTask<StorageFileFilterDataModel> GetFilterDataModelAsync(string directoryPath);

    ValueTask<IList<StorageFile>> GetByFilterAsync(StorageFileFilterModel filterModel);
}