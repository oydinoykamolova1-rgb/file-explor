using FileExploler.Application.Common.Models.Filtering;
using FileExploler.Application.FileStorage.Models.Filtering;
using FileExploler.Application.FileStorage.Models.Storage;
using FileExploler.Application.FileStorage.Services;

namespace FileExploler.Infrastructure.FileStorage.Services;

public class FileProcessingService : IFileProcessingService
{
    private readonly IDirectoryService _directoryService;
    private readonly IFileService _fileService;

    public FileProcessingService(IDirectoryService directoryService, IFileService fileService)
    {
        _directoryService = directoryService;
        _fileService = fileService;
    }

    public async ValueTask<IList<StorageFile>> GetByFilterAsync(StorageFileFilterModel filterModel)
    {
        if (string.IsNullOrWhiteSpace(filterModel?.DirectoryPath) || !Directory.Exists(filterModel.DirectoryPath))
            return new List<StorageFile>();

        try
        {
            var filteredFilesPath = _directoryService
                .GetFilesPath(filterModel.DirectoryPath, filterModel)
                .Where(filePath => filterModel.FileTypes == null || !filterModel.FileTypes.Any() || filterModel.FileTypes.Contains(_fileService.GetFileType(filePath)));

            var files = await _fileService.GetFilesByPathAsync(filteredFilesPath);
            return files;
        }
        catch (Exception)
        {
            return new List<StorageFile>();
        }
    }

    public async ValueTask<StorageFileFilterDataModel> GetFilterDataModelAsync(string directoryPath)
    {
        if (string.IsNullOrWhiteSpace(directoryPath) || !Directory.Exists(directoryPath))
            return new StorageFileFilterDataModel { FilterData = new List<StorageFilesSummary>() };

        try
        {
            var pagination = new FilterPagination
            {
                PageSize = 500,
                PageToken = 1
            };

            var filePath = _directoryService.GetFilesPath(directoryPath, pagination);
            var files = await _fileService.GetFilesByPathAsync(filePath);

            var filesSummary = _fileService.GetFilesSummary(files);
            return new StorageFileFilterDataModel
            {
                FilterData = filesSummary.ToList()
            };
        }
        catch (Exception)
        {
            return new StorageFileFilterDataModel { FilterData = new List<StorageFilesSummary>() };
        }
    }
}
