<template>
  <div 
    class="w-full p-2.5 rounded-xl text-emerald-100 flex flex-col gap-1.5 cursor-pointer hover:bg-emerald-900/40 border border-transparent hover:border-emerald-800/30 transition-all duration-200 group"
    @click="handleDriveClick"
  >
    <div class="flex items-center justify-between">
      <div class="flex items-center gap-2.5 overflow-hidden">
        <svg xmlns="http://www.w3.org/2000/svg" class="w-5 h-5 text-emerald-400 group-hover:text-emerald-300 flex-shrink-0" fill="none" viewBox="0 0 24 24" stroke="currentColor">
          <path stroke-linecap="round" stroke-linejoin="round" stroke-width="1.8" d="M5 12h14M5 12a2 2 0 01-2-2V6a2 2 0 012-2h14a2 2 0 012 2v4a2 2 0 01-2 2M5 12a2 2 0 00-2 2v4a2 2 0 002 2h14a2 2 0 002-2v-4a2 2 0 00-2-2m-2-4h.01M17 16h.01" />
        </svg>
        <span class="font-medium text-sm truncate text-emerald-100 group-hover:text-emerald-200">
          {{ drive.name || drive.path }}
        </span>
      </div>
      <span class="text-xs text-emerald-400/70 font-mono flex-shrink-0">
        {{ explorerLocationService.getFileSizeString(drive.totalSpace) }}
      </span>
    </div>

    <!-- Mini Progress Bar -->
    <div class="w-full bg-emerald-950/70 rounded-full h-1.5 overflow-hidden border border-emerald-900/40">
      <div 
        class="bg-gradient-to-r from-emerald-500 to-amber-500 h-full rounded-full transition-all duration-500" 
        :style="{ width: `${usagePercentage}%` }"
      ></div>
    </div>
  </div>
</template>

<script lang="ts" setup>
import { computed } from "vue";
import type { StorageDrive } from "@/infrastructure/models/entities/StorageDrive";
import { ExplorerLocationService } from "@/infrastructure/services/explorerLocationService";
import { useExplorerStore } from "@/common/stores/ExplorerStore";

const explorerLocationService = new ExplorerLocationService();
const explorerStore = useExplorerStore();

const props = defineProps({
  drive: {
    type: Object as () => StorageDrive,
    required: true
  }
});

const usagePercentage = computed(() => {
  if (!props.drive.totalSpace) return 0;
  return Math.min(100, Math.round((props.drive.usedSpace / props.drive.totalSpace) * 100));
});

const handleDriveClick = () => {
  explorerStore.setCurrentPath(props.drive.path);
};
</script>