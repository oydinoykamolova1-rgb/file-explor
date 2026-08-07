<template>
  <div class="p-5 h-[calc(100%-64px)] overflow-y-auto">
    <!-- Grid items view -->
    <div v-if="gridItems && gridItems.length > 0">
      <div class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4">
        <!-- Grid Items -->
        <div v-for="item in gridItems" :key="item.path">
          
          <div v-if="item.entryType === StorageEntryType.Drive">
            <drive-card 
              :drive="item as StorageDrive" 
              @onFetchDriveEntries="handleFetchDriveEntries"
            ></drive-card>
          </div>

          <div v-else-if="item.entryType === StorageEntryType.Directory">
            <directory-card 
              :directory="item as StorageDirectory" 
              @onOpenDirectory="handleOpenDirectory"
              @onContextMenu="handleContextMenu"
            ></directory-card>
          </div>

          <div v-else-if="item.entryType === StorageEntryType.File">
            <file-card 
              :file="item as StorageFile"
              @onOpenFile="handleOpenFile"
              @onContextMenu="handleContextMenu"
            ></file-card>
          </div>

        </div>
      </div>
    </div>

    <!-- Empty State -->
    <div v-else class="h-full min-h-[300px] flex flex-col items-center justify-center text-emerald-400/50 space-y-3">
      <svg xmlns="http://www.w3.org/2000/svg" class="w-16 h-16 stroke-1" fill="none" viewBox="0 0 24 24" stroke="currentColor">
        <path stroke-linecap="round" stroke-linejoin="round" d="M3 7v10a2 2 0 002 2h14a2 2 0 002-2V9a2 2 0 00-2-2h-6l-2-2H5a2 2 0 00-2 2z" />
      </svg>
      <p class="text-sm font-mono">This folder is empty</p>
    </div>
  </div>
</template>

<script lang="ts" setup>
import type { PropType } from "vue";
import DriveCard from "@/modules/explorerGrid/components/DriveCard.vue";
import FileCard from "@/modules/explorerGrid/components/FileCard.vue";
import DirectoryCard from "@/modules/explorerGrid/components/DirectoryCard.vue";
import type { IStorageEntry } from "@/infrastructure/models/entities/IStorageEntry";
import { StorageEntryType } from "@/infrastructure/models/entities/StorageItemType";
import type { StorageDrive } from "@/infrastructure/models/entities/StorageDrive";
import type { StorageDirectory } from "@/infrastructure/models/entities/StorageDirectory";
import type { StorageFile } from "@/infrastructure/models/entities/StorageFile";
import { useExplorerStore } from "@/common/stores/ExplorerStore";

const explorerStore = useExplorerStore();

const props = defineProps({
  gridItems: {
    type: Array as PropType<Array<IStorageEntry>>,
    required: true
  }
});

const emit = defineEmits<{
  (e: "openFile", file: StorageFile): void;
  (e: "contextMenu", event: MouseEvent, item: IStorageEntry): void;
}>();

const handleFetchDriveEntries = (drivePath: string) => {
  explorerStore.setCurrentPath(drivePath);
};

const handleOpenDirectory = (directoryPath: string) => {
  explorerStore.setCurrentPath(directoryPath);
};

const handleOpenFile = (file: StorageFile) => {
  emit("openFile", file);
};

const handleContextMenu = (event: MouseEvent, item: IStorageEntry) => {
  emit("contextMenu", event, item);
};
</script>