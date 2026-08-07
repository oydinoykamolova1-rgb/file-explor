using FileExploler.Application.FileStorage.Models.Storage;

namespace FileExploler.Application.FileStorage.Brokers;

public interface IDirectoryBroker
{
    IEnumerable<string> GetDirectoriesPath(string directoryPath);

    IEnumerable<string> GetFilesPath(string directoryPath);

    IEnumerable<StorageDirectory> GetDirectories(string directoryPath);

    StorageDirectory GetByPathAsync(string directoryPath);

    bool ExistAsync(string directoryPath);

    StorageDirectory CreateDirectory(string directoryPath);

    void DeleteDirectory(string directoryPath, bool recursive = true);
}
