<template>
  <div 
    @click="emit('onLoadFiles', filesSummary.fileType)" 
    class="p-2.5 flex text-emerald-100 items-center justify-between gap-3 transparent-shadow-off rounded-xl cursor-pointer hover:bg-emerald-900/40 border border-transparent hover:border-emerald-800/30 transition-all duration-200 group"
  >
    <div class="flex items-center gap-2.5 overflow-hidden">
      <div class="p-1.5 rounded-lg bg-emerald-950/60 text-emerald-400 group-hover:text-emerald-300 transition-colors">
        <svg xmlns="http://www.w3.org/2000/svg" class="w-4 h-4" fill="none" viewBox="0 0 24 24" stroke="currentColor">
          <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M7 21h10a2 2 0 002-2V9.414a1 1 0 00-.293-.707l-5.414-5.414A1 1 0 0012.586 3H7a2 2 0 00-2 2v14a2 2 0 002 2z" />
        </svg>
      </div>
      <span class="text-xs font-medium text-emerald-100 group-hover:text-emerald-300 truncate">
        {{ filesSummary.displayName }}
      </span>
    </div>

    <div class="flex items-center gap-3 text-xs font-mono text-emerald-400/70 flex-shrink-0">
      <span class="px-1.5 py-0.5 rounded bg-emerald-950/60 text-emerald-300">{{ filesSummary.count }}</span>
      <span>{{ explorerLocationService.getFileSizeString(filesSummary.size) }}</span>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ExplorerLocationService } from "@/infrastructure/services/explorerLocationService";
import type { StorageFilesSummary } from "@/infrastructure/models/filtering/StorageFilesSummary";
import type { StorageFileType } from "@/infrastructure/models/filtering/StorageFileType";

const explorerLocationService = new ExplorerLocationService();

const emit = defineEmits<{
  onLoadFiles: [fileType: StorageFileType]
}>();

const props = defineProps({
  filesSummary: {
    type: Object as () => StorageFilesSummary,
    required: true
  }
});
</script>