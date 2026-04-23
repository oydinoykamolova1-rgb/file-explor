using AutoMapper;
using FileExploler.Application.Common.Models.Filtering;
using FileExploler.Application.Common.Querying.Extensions;
using FileExploler.Application.FileStorage.Brokers;
using FileExploler.Application.FileStorage.Models.Storage;
using FileExploler.Application.FileStorage.Services;

namespace FileExploler.Infrastructure.FileStorage.Services;

public class DirectoryService : IDirectoryService
{
    private readonly IDirectoryBroker _broker;
    private readonly IMapper _mapper;

    public DirectoryService(IDirectoryBroker broker, IMapper mapper)
    {
        _broker = broker;
        _mapper = mapper;
    }

    public ValueTask<StorageDirectory?> GetByPathAsync(string directoryPath)
    {
        if (string.IsNullOrWhiteSpace(directoryPath))
            throw new ArgumentNullException(nameof(directoryPath));

        return new ValueTask<StorageDirectory?>(_broker.GetByPathAsync(directoryPath));
    }

    public async ValueTask<IList<StorageDirectory>> GetDirectoriesAsync(string directoryPath, FilterPagination paginationOptions)
    {
        if (string.IsNullOrWhiteSpace(directoryPath))
            throw new ArgumentNullException(nameof(directoryPath));

        var directories = await Task.Run(() => _broker.GetDirectories(directoryPath).Applypagination(paginationOptions).ToList());

        return directories;
    }

    public IEnumerable<string> GetDirectoriesPath(string directoryPath, FilterPagination paginationOptions) =>
        _broker.GetDirectoriesPath(directoryPath).Applypagination(paginationOptions);

    public IEnumerable<string> GetFilesPath(string directoryPath, FilterPagination paginationOptions) =>
        _broker.GetFilesPath(directoryPath).Applypagination(paginationOptions);

}
