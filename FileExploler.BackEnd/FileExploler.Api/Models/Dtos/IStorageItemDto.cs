using FileExploler.Application.FileStorage.Models.Storage;

namespace FileExploler.Api.Models.Dtos;

public interface IStorageItemDto
{
    string Path { get; set; }

    StorageEntryType EntryType { get; set; }
}
