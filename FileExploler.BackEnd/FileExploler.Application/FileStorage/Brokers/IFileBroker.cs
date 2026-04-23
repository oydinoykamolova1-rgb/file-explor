using FileExploler.Application.FileStorage.Models.Storage;

namespace FileExploler.Application.FileStorage.Brokers;

public interface IFileBroker
{
    StorageFile GetByPath(string filePath);
}
