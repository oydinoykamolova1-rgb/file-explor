using FileExploler.Application.FileStorage.Models.Storage;

namespace FileExploler.Application.FileStorage.Services;

public interface IDriveService
{
    ValueTask<IList<StorageDrive>> GetAsync();
}
