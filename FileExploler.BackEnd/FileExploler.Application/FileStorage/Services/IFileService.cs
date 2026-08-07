using FileExploler.Application.Common.Models.Filtering;
using FileExploler.Application.FileStorage.Models.Filtering;
using FileExploler.Application.FileStorage.Models.Storage;

namespace FileExploler.Application.FileStorage.Services;

public interface IFileService
{
    ValueTask<IList<StorageFile>> GetFilesByPathAsync(IEnumerable<string> filePath);

    ValueTask<StorageFile> GetFilesByPathAsync(string filePath);

    IEnumerable<StorageFilesSummary> GetFilesSummary(IEnumerable<StorageFile> files);

    StorageFileType GetFileType(string filePath);

    ValueTask<bool> DeleteFileAsync(string filePath);

    ValueTask<StorageFile> CreateFileAsync(string filePath);

    ValueTask<Stream> GetFileStreamAsync(string filePath);
}
