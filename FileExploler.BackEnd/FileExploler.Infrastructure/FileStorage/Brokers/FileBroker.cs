using AutoMapper;
using FileExploler.Application.FileStorage.Brokers;
using FileExploler.Application.FileStorage.Models.Storage;

namespace FileExploler.Infrastructure.FileStorage.Brokers;

public class FileBroker : IFileBroker
{
    private readonly IMapper _mapper;

    public FileBroker(IMapper mapper)
    {
        _mapper = mapper;
    }

    public StorageFile GetByPath(string filePath)
    {
        return _mapper.Map<StorageFile>(new FileInfo(filePath));
    }
}
