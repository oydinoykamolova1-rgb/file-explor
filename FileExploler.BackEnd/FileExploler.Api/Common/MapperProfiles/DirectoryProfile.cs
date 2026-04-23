using AutoMapper;
using FileExploler.Api.Models.Dtos;
using FileExploler.Application.FileStorage.Models.Storage;

namespace FileExploler.Api.Common.MapperProfiles;

public class DirectoryProfile : Profile
{
    public DirectoryProfile()
    {
        CreateMap<StorageDirectory, StorageDirectoryDto>();
        CreateMap<StorageDirectoryDto, StorageDirectory>();
    }
}
