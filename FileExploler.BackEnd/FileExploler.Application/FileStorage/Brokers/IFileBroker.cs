using FileExploler.Application.FileStorage.Models.Storage;

namespace FileExploler.Application.FileStorage.Brokers;

public interface IFileBroker
{
    StorageFile GetByPath(string filePath);
    bool Exist(string filePath);
    StorageFile Create(string filePath);
    void Delete(string filePath);
    Stream GetStream(string filePath);
}
