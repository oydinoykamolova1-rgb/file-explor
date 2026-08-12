using AutoMapper;
using FileExploler.Application.FileStorage.Models.Storage;

namespace FileExploler.Infrastructure.Common.MapperProfiles;

public class DirectoryProfile : Profile
{
    public DirectoryProfile()
    {
        CreateMap<DirectoryInfo, StorageDirectory>()
            .ForMember(src => src.Name, opt => opt.MapFrom(dest => dest.Name))
            .ForMember(src => src.Path, opt => opt.MapFrom(dest => dest.FullName))
            .ForMember(src => src.ItemsCount, opt => opt.MapFrom(dest => GetItemsCountSafe(dest)));
    }

    private static int GetItemsCountSafe(DirectoryInfo directoryInfo)
    {
        try
        {
            return directoryInfo.EnumerateFileSystemInfos("*", new EnumerationOptions
            {
                IgnoreInaccessible = true,
                RecurseSubdirectories = false
            }).Count();
        }
        catch
        {
            return 0;
        }
    }
}
