using AutoMapper;
using FileExploler.Api.Models.Dtos;
using FileExploler.Application.FileStorage.Models.Storage;

namespace FileExploler.Api.Common.MapperProfiles;

public class StorageItemProfile : Profile
{
    public StorageItemProfile()
    {
        CreateMap<IStorageEntry, IStorageItemDto>();
        CreateMap<IStorageItemDto, IStorageEntry>();
    }
}
