using AutoMapper;
using FileExploler.Application.FileStorage.Brokers;
using FileExploler.Application.FileStorage.Models.Storage;

namespace FileExploler.Infrastructure.FileStorage.Brokers;

public class DirectoryBroker : IDirectoryBroker
{
    private readonly IMapper _mapper;

    public DirectoryBroker(IMapper mapper)
    {
        _mapper = mapper;
    }

    public bool ExistAsync(string directoryPath) => Directory.Exists(directoryPath);

    public StorageDirectory GetByPathAsync(string directoryPath) => _mapper.Map<StorageDirectory>(new DirectoryInfo(directoryPath));

    public IEnumerable<StorageDirectory> GetDirectories(string directoryPath) => GetDirectoriesPath(directoryPath)
        .Select(path => _mapper.Map<StorageDirectory>(new DirectoryInfo(path)));

    public IEnumerable<string> GetDirectoriesPath(string directoryPath) => Directory.EnumerateDirectories(directoryPath);

    public IEnumerable<string> GetFilesPath(string directoryPath) => Directory.EnumerateFiles(directoryPath);

    public StorageDirectory CreateDirectory(string directoryPath)
    {
        var info = Directory.CreateDirectory(directoryPath);
        return _mapper.Map<StorageDirectory>(info);
    }

    public void DeleteDirectory(string directoryPath, bool recursive = true)
    {
        if (Directory.Exists(directoryPath))
            Directory.Delete(directoryPath, recursive);
    }
}
