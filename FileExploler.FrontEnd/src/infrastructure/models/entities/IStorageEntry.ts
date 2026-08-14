import type { StorageEntryType } from "@/infrastructure/models/entities/StorageItemType";

export interface IStorageEntry {
    name?: string;
    path: string;
    entryType: StorageEntryType;
}