namespace FileExploler.Application.FileStorage.Models.Storage;

public interface IStorageEntry
{
    string Path { get; }

    StorageEntryType EntryType { get; set; }
}
