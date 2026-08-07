<template>
  <div class="w-11/12 max-w-6xl h-[85vh] overflow-hidden rounded-2xl container-shadow flex flex-col relative">

    <!-- Main Content Area -->
    <div class="flex h-full w-full overflow-hidden">
      <!-- Left Sidebar Action Section -->
      <explorer-actions class="w-72 flex-shrink-0 section-shadow border-r border-emerald-900/20" />

      <!-- Right Main Grid Section -->
      <div class="flex-1 flex flex-col h-full bg-[#0c1916]/80 overflow-hidden">
        
        <!-- Header Actions & Navigation Bar -->
        <explorer-grid-actions 
          @openCreateFolder="isCreateFolderOpen = true"
          @uploadFile="handleUploadFile"
        />

        <!-- Loading State Indicator -->
        <div v-if="isLoading" class="flex-1 flex flex-col items-center justify-center text-emerald-400 gap-3">
          <div class="w-8 h-8 border-2 border-emerald-500 border-t-transparent rounded-full animate-spin"></div>
          <span class="text-xs font-mono tracking-wider">Loading items...</span>
        </div>

        <!-- Main Explorer Items Grid -->
        <explorer-grid 
          v-else 
          :grid-items="filteredGridItems"
          @openFile="handleOpenFile"
          @contextMenu="handleContextMenu"
        />
      </div>
    </div>

    <!-- Create Folder Modal -->
    <create-folder-modal
      :is-open="isCreateFolderOpen"
      @close="isCreateFolderOpen = false"
      @create="handleCreateFolder"
    />

    <!-- Right-Click Context Menu -->
    <context-menu
      :is-open="contextMenuState.isOpen"
      :x="contextMenuState.x"
      :y="contextMenuState.y"
      :item-type="contextMenuState.item?.entryType === StorageEntryType.File ? 'file' : 'directory'"
      @close="contextMenuState.isOpen = false"
      @action="handleContextMenuAction"
    />

  </div>
</template>

<script setup lang="ts">
import ExplorerActions from "@/modules/explorerActions/components/ExplorerActions.vue";
import ExplorerGrid from "@/modules/explorerGrid/components/ExplorerGrid.vue";
import ExplorerGridActions from "@/modules/explorerGrid/components/ExplorerGridActions.vue";
import CreateFolderModal from "@/modules/explorerGrid/components/CreateFolderModal.vue";
import ContextMenu from "@/modules/explorerGrid/components/ContextMenu.vue";
import { ExplorerApiClient } from "@/infrastructure/apiClients/ExplorerApiClient";
import { onBeforeMount, ref, computed, watch } from "vue";
import { StorageDirectoryEntryFilterModel } from "@/infrastructure/models/filtering/StorageDirectoryEntryFilterModel";
import type { IStorageEntry } from "@/infrastructure/models/entities/IStorageEntry";
import { StorageEntryType } from "@/infrastructure/models/entities/StorageItemType";
import type { StorageFile } from "@/infrastructure/models/entities/StorageFile";
import type { StorageDirectory } from "@/infrastructure/models/entities/StorageDirectory";
import { useExplorerStore } from "@/common/stores/ExplorerStore";
import { storeToRefs } from "pinia";

const explorerApiClient = new ExplorerApiClient();
const explorerStore = useExplorerStore();
const { currentPath, currentFilesFilterModel, searchQuery, refreshCounter } = storeToRefs(explorerStore);

const gridItems = ref<Array<IStorageEntry>>([]);
const isLoading = ref<boolean>(true);
const isCreateFolderOpen = ref<boolean>(false);

const contextMenuState = ref({
  isOpen: false,
  x: 0,
  y: 0,
  item: null as IStorageEntry | null
});

onBeforeMount(() => {
  loadGridItemsAsync();
});

watch([currentPath, refreshCounter], async () => {
  await loadGridItemsAsync();
});

watch(currentFilesFilterModel, async () => {
  if (currentFilesFilterModel.value) {
    await loadGridFilesAsync();
  }
});

const filteredGridItems = computed(() => {
  if (!searchQuery.value || !searchQuery.value.trim()) {
    return gridItems.value;
  }
  const q = searchQuery.value.toLowerCase().trim();
  return gridItems.value.filter(item => item.name && item.name.toLowerCase().includes(q));
});

const loadGridItemsAsync = async () => {
  isLoading.value = true;
  gridItems.value = [];

  if (!currentPath.value) {
    // Root view → show all computer drives (C:, D:, ...)
    const drivesResponse = await explorerApiClient.drives.getDrivesAsync();
    if (drivesResponse.response) {
      gridItems.value = drivesResponse.response as unknown as Array<IStorageEntry>;
    }
  } else {
    // Directory view → show entries inside the selected path
    const filterModel = new StorageDirectoryEntryFilterModel(200, 1, true, true);
    const entriesResponse = await explorerApiClient.directories.getEntriesAsync(currentPath.value, filterModel);
    if (entriesResponse.response) {
      gridItems.value = entriesResponse.response;
    }
  }

  isLoading.value = false;
};


const loadGridFilesAsync = async () => {
  if (!currentFilesFilterModel.value) return;
  isLoading.value = true;
  gridItems.value = [];

  const filesResponse = await explorerApiClient.files.getFilesByFilterAsync(currentFilesFilterModel.value);

  if (filesResponse.response) {
    gridItems.value = filesResponse.response;
  }

  isLoading.value = false;
};

const handleCreateFolder = async (folderName: string) => {
  if (!currentPath.value) return;
  await explorerApiClient.directories.createDirectoryAsync(currentPath.value, folderName);
  explorerStore.triggerRefresh();
};

const handleUploadFile = async (file: File) => {
  if (!currentPath.value) return;
  isLoading.value = true;
  await explorerApiClient.files.uploadFileAsync(file, currentPath.value);
  explorerStore.triggerRefresh();
};

const handleOpenFile = (file: StorageFile) => {
  const downloadUrl = explorerApiClient.files.getDownloadUrl(file.path, true);
  window.open(downloadUrl, "_blank");
};

const handleContextMenu = (event: MouseEvent, item: IStorageEntry) => {
  contextMenuState.value = {
    isOpen: true,
    x: event.clientX,
    y: event.clientY,
    item
  };
};

const handleContextMenuAction = async (action: 'open' | 'download' | 'delete') => {
  const item = contextMenuState.value.item;
  if (!item) return;

  if (action === 'open') {
    if (item.entryType === StorageEntryType.Directory || item.entryType === StorageEntryType.Drive) {
      explorerStore.setCurrentPath(item.path);
    } else if (item.entryType === StorageEntryType.File) {
      handleOpenFile(item as StorageFile);
    }
  } else if (action === 'download' && item.entryType === StorageEntryType.File) {
    const downloadUrl = explorerApiClient.files.getDownloadUrl(item.path, false);
    window.location.href = downloadUrl;
  } else if (action === 'delete') {
    isLoading.value = true;
    if (item.entryType === StorageEntryType.File) {
      await explorerApiClient.files.deleteFileAsync(item.path);
    } else if (item.entryType === StorageEntryType.Directory) {
      await explorerApiClient.directories.deleteDirectoryAsync(item.path);
    }
    explorerStore.triggerRefresh();
  }
};
</script>