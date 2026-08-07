using AutoMapper;
using FileExploler.Application.FileStorage.Brokers;
using FileExploler.Application.FileStorage.Models.Storage;

namespace FileExploler.Infrastructure.FileStorage.Brokers;

public class DriveBroker : IDriveBroker
{
    private readonly IMapper _mapper;

    public DriveBroker(IMapper mapper)
    {
        _mapper = mapper;
    }

    public IEnumerable<StorageDrive> Get()
    {
        return DriveInfo
            .GetDrives()
            .Where(drive => drive.IsReady)
            .Select(drive =>
            {
                try { return _mapper.Map<StorageDrive>(drive); }
                catch { return null; }
            })
            .Where(drive => drive != null)
            .Cast<StorageDrive>()
            .AsQueryable();
    }
}
