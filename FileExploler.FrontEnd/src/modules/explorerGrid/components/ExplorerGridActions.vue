<template>
  <div class="h-[64px] w-full flex items-center justify-between px-5 py-2.5 gap-4 border-b border-emerald-900/20 bg-[#0d1815]/60 backdrop-blur-md">
    
    <!-- Left Section: Navigation & Breadcrumb -->
    <div class="flex items-center gap-3 overflow-hidden flex-1">
      
      <!-- Back / Forward Navigation -->
      <div class="flex items-center gap-1.5 flex-shrink-0">
        <button 
          :disabled="!explorerStore.canGoBack && !explorerStore.currentPath"
          class="p-2 rounded-xl text-emerald-200 bg-emerald-950/60 border border-emerald-800/30 hover:bg-emerald-800/40 hover:border-emerald-500/40 transition-all disabled:opacity-30 disabled:pointer-events-none"
          @click="handleBackAction"
          title="Go Back"
        >
          <svg xmlns="http://www.w3.org/2000/svg" class="w-4 h-4" fill="none" viewBox="0 0 24 24" stroke="currentColor">
            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M15 19l-7-7 7-7" />
          </svg>
        </button>

        <button 
          :disabled="!explorerStore.canGoForward"
          class="p-2 rounded-xl text-emerald-200 bg-emerald-950/60 border border-emerald-800/30 hover:bg-emerald-800/40 hover:border-emerald-500/40 transition-all disabled:opacity-30 disabled:pointer-events-none"
          @click="explorerStore.goForward()"
          title="Go Forward"
        >
          <svg xmlns="http://www.w3.org/2000/svg" class="w-4 h-4" fill="none" viewBox="0 0 24 24" stroke="currentColor">
            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M9 5l7 7-7 7" />
          </svg>
        </button>
      </div>

      <!-- Clickable Breadcrumbs -->
      <div class="flex items-center gap-1 overflow-x-auto text-sm font-mono scrollbar-none py-1">
        <button 
          @click="explorerStore.setCurrentPath(null)"
          class="px-2 py-1 rounded-lg text-emerald-400 hover:text-emerald-200 hover:bg-emerald-900/40 transition-colors flex items-center gap-1.5 flex-shrink-0"
        >
          <svg xmlns="http://www.w3.org/2000/svg" class="w-4 h-4 text-emerald-400" fill="none" viewBox="0 0 24 24" stroke="currentColor">
            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M3 12l2-2m0 0l7-7 7 7M5 10v10a1 1 0 001 1h3m10-11l2 2m-2-2v10a1 1 0 01-1 1h-3m-6 0a1 1 0 001-1v-4a1 1 0 011-1h2a1 1 0 011 1v4a1 1 0 001 1m-6 0h6" />
          </svg>
          Computer
        </button>

        <template v-for="(crumb, idx) in breadcrumbs" :key="crumb.path">
          <span class="text-emerald-700/60 font-sans flex-shrink-0">/</span>
          <button 
            @click="explorerStore.setCurrentPath(crumb.path)"
            class="px-2 py-1 rounded-lg hover:bg-emerald-900/40 transition-colors flex-shrink-0 truncate max-w-[140px]"
            :class="idx === breadcrumbs.length - 1 ? 'text-emerald-100 font-semibold' : 'text-emerald-400/80 hover:text-emerald-200'"
          >
            {{ crumb.text }}
          </button>
        </template>
      </div>

    </div>

    <!-- Right Section: Toolbar Actions & Search -->
    <div class="flex items-center gap-3 flex-shrink-0">
      
      <!-- New Folder Button -->
      <button 
        v-if="explorerStore.currentPath"
        @click="emit('openCreateFolder')"
        class="px-3 py-1.5 rounded-xl bg-emerald-950/70 hover:bg-emerald-800/40 border border-emerald-800/40 text-emerald-300 hover:text-emerald-100 text-xs font-medium flex items-center gap-1.5 transition-all shadow-sm"
        title="Create New Folder"
      >
        <svg xmlns="http://www.w3.org/2000/svg" class="w-4 h-4 text-emerald-400" fill="none" viewBox="0 0 24 24" stroke="currentColor">
          <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M9 13h6m-3-3v6m-9 1V7a2 2 0 012-2h6l2 2h6a2 2 0 012 2v8a2 2 0 01-2 2H5a2 2 0 01-2-2z" />
        </svg>
        <span>New Folder</span>
      </button>

      <!-- Upload File Button -->
      <button 
        v-if="explorerStore.currentPath"
        @click="triggerFileUpload"
        class="px-3 py-1.5 rounded-xl bg-emerald-700/80 hover:bg-emerald-600 border border-emerald-600/40 text-white text-xs font-medium flex items-center gap-1.5 transition-all shadow-sm shadow-emerald-950/50"
        title="Upload File"
      >
        <svg xmlns="http://www.w3.org/2000/svg" class="w-4 h-4" fill="none" viewBox="0 0 24 24" stroke="currentColor">
          <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M4 16v1a3 3 0 003 3h10a3 3 0 003-3v-1m-4-8l-4-4m0 0L8 8m4-4v12" />
        </svg>
        <span>Upload</span>
      </button>
      <input ref="fileInput" type="file" class="hidden" @change="handleFileSelected" />

      <!-- Live Search Bar -->
      <div class="relative">
        <svg xmlns="http://www.w3.org/2000/svg" class="w-4 h-4 text-emerald-400/70 absolute top-1/2 left-3 transform -translate-y-1/2 pointer-events-none" fill="none" viewBox="0 0 24 24" stroke="currentColor">
          <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M21 21l-6-6m2-5a7 7 0 11-14 0 7 7 0 0114 0z" />
        </svg>
        <input 
          v-model="searchInput"
          type="text"
          class="w-36 focus:w-48 bg-emerald-950/60 border border-emerald-800/40 rounded-xl py-1.5 pl-9 pr-3 text-xs text-emerald-100 placeholder-emerald-600 outline-none focus:border-emerald-500 transition-all duration-300"
          placeholder="Search..."
        />
      </div>

      <!-- Refresh Button -->
      <button 
        @click="explorerStore.triggerRefresh()"
        class="p-2 rounded-xl text-emerald-300 hover:text-emerald-100 hover:bg-emerald-900/40 transition-colors"
        title="Refresh"
      >
        <svg xmlns="http://www.w3.org/2000/svg" class="w-4 h-4" fill="none" viewBox="0 0 24 24" stroke="currentColor">
          <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M4 4v5h.582m15.356 2A8.001 8.001 0 004.582 9m0 0H9m11 11v-5h-.581m0 0a8.003 8.003 0 01-15.357-2m15.357 2H15" />
        </svg>
      </button>

    </div>

  </div>
</template>

<script setup lang="ts">
import { ref, computed, watch } from "vue";
import { useExplorerStore } from "@/common/stores/ExplorerStore";

const explorerStore = useExplorerStore();
const searchInput = ref("");
const fileInput = ref<HTMLInputElement>();

const emit = defineEmits<{
  (e: "openCreateFolder"): void;
  (e: "uploadFile", file: File): void;
}>();

watch(searchInput, (newQuery) => {
  explorerStore.setSearchQuery(newQuery);
});

const breadcrumbs = computed(() => {
  if (!explorerStore.currentPath) return [];

  // Normalize Windows / Posix path separators
  const normPath = explorerStore.currentPath.replace(/\//g, '\\');
  const parts = normPath.split('\\').filter(p => p.trim() !== '');

  const result: Array<{ text: string; path: string }> = [];
  let accumulatedPath = '';

  for (let i = 0; i < parts.length; i++) {
    const part = parts[i];
    if (i === 0 && part.endsWith(':')) {
      accumulatedPath = part + '\\';
    } else {
      accumulatedPath = accumulatedPath ? `${accumulatedPath.replace(/\\$/, '')}\\${part}` : part;
    }
    result.push({ text: part, path: accumulatedPath });
  }

  return result;
});

const handleBackAction = () => {
  if (explorerStore.canGoBack) {
    explorerStore.goBack();
  } else if (explorerStore.currentPath) {
    const normPath = explorerStore.currentPath.replace(/\//g, '\\');
    const pathSegments = normPath.split('\\').filter(p => p.trim() !== '');
    if (pathSegments.length > 1) {
      const parentPath = pathSegments.slice(0, -1).join('\\');
      explorerStore.setCurrentPath(parentPath);
    } else {
      explorerStore.setCurrentPath(null);
    }
  }
};

const triggerFileUpload = () => {
  fileInput.value?.click();
};

const handleFileSelected = (e: Event) => {
  const target = e.target as HTMLInputElement;
  if (target.files && target.files.length > 0) {
    emit("uploadFile", target.files[0]);
    target.value = "";
  }
};
</script>