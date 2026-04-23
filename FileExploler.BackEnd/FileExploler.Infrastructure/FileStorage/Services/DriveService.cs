using FileExploler.Application.FileStorage.Brokers;
using FileExploler.Application.FileStorage.Models.Storage;
using FileExploler.Application.FileStorage.Services;

namespace FileExploler.Infrastructure.FileStorage.Services;

public class DriveService : IDriveService
{
    private readonly IDriveBroker _broker;

    public DriveService(IDriveBroker broker)
    {
        _broker = broker;
    }

    public ValueTask<IList<StorageDrive>> GetAsync()
    {
        var drives = _broker.Get().ToList();

        return new ValueTask<IList<StorageDrive>>(drives);
    }
}
