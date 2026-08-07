using FileExploler.Application.FileStorage.Models.Filtering;
using FileExploler.Application.FileStorage.Models.Storage;
using FileExploler.Application.FileStorage.Services;

namespace FileExploler.Infrastructure.FileStorage.Services;

public class DirectoryProcessingService : IDirectoryProcessingService
{
    private readonly IFileService _fileService;
    private readonly IDirectoryService _directoryService;

    public DirectoryProcessingService(IFileService fileService, IDirectoryService directoryService)
    {
        _fileService = fileService;
        _directoryService = directoryService;
    }

    public async ValueTask<List<IStorageEntry>> GetEntriesAsync(string directoryPath, StorageDirectoryEntryFilterModel filterModel)
    {
        var storageItems = new List<IStorageEntry>();

        if (string.IsNullOrWhiteSpace(directoryPath) || !Directory.Exists(directoryPath))
            return storageItems;

        try
        {
            if (filterModel.IncludeDirectories)
            {
                var dirs = await _directoryService.GetDirectoriesAsync(directoryPath, filterModel);
                storageItems.AddRange(dirs);
            }

            if (filterModel.IncludeFiles)
            {
                var filesPath = _directoryService.GetFilesPath(directoryPath, filterModel);
                var files = await _fileService.GetFilesByPathAsync(filesPath);
                storageItems.AddRange(files);
            }
        }
        catch (UnauthorizedAccessException) { }
        catch (DirectoryNotFoundException) { }
        catch (Exception) { }

        return storageItems;
    }

    public async ValueTask<List<IStorageEntry>> SearchEntriesAsync(string directoryPath, string searchPattern)
    {
        var result = new List<IStorageEntry>();
        if (string.IsNullOrWhiteSpace(directoryPath) || !Directory.Exists(directoryPath) || string.IsNullOrWhiteSpace(searchPattern))
            return result;

        try
        {
            var dirInfo = new DirectoryInfo(directoryPath);
            var matchingDirs = dirInfo.EnumerateDirectories($"*{searchPattern}*", SearchOption.TopDirectoryOnly);
            foreach (var dir in matchingDirs.Take(50))
            {
                var storageDir = await _directoryService.GetByPathAsync(dir.FullName);
                if (storageDir != null) result.Add(storageDir);
            }

            var matchingFiles = dirInfo.EnumerateFiles($"*{searchPattern}*", SearchOption.TopDirectoryOnly);
            foreach (var file in matchingFiles.Take(50))
            {
                var storageFile = await _fileService.GetFilesByPathAsync(file.FullName);
                if (storageFile != null) result.Add(storageFile);
            }
        }
        catch (Exception) { }

        return result;
    }
}
