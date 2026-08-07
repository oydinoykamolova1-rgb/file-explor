<template>
  <div v-if="isOpen" class="fixed inset-0 z-50 flex items-center justify-center bg-black/60 backdrop-blur-sm p-4 animate-fade-in">
    <div class="bg-[#11221e] border border-emerald-800/40 rounded-2xl p-6 w-full max-w-md shadow-2xl shadow-emerald-950/60 transform transition-all scale-100">
      
      <div class="flex items-center justify-between mb-4 pb-3 border-b border-emerald-900/30">
        <h3 class="text-lg font-semibold text-emerald-100 flex items-center gap-2">
          <svg xmlns="http://www.w3.org/2000/svg" class="w-5 h-5 text-emerald-400" fill="none" viewBox="0 0 24 24" stroke="currentColor">
            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M9 13h6m-3-3v6m-9 1V7a2 2 0 012-2h6l2 2h6a2 2 0 012 2v8a2 2 0 01-2 2H5a2 2 0 01-2-2z" />
          </svg>
          Create New Folder
        </h3>
        <button @click="close" class="text-emerald-400/60 hover:text-emerald-200 transition-colors">
          <svg xmlns="http://www.w3.org/2000/svg" class="w-5 h-5" fill="none" viewBox="0 0 24 24" stroke="currentColor">
            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M6 18L18 6M6 6l12 12" />
          </svg>
        </button>
      </div>

      <div class="space-y-4">
        <div>
          <label class="block text-xs font-mono text-emerald-400/80 mb-1.5 uppercase tracking-wider">Folder Name</label>
          <input 
            v-model="folderName"
            type="text"
            placeholder="New Folder"
            class="w-full bg-emerald-950/70 border border-emerald-700/40 rounded-xl px-4 py-2.5 text-emerald-100 placeholder-emerald-700 focus:outline-none focus:border-emerald-500 transition-colors"
            @keyup.enter="handleCreate"
          />
        </div>

        <div class="flex justify-end gap-3 pt-2">
          <button 
            @click="close"
            class="px-4 py-2 rounded-xl text-sm font-medium text-emerald-300 hover:bg-emerald-900/30 transition-colors"
          >
            Cancel
          </button>
          <button 
            @click="handleCreate"
            :disabled="!folderName.trim() || isSubmitting"
            class="px-5 py-2 rounded-xl text-sm font-medium bg-emerald-600 hover:bg-emerald-500 text-white disabled:opacity-50 transition-colors shadow-md shadow-emerald-950/40"
          >
            {{ isSubmitting ? 'Creating...' : 'Create Folder' }}
          </button>
        </div>
      </div>

    </div>
  </div>
</template>

<script setup lang="ts">
import { ref } from 'vue';

const props = defineProps<{
  isOpen: boolean;
}>();

const emit = defineEmits<{
  (e: 'close'): void;
  (e: 'create', folderName: string): void;
}>();

const folderName = ref('');
const isSubmitting = ref(false);

const close = () => {
  folderName.value = '';
  emit('close');
};

const handleCreate = () => {
  if (!folderName.value.trim()) return;
  isSubmitting.value = true;
  emit('create', folderName.value.trim());
  isSubmitting.value = false;
  close();
};
</script>
