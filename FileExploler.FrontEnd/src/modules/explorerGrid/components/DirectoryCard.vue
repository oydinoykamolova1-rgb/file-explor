<template>
  <div 
    @click="emit('onOpenDirectory', directory.path)"
    @contextmenu.prevent="emit('onContextMenu', $event, directory)"
    class="p-3.5 transparent-shadow-on card-round cursor-pointer group flex items-center gap-3.5 transition-all duration-200 hover:-translate-y-0.5 hover:border-emerald-500/40 select-none"
  >
    <!-- Folder Icon -->
    <div class="p-2.5 rounded-xl bg-amber-950/40 border border-amber-800/30 text-amber-400 group-hover:text-amber-300 group-hover:bg-amber-900/40 transition-all flex-shrink-0">
      <svg xmlns="http://www.w3.org/2000/svg" class="w-8 h-8" fill="currentColor" viewBox="0 0 24 24">
        <path d="M19.5 21a3 3 0 003-3v-8a3 3 0 00-3-3h-7.618a1.5 1.5 0 01-1.06-.44L8.763 4.5a3 3 0 00-2.12-.88H4.5a3 3 0 00-3 3v11.382a3 3 0 003 3h15z" />
      </svg>
    </div>

    <!-- Directory Details -->
    <div class="overflow-hidden flex-1">
      <p class="font-medium text-emerald-100 group-hover:text-emerald-300 truncate text-sm transition-colors">
        {{ directory.name }}
      </p>
      <span class="text-xs text-emerald-400/70 font-mono">
        {{ directory.itemsCount !== undefined ? `${directory.itemsCount} items` : 'Directory' }}
      </span>
    </div>
  </div>
</template>

<script lang="ts" setup>
import type { StorageDirectory } from "@/infrastructure/models/entities/StorageDirectory";

const emit = defineEmits<{
  onOpenDirectory: [directoryPath: string];
  onContextMenu: [event: MouseEvent, item: StorageDirectory];
}>();

const props = defineProps({
  directory: {
    required: true,
    type: Object as () => StorageDirectory
  }
});
</script>