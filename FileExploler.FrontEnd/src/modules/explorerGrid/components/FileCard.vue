<template>
  <div 
    @click="emit('onOpenFile', file)"
    @contextmenu.prevent="emit('onContextMenu', $event, file)"
    class="p-3.5 transparent-shadow-on card-round cursor-pointer group flex items-center gap-3.5 transition-all duration-200 hover:-translate-y-0.5 hover:border-emerald-500/40 select-none"
  >
    <!-- File Icon based on type -->
    <div 
      class="p-2.5 rounded-xl border flex-shrink-0 transition-all"
      :class="iconContainerClass"
    >
      <svg xmlns="http://www.w3.org/2000/svg" class="w-8 h-8" fill="none" viewBox="0 0 24 24" stroke="currentColor">
        <path stroke-linecap="round" stroke-linejoin="round" stroke-width="1.8" :d="iconPath" />
      </svg>
    </div>

    <!-- File Details -->
    <div class="overflow-hidden flex-1">
      <p class="font-medium text-emerald-100 group-hover:text-emerald-300 truncate text-sm transition-colors">
        {{ file.name }}
      </p>
      <span class="text-xs text-emerald-400/70 font-mono">
        {{ explorerLocationService.getFileSizeString(file.size) }}
      </span>
    </div>
  </div>
</template>

<script lang="ts" setup>
import { computed } from "vue";
import type { StorageFile } from "@/infrastructure/models/entities/StorageFile";
import { ExplorerLocationService } from "@/infrastructure/services/explorerLocationService";

const explorerLocationService = new ExplorerLocationService();

const emit = defineEmits<{
  onOpenFile: [file: StorageFile];
  onContextMenu: [event: MouseEvent, file: StorageFile];
}>();

const props = defineProps({
  file: {
    required: true,
    type: Object as () => StorageFile
  }
});

const fileExtension = computed(() => {
  if (!props.file?.name) return '';
  const parts = props.file.name.split('.');
  return parts.length > 1 ? parts.pop()!.toLowerCase() : '';
});

const iconContainerClass = computed(() => {
  const ext = fileExtension.value;
  if (['png', 'jpg', 'jpeg', 'gif', 'svg', 'webp'].includes(ext)) {
    return 'bg-emerald-950/50 border-emerald-700/40 text-emerald-400 group-hover:bg-emerald-900/60';
  }
  if (['pdf', 'doc', 'docx', 'txt', 'md'].includes(ext)) {
    return 'bg-amber-950/50 border-amber-700/40 text-amber-400 group-hover:bg-amber-900/60';
  }
  if (['mp3', 'wav', 'mp4', 'mkv', 'avi'].includes(ext)) {
    return 'bg-teal-950/50 border-teal-700/40 text-teal-400 group-hover:bg-teal-900/60';
  }
  return 'bg-slate-900/50 border-slate-700/40 text-slate-400 group-hover:bg-slate-800/60';
});

const iconPath = computed(() => {
  const ext = fileExtension.value;
  if (['png', 'jpg', 'jpeg', 'gif', 'svg', 'webp'].includes(ext)) {
    return 'M4 16l4.586-4.586a2 2 0 012.828 0L16 16m-2-2l1.586-1.586a2 2 0 012.828 0L20 14m-6-6h.01M6 20h12a2 2 0 002-2V6a2 2 0 00-2-2H6a2 2 0 00-2 2v12a2 2 0 002 2z';
  }
  if (['mp3', 'wav', 'mp4', 'mkv', 'avi'].includes(ext)) {
    return 'M14.752 11.168l-3.197-2.132A1 1 0 0010 9.87v4.263a1 1 0 001.555.832l3.197-2.132a1 1 0 000-1.664z M21 12a9 9 0 11-18 0 9 9 0 0118 0z';
  }
  return 'M9 12h6m-6 4h6m2 5H7a2 2 0 01-2-2V5a2 2 0 012-2h5.586a1 1 0 01.707.293l5.414 5.414a1 1 0 01.293.707V19a2 2 0 01-2 2z';
});
</script>