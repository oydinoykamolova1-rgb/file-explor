using AutoMapper;
using FileExploler.Api.Models.Dtos;
using FileExploler.Application.FileStorage.Models.Storage;

namespace FileExploler.Api.Common.MapperProfiles;

public class FileProfile : Profile
{
    public FileProfile()
    {
        CreateMap<StorageFile, StorageFileDto>();
        CreateMap<StorageFileDto, StorageFile>();
    }
}
