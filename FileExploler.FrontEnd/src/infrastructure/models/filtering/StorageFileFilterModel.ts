import { FilterPagination } from "@/infrastructure/models/filtering/FilterPagination";
import type { StorageFileType } from "@/infrastructure/models/filtering/StorageFileType";

export class StorageFileFilterModel extends FilterPagination {
    directoryPath?: string;
    fileTypes: Array<StorageFileType>;

    constructor(pageSize: number, pageToken: number, fileTypes: Array<StorageFileType>, directoryPath?: string) {
        super(pageSize, pageToken);
        this.fileTypes = fileTypes;
        this.directoryPath = directoryPath;
    }

    override convertToQueryParams(): URLSearchParams {
        const params = super.convertToQueryParams();
        if (this.directoryPath) {
            params.append("directoryPath", this.directoryPath);
        }
        if (this.fileTypes && this.fileTypes.length > 0) {
            this.fileTypes.forEach(ft => params.append("fileTypes", ft.toString()));
        }

        return params;
    }
}