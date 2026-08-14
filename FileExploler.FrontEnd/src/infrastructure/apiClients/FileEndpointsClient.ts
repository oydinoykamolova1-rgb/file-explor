import type ApiClientBase from "@/infrastructure/api/AxiosClient";
import { StorageFileFilterDataModel } from "@/infrastructure/models/filtering/StorageFileFilterDataModel";
import type { StorageFile } from "@/infrastructure/models/entities/StorageFile";
import type { StorageFileFilterModel } from "@/infrastructure/models/filtering/StorageFileFilterModel";

export class FileEndpointsClient {
    private client: ApiClientBase;

    constructor(client: ApiClientBase) {
        this.client = client;
    }

    public async getRootFilesFilterDataAsync(directoryPath?: string) {
        const query = directoryPath ? `?directoryPath=${encodeURIComponent(directoryPath)}` : "";
        return await this.client.getAsync<StorageFileFilterDataModel>(`api/files/root/files/filter${query}`);
    }

    public async getFilesByFilterAsync(filterOptions: StorageFileFilterModel) {
        const queryString = filterOptions.convertToQueryParams();
        return await this.client.getAsync<Array<StorageFile>>(`api/files/root/files/by-filter?${queryString}`);
    }

    public async uploadFileAsync(file: File, directoryPath: string) {
        const formData = new FormData();
        formData.append("file", file);
        formData.append("directoryPath", directoryPath);

        return await this.client.postAsync<StorageFile>("api/files/upload", formData);
    }

    public async deleteFileAsync(filePath: string) {
        const encodedPath = encodeURIComponent(filePath);
        return await this.client.deleteAsync<boolean>(`api/files?filePath=${encodedPath}`);
    }

    public getDownloadUrl(filePath: string, inline = false): string {
        return `${this.client.client.defaults.baseURL}/api/files/download?filePath=${encodeURIComponent(filePath)}&inline=${inline}`;
    }
}