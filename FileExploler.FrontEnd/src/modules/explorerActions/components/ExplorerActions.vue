<template>
  <div class="h-full flex flex-col justify-between p-4 space-y-4 overflow-y-auto">
    <div>
      <!-- Explorer Title -->
      <div class="h-[52px] flex items-center justify-between px-2 mb-2">
        <div class="flex items-center gap-2.5">
          <div class="p-2 rounded-xl bg-emerald-900/40 text-emerald-400 border border-emerald-700/30">
            <svg xmlns="http://www.w3.org/2000/svg" class="w-5 h-5" fill="none" viewBox="0 0 24 24" stroke="currentColor">
              <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M3 7v10a2 2 0 002 2h14a2 2 0 002-2V9a2 2 0 00-2-2h-6l-2-2H5a2 2 0 00-2 2z" />
            </svg>
          </div>
          <div>
            <h2 class="text-emerald-100 text-base font-semibold tracking-wide">File Explorer</h2>
            <span class="text-[11px] text-emerald-400/60 font-mono">Nature Edition</span>
          </div>
        </div>
      </div>

      <horizontal-divider class="my-3 opacity-30" />

      <!-- Files Statistics -->
      <div class="space-y-1">
        <h3 class="px-2 text-xs font-mono uppercase tracking-wider text-emerald-400/60 mb-2">File Types</h3>
        <div v-for="filesSummary in filesFilterData?.filterData || []" :key="filesSummary.fileType">
          <file-summary-card-compact 
            @onLoadFiles="loadFilesAsync" 
            :files-summary="filesSummary"
          ></file-summary-card-compact>
        </div>
      </div>
    </div>

    <!-- My Computer Statistics -->
    <div class="space-y-1.5 pt-4 border-t border-emerald-900/20">
      <h3 class="px-2 text-xs font-mono uppercase tracking-wider text-emerald-400/60 mb-2">Drives & Devices</h3>
      <div v-for="drive in drives" :key="drive.path">
        <drive-card-compact :drive="drive"></drive-card-compact>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import HorizontalDivider from "@/common/components/HorizontalDivider.vue";
import DriveCardCompact from "@/modules/explorerActions/components/DriveCardCompact.vue";
import type { StorageDrive } from "@/infrastructure/models/entities/StorageDrive";
import { onMounted, ref, watch } from "vue";
import { ExplorerApiClient } from "@/infrastructure/apiClients/ExplorerApiClient";
import type { StorageFileFilterDataModel } from "@/infrastructure/models/filtering/StorageFileFilterDataModel";
import FileSummaryCardCompact from "@/modules/explorerActions/components/FileSummaryCardCompact.vue";
import type { StorageFileType } from "@/infrastructure/models/filtering/StorageFileType";
import { StorageFileFilterModel } from "@/infrastructure/models/filtering/StorageFileFilterModel";
import { useExplorerStore } from "@/common/stores/ExplorerStore";
import { storeToRefs } from "pinia";

const explorerStore = useExplorerStore();
const { currentPath, refreshCounter } = storeToRefs(explorerStore);
const explorerApiClient = new ExplorerApiClient();

const drives = ref<StorageDrive[]>([]);
const filesFilterData = ref<StorageFileFilterDataModel | null>(null);

onMounted(async () => {
  await loadDrivesAsync();
  await loadFilesFilterDataAsync();
});

watch([currentPath, refreshCounter], async () => {
  await loadFilesFilterDataAsync();
});

const loadDrivesAsync = async () => {
  const drivesResponse = await explorerApiClient.drives.getDrivesAsync();
  if (drivesResponse.response) {
    drives.value = drivesResponse.response;
  }
};

const loadFilesFilterDataAsync = async () => {
  const filesSummary = await explorerApiClient.files.getRootFilesFilterDataAsync(currentPath.value || undefined);
  if (filesSummary.response) {
    filesFilterData.value = filesSummary.response;
  }
};

const loadFilesAsync = async (fileType: StorageFileType) => {
  const filterOptions = new StorageFileFilterModel(10000, 1, [fileType]);
  explorerStore.setCurrentFilesFilterModel(filterOptions);
};
</script>