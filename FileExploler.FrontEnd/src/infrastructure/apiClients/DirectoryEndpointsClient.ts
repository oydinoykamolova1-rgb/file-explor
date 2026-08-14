import ApiClientBase from "@/infrastructure/api/AxiosClient";
import type { FilterPagination } from "@/infrastructure/models/filtering/FilterPagination";
import type { IStorageEntry } from "@/infrastructure/models/entities/IStorageEntry";
import { StorageDirectoryEntryFilterModel } from "@/infrastructure/models/filtering/StorageDirectoryEntryFilterModel";

export class DirectoryEndpointsClient {
    private client: ApiClientBase;

    constructor(client: ApiClientBase) {
        this.client = client;
    }

    public async getRootEntriesAsync(filterModel: StorageDirectoryEntryFilterModel) {
        const queryString = filterModel.convertToQueryParams();
        return await this.client.getAsync<Array<IStorageEntry>>(`api/directories/root/entries?${queryString}`);
    }

    public async getEntriesAsync(drivePath: string, paginationOptions: FilterPagination) {
        const encodedDrivePath = encodeURIComponent(drivePath);
        const queryString = paginationOptions.convertToQueryParams();
        return await this.client.getAsync<Array<IStorageEntry>>(`api/directories/entries?directoryPath=${encodedDrivePath}&${queryString}`);
    }

    public async searchEntriesAsync(directoryPath: string, query: string) {
        const encodedPath = encodeURIComponent(directoryPath);
        const encodedQuery = encodeURIComponent(query);
        return await this.client.getAsync<Array<IStorageEntry>>(`api/directories/search?directoryPath=${encodedPath}&query=${encodedQuery}`);
    }

    public async createDirectoryAsync(path: string, name: string) {
        return await this.client.postAsync<IStorageEntry>("api/directories", { path, name });
    }

    public async deleteDirectoryAsync(directoryPath: string) {
        const encodedPath = encodeURIComponent(directoryPath);
        return await this.client.deleteAsync<boolean>(`api/directories?directoryPath=${encodedPath}`);
    }
}