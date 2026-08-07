<template>
  <div 
    class="transparent-shadow-on card-round p-4 cursor-pointer group flex flex-col justify-between transition-all duration-300 hover:-translate-y-1 hover:border-emerald-500/40"
    @click="handleDriveClick"
  >
    <!-- Header -->
    <div class="flex items-center gap-3 mb-3">
      <div class="p-2.5 rounded-xl bg-emerald-950/60 border border-emerald-800/40 text-emerald-400 group-hover:text-emerald-300 group-hover:bg-emerald-900/60 transition-colors">
        <svg xmlns="http://www.w3.org/2000/svg" class="w-7 h-7" fill="none" viewBox="0 0 24 24" stroke="currentColor">
          <path stroke-linecap="round" stroke-linejoin="round" stroke-width="1.8" d="M5 12h14M5 12a2 2 2 0 01-2-2V6a2 2 2 0 012-2h14a2 2 2 0 012 2v4a2 2 2 0 01-2 2M5 12a2 2 2 0 00-2 2v4a2 2 2 0 002 2h14a2 2 2 0 002-2v-4a2 2 2 0 00-2-2m-2-4h.01M17 16h.01" />
        </svg>
      </div>
      <div class="overflow-hidden">
        <h4 class="font-semibold text-emerald-100 group-hover:text-emerald-300 transition-colors truncate text-base">
          {{ drive.name || drive.path }}
        </h4>
        <span class="text-xs text-emerald-400/70 uppercase tracking-wider font-mono">
          {{ drive.type || 'Local Drive' }}
        </span>
      </div>
    </div>

    <!-- Chart & Details -->
    <div class="flex items-center gap-4">
      <div class="w-20 h-20 relative flex-shrink-0">
        <canvas ref="chartCanvas" width="80" height="80"></canvas>
      </div>
      <div class="flex-1 space-y-1 text-xs font-mono">
        <div class="flex justify-between text-emerald-200">
          <span class="text-emerald-400/80">Used:</span>
          <span>{{ explorerLocationService.getFileSizeString(drive.usedSpace) }}</span>
        </div>
        <div class="flex justify-between text-emerald-200">
          <span class="text-emerald-400/80">Free:</span>
          <span>{{ explorerLocationService.getFileSizeString(drive.freeSpace) }}</span>
        </div>
        <div class="w-full bg-emerald-950/60 rounded-full h-1.5 overflow-hidden mt-1 border border-emerald-900/30">
          <div 
            class="bg-gradient-to-r from-emerald-500 to-amber-500 h-full rounded-full transition-all duration-500" 
            :style="{ width: `${usagePercentage}%` }"
          ></div>
        </div>
      </div>
    </div>
  </div>
</template>

<script lang="ts" setup>
import { Chart, ChartItem, DoughnutController, ArcElement, Tooltip } from "chart.js";
import { onMounted, ref, computed } from "vue";
import type { StorageDrive } from "@/infrastructure/models/entities/StorageDrive";
import { ExplorerLocationService } from "@/infrastructure/services/explorerLocationService";
import { useExplorerStore } from "@/common/stores/ExplorerStore";

Chart.register(ArcElement, DoughnutController, Tooltip);

const explorerLocationService = new ExplorerLocationService();
const explorerStore = useExplorerStore();

const props = defineProps({
  drive: {
    type: Object as () => StorageDrive,
    required: true
  }
});

const emit = defineEmits<{
  onFetchDriveEntries: [drivePath: string]
}>();

const chartCanvas = ref<ChartItem>();

const usagePercentage = computed(() => {
  if (!props.drive.totalSpace) return 0;
  return Math.min(100, Math.round((props.drive.usedSpace / props.drive.totalSpace) * 100));
});

const handleDriveClick = () => {
  emit("onFetchDriveEntries", props.drive.path);
  explorerStore.setCurrentPath(props.drive.path);
};

onMounted(() => {
  renderChart();
});

const renderChart = () => {
  if (!chartCanvas.value) return;

  const chartData = {
    labels: ['Used Space', 'Free Space', 'Unavailable'],
    datasets: [
      {
        data: [props.drive?.usedSpace || 0, props.drive?.freeSpace || 0, props.drive?.unavailableSpace || 0],
        backgroundColor: ['#10b981', '#064e3b', '#334155'],
        hoverOffset: 2,
        borderWidth: 0
      }
    ]
  };

  new Chart(chartCanvas.value as ChartItem, {
    type: 'doughnut',
    data: chartData,
    options: {
      cutout: '72%',
      responsive: true,
      maintainAspectRatio: false,
      plugins: {
        tooltip: { enabled: true }
      }
    }
  });
};
</script>