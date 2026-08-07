<template>
  <div class="relative w-screen h-screen flex items-center justify-center bg-[#070e0d] overflow-hidden select-none">
    
    <!-- Nature Ambient Forest Background Gradient -->
    <div class="absolute inset-0 bg-gradient-to-br from-[#0c1815] via-[#081210] to-[#040807]"></div>

    <!-- Decorative Nature Radial Glows -->
    <div class="absolute -top-40 -left-40 w-96 h-96 bg-emerald-600/10 rounded-full blur-3xl pointer-events-none"></div>
    <div class="absolute -bottom-40 -right-40 w-96 h-96 bg-amber-600/10 rounded-full blur-3xl pointer-events-none"></div>
    <div class="absolute top-1/2 left-1/2 transform -translate-x-1/2 -translate-y-1/2 w-[700px] h-[500px] bg-emerald-500/5 rounded-full blur-3xl pointer-events-none"></div>

    <!-- Background Image Fallback overlay -->
    <img 
      class="absolute top-0 left-0 z-0 h-screen object-cover w-full opacity-20 mix-blend-overlay pointer-events-none"
      src="../../../assets/images/desktop-background.jpg" 
      alt="Nature desktop screen background"
      @error="handleImgError"
    />

    <!-- Explorer Main Glassmorphism Container -->
    <explorer-container class="z-20 shadow-2xl" />

  </div>
</template>

<script setup lang="ts">
import ExplorerContainer from "@/modules/explorerContainer/components/ExplorerContainer.vue";

const handleImgError = (e: Event) => {
  (e.target as HTMLElement).style.display = 'none';
};
</script>