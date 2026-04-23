using AutoMapper;
using FileExploler.Api.Models.Dtos;
using FileExploler.Application.FileStorage.Models.Storage;

namespace FileExploler.Api.Common.MapperProfiles;

public class DriveProfile : Profile
{
    public DriveProfile()
    {
        CreateMap<StorageDriveDto, StorageDrive>();
        CreateMap<StorageDrive, StorageDriveDto>();
    }
}
