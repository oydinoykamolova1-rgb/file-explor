using FileExploler.Application.FileStorage.Models.Storage;

namespace FileExploler.Application.FileStorage.Brokers;

public interface IDriveBroker
{
    IEnumerable<StorageDrive> Get();
}
