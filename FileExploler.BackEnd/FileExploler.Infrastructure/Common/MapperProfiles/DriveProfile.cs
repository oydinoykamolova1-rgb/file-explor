using AutoMapper;
using FileExploler.Application.FileStorage.Models.Storage;

namespace FileExploler.Infrastructure.Common.MapperProfiles;

public class DriveProfile : Profile
{
    public DriveProfile()
    {
        CreateMap<DriveInfo, StorageDrive>()
            .ForMember(src => src.Name, opt => opt.MapFrom(dest =>
                string.IsNullOrWhiteSpace(dest.VolumeLabel) ? dest.Name.TrimEnd('\\') : dest.VolumeLabel))
            .ForMember(src => src.Label, opt => opt.MapFrom(dest =>
                dest.Name.Contains(':') ? dest.Name.Substring(0, dest.Name.IndexOf(':')) : dest.Name.TrimEnd('\\')))
            .ForMember(src => src.Path, opt => opt.MapFrom(dest => dest.Name))
            .ForMember(src => src.Format, opt => opt.MapFrom(dest => dest.IsReady ? dest.DriveFormat : "Unknown"))
            .ForMember(src => src.Type, opt => opt.MapFrom(dest => dest.DriveType.ToString()))
            .ForMember(src => src.TotalSpace, opt => opt.MapFrom(dest => dest.IsReady ? dest.TotalSize : 0))
            .ForMember(src => src.FreeSpace, opt => opt.MapFrom(dest => dest.IsReady ? dest.AvailableFreeSpace : 0))
            .ForMember(src => src.UnavailableSpace, opt => opt.MapFrom(dest => dest.IsReady ? (dest.TotalFreeSpace - dest.AvailableFreeSpace) : 0))
            .ForMember(src => src.UsedSpace, opt => opt.MapFrom(dest => dest.IsReady ? (dest.TotalSize - dest.TotalFreeSpace) : 0));
    }
}
