using FileExploler.Application.Common.Models.Filtering;
using FileExploler.Application.FileStorage.Brokers;
using FileExploler.Application.FileStorage.Models.Filtering;
using FileExploler.Application.FileStorage.Models.Settings;
using FileExploler.Application.FileStorage.Models.Storage;
using FileExploler.Application.FileStorage.Services;
using Microsoft.Extensions.Options;

namespace FileExploler.Infrastructure.FileStorage.Services;

public class FileService : IFileService
{
    private readonly FileFilterSettings _fileFilterSettings;
    private readonly FileStorageSettings _fileStorageSettings;
    private readonly IFileBroker _fileBroker;

    public FileService(IOptions<FileStorageSettings> fileStorageSettings, IOptions<FileFilterSettings> fileFilterSettings, IFileBroker fileBroker)
    {
        _fileFilterSettings = fileFilterSettings.Value;
        _fileStorageSettings = fileStorageSettings.Value;
        _fileBroker = fileBroker;
    }

    public async ValueTask<IList<StorageFile>> GetFilesByPathAsync(IEnumerable<string> filePath)
    {
        var files = await Task.Run(() => { return filePath.Select(filePath => _fileBroker.GetByPath(filePath)).ToList(); });

        return files;
    }

    public ValueTask<StorageFile> GetFilesByPathAsync(string filePath) =>
        !string.IsNullOrWhiteSpace(filePath)
        ? new ValueTask<StorageFile>(_fileBroker.GetByPath(filePath))
        : throw new ArgumentNullException(nameof(filePath));


    public IEnumerable<StorageFilesSummary> GetFilesSummary(IEnumerable<StorageFile> files)
    {
        var filesType = files.Select(file => (File: file, Type: GetFileType(file.Path)));
        return filesType
            .GroupBy(file => file.Type)
            .Select(filesGroup => new StorageFilesSummary
            {
                FileType = filesGroup.Key,
                DisplayName = _fileFilterSettings.FileExtensions.FirstOrDefault(extension => extension.FileType == filesGroup.Key)?.DisplayName ??
                    "Other files",
                Count = filesGroup.Count(),
                Size = filesGroup.Sum(file => file.File.Size),
                ImageUrl = _fileFilterSettings.FileExtensions.FirstOrDefault(extensions => extensions.FileType == filesGroup.Key)?.ImageUrl ??
                        _fileStorageSettings.FileImageUrl
            });
    }

    public StorageFileType GetFileType(string filePath)
    {
        var fileExtension = Path.GetExtension(filePath).TrimStart('.');
        var matchedFileType = _fileFilterSettings.FileExtensions.FirstOrDefault(extension => extension.Extensions.Contains(fileExtension));
        return matchedFileType?.FileType ?? StorageFileType.Other;
    }
}
