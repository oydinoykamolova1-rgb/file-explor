import { defineStore } from "pinia";
import type { StorageFileFilterModel } from "@/infrastructure/models/filtering/StorageFileFilterModel";

export const useExplorerStore = defineStore({
    id: "explorer",
    state: () => ({
        currentPath: null as string | null,
        currentFilesFilterModel: null as StorageFileFilterModel | null,
        history: [] as string[],
        forwardHistory: [] as string[],
        searchQuery: "",
        viewMode: "grid" as "grid" | "list",
        refreshCounter: 0
    }),
    getters: {
        canGoBack: (state) => state.history.length > 0,
        canGoForward: (state) => state.forwardHistory.length > 0
    },
    actions: {
        setCurrentPath(path: string | null, recordHistory = true) {
            if (path === this.currentPath) return;

            if (recordHistory && this.currentPath !== null) {
                this.history.push(this.currentPath);
                this.forwardHistory = []; // clear forward history on new navigation
            }

            this.currentPath = path;
            this.currentFilesFilterModel = null;
            this.searchQuery = "";
        },
        goBack() {
            if (this.history.length === 0) return;
            const previousPath = this.history.pop()!;
            if (this.currentPath) {
                this.forwardHistory.push(this.currentPath);
            }
            this.currentPath = previousPath;
            this.currentFilesFilterModel = null;
        },
        goForward() {
            if (this.forwardHistory.length === 0) return;
            const nextPath = this.forwardHistory.pop()!;
            if (this.currentPath) {
                this.history.push(this.currentPath);
            }
            this.currentPath = nextPath;
            this.currentFilesFilterModel = null;
        },
        resetCurrentPath() {
            this.currentPath = null;
            this.history = [];
            this.forwardHistory = [];
        },
        setCurrentFilesFilterModel(filterDataModel: StorageFileFilterModel) {
            if (!filterDataModel) return;
            this.currentFilesFilterModel = filterDataModel;
        },
        resetCurrentFilesFilterModel() {
            this.currentFilesFilterModel = null;
        },
        setSearchQuery(query: string) {
            this.searchQuery = query;
        },
        toggleViewMode() {
            this.viewMode = this.viewMode === "grid" ? "list" : "grid";
        },
        triggerRefresh() {
            this.refreshCounter++;
        }
    }
});