<template>
  <div 
    v-if="isOpen" 
    class="fixed z-50 bg-[#11221e]/95 backdrop-blur-xl border border-emerald-800/40 rounded-xl shadow-2xl shadow-black/60 py-1.5 min-w-[170px] text-sm animate-fade-in"
    :style="{ top: `${y}px`, left: `${x}px` }"
    @click.stop
  >
    <button 
      @click="onAction('open')" 
      class="w-full px-3.5 py-2 text-left text-emerald-100 hover:bg-emerald-800/40 flex items-center gap-2.5 transition-colors"
    >
      <svg xmlns="http://www.w3.org/2000/svg" class="w-4 h-4 text-emerald-400" fill="none" viewBox="0 0 24 24" stroke="currentColor">
        <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M15 12a3 3 0 11-6 0 3 3 0 016 0z" />
        <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M2.458 12C3.732 7.943 7.523 5 12 5c4.478 0 8.268 2.943 9.542 7-1.274 4.057-5.064 7-9.542 7-4.477 0-8.268-2.943-9.542-7z" />
      </svg>
      Open
    </button>

    <button 
      v-if="itemType === 'file'"
      @click="onAction('download')" 
      class="w-full px-3.5 py-2 text-left text-emerald-100 hover:bg-emerald-800/40 flex items-center gap-2.5 transition-colors"
    >
      <svg xmlns="http://www.w3.org/2000/svg" class="w-4 h-4 text-amber-400" fill="none" viewBox="0 0 24 24" stroke="currentColor">
        <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M4 16v1a3 3 0 003 3h10a3 3 0 003-3v-1m-4-4l-4 4m0 0l-4-4m4 4V4" />
      </svg>
      Download
    </button>

    <div class="h-px bg-emerald-900/40 my-1"></div>

    <button 
      @click="onAction('delete')" 
      class="w-full px-3.5 py-2 text-left text-rose-400 hover:bg-rose-950/40 hover:text-rose-300 flex items-center gap-2.5 transition-colors"
    >
      <svg xmlns="http://www.w3.org/2000/svg" class="w-4 h-4 text-rose-400" fill="none" viewBox="0 0 24 24" stroke="currentColor">
        <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M19 7l-.867 12.142A2 2 0 0116.138 21H7.862a2 2 0 01-1.995-1.858L5 7m5 4v6m4-6v6m1-10V4a1 1 0 00-1-1h-4a1 1 0 00-1 1v3M4 7h16" />
      </svg>
      Delete
    </button>
  </div>
</template>

<script setup lang="ts">
import { onMounted, onUnmounted } from 'vue';

const props = defineProps<{
  isOpen: boolean;
  x: number;
  y: number;
  itemType?: string;
}>();

const emit = defineEmits<{
  (e: 'close'): void;
  (e: 'action', action: 'open' | 'download' | 'delete'): void;
}>();

const onAction = (action: 'open' | 'download' | 'delete') => {
  emit('action', action);
  emit('close');
};

const handleGlobalClick = () => {
  if (props.isOpen) emit('close');
};

onMounted(() => {
  window.addEventListener('click', handleGlobalClick);
});

onUnmounted(() => {
  window.removeEventListener('click', handleGlobalClick);
});
</script>
