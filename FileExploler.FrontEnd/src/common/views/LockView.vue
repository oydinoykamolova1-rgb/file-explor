<template>
  <div class="relative w-screen h-screen overflow-hidden">

    <!-- Animated Nature Background -->
    <div class="absolute inset-0 bg-gradient-to-br from-[#081210] via-[#0c1e18] to-[#071008]"></div>
    <div class="absolute -top-32 -left-32 w-[600px] h-[600px] bg-emerald-600/10 rounded-full blur-3xl animate-pulse"></div>
    <div class="absolute -bottom-32 -right-32 w-[500px] h-[500px] bg-amber-600/8 rounded-full blur-3xl animate-pulse" style="animation-delay: 2s;"></div>
    <div class="absolute top-1/2 left-1/2 transform -translate-x-1/2 -translate-y-1/2 w-[800px] h-[600px] bg-emerald-500/5 rounded-full blur-3xl"></div>

    <!-- Background Image with overlay -->
    <img 
      class="absolute inset-0 w-full h-full object-cover opacity-15 mix-blend-overlay"
      src="../../assets/images/lock-screen-background.jpg" 
      alt="Background"
      @error="(e) => (e.target as HTMLElement).style.display = 'none'"
    />

    <!-- Login Card -->
    <div class="relative z-10 h-full flex items-center justify-center">
      <div class="w-full max-w-sm mx-4">

        <!-- Glassmorphism Card -->
        <div class="bg-[#11221e]/80 backdrop-blur-2xl border border-emerald-800/40 rounded-3xl p-8 shadow-2xl shadow-black/60">

          <!-- Profile Section -->
          <div class="flex flex-col items-center gap-4 mb-8">
            <!-- Avatar Circle -->
            <div class="relative">
              <div class="w-28 h-28 rounded-full ring-2 ring-emerald-500/40 ring-offset-2 ring-offset-transparent overflow-hidden shadow-lg shadow-emerald-950/60">
                <img 
                  src="../../assets/images/profile-picture.jpg" 
                  alt="Oydinoy"
                  class="w-full h-full object-cover object-top"
                />
              </div>
              <!-- Online indicator -->
              <div class="absolute bottom-1 right-1 w-4 h-4 bg-emerald-400 rounded-full border-2 border-[#11221e] shadow-md"></div>
            </div>

            <!-- Username -->
            <div class="text-center">
              <h2 class="text-2xl font-bold text-emerald-100 tracking-wide">Oydinoy</h2>
              <p class="text-xs text-emerald-400/60 font-mono mt-0.5 tracking-widest uppercase">File Explorer</p>
            </div>
          </div>

          <!-- Login Form -->
          <form @submit.prevent="handleLogin" class="space-y-4">

            <!-- Password Field -->
            <div class="relative">
              <label class="block text-xs font-mono uppercase tracking-wider text-emerald-400/70 mb-1.5">Password</label>
              <div class="relative">
                <svg xmlns="http://www.w3.org/2000/svg" class="w-4 h-4 text-emerald-500/60 absolute top-1/2 left-3.5 transform -translate-y-1/2" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                  <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M12 15v2m-6 4h12a2 2 0 002-2v-6a2 2 0 00-2-2H6a2 2 0 00-2 2v6a2 2 0 002 2zm10-10V7a4 4 0 00-8 0v4h8z" />
                </svg>
                <input 
                  :type="showPassword ? 'text' : 'password'"
                  v-model="password"
                  placeholder="Enter password"
                  autofocus
                  class="w-full bg-emerald-950/60 border rounded-xl py-3 pl-10 pr-10 text-sm text-emerald-100 placeholder-emerald-700 outline-none transition-all duration-200"
                  :class="[
                    errorMessage 
                      ? 'border-rose-500/60 focus:border-rose-400 bg-rose-950/20' 
                      : 'border-emerald-700/40 focus:border-emerald-500'
                  ]"
                  @input="errorMessage = ''"
                />
                <button 
                  type="button"
                  @click="showPassword = !showPassword"
                  class="absolute top-1/2 right-3 transform -translate-y-1/2 text-emerald-500/50 hover:text-emerald-300 transition-colors"
                >
                  <svg v-if="showPassword" xmlns="http://www.w3.org/2000/svg" class="w-4 h-4" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                    <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M13.875 18.825A10.05 10.05 0 0112 19c-4.478 0-8.268-2.943-9.543-7a9.97 9.97 0 011.563-3.029m5.858.908a3 3 0 114.243 4.243M9.878 9.878l4.242 4.242M9.88 9.88l-3.29-3.29m7.532 7.532l3.29 3.29M3 3l3.59 3.59m0 0A9.953 9.953 0 0112 5c4.478 0 8.268 2.943 9.543 7a10.025 10.025 0 01-4.132 5.411m0 0L21 21" />
                  </svg>
                  <svg v-else xmlns="http://www.w3.org/2000/svg" class="w-4 h-4" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                    <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M15 12a3 3 0 11-6 0 3 3 0 016 0z" />
                    <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M2.458 12C3.732 7.943 7.523 5 12 5c4.478 0 8.268 2.943 9.542 7-1.274 4.057-5.064 7-9.542 7-4.477 0-8.268-2.943-9.542-7z" />
                  </svg>
                </button>
              </div>

              <!-- Error Message -->
              <div v-if="errorMessage" class="flex items-center gap-1.5 mt-2">
                <svg xmlns="http://www.w3.org/2000/svg" class="w-3.5 h-3.5 text-rose-400 flex-shrink-0" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                  <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M12 8v4m0 4h.01M21 12a9 9 0 11-18 0 9 9 0 0118 0z" />
                </svg>
                <p class="text-xs text-rose-400 font-mono">{{ errorMessage }}</p>
              </div>
            </div>

            <!-- Login Button -->
            <button 
              type="submit"
              :disabled="!password.trim() || isLoading"
              class="w-full py-3 rounded-xl text-sm font-semibold text-white transition-all duration-200 mt-2 flex items-center justify-center gap-2 shadow-lg shadow-emerald-950/50"
              :class="[
                !password.trim() || isLoading
                  ? 'bg-emerald-800/40 text-emerald-400/50 cursor-not-allowed border border-emerald-800/30'
                  : 'bg-gradient-to-r from-emerald-600 to-emerald-500 hover:from-emerald-500 hover:to-emerald-400 border border-emerald-500/40 hover:shadow-emerald-500/20'
              ]"
            >
              <div v-if="isLoading" class="w-4 h-4 border-2 border-white/30 border-t-white rounded-full animate-spin"></div>
              <svg v-else xmlns="http://www.w3.org/2000/svg" class="w-4 h-4" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M11 16l-4-4m0 0l4-4m-4 4h14m-5 4v1a3 3 0 01-3 3H6a3 3 0 01-3-3V7a3 3 0 013-3h7a3 3 0 013 3v1" />
              </svg>
              {{ isLoading ? 'Kirish...' : 'Kirish' }}
            </button>
          </form>

          <!-- Bottom hint -->
          <div class="mt-6 flex items-center gap-2 justify-center">
            <div class="h-px flex-1 bg-emerald-900/40"></div>
            <span class="text-xs text-emerald-600/50 font-mono px-2">Nature File Explorer</span>
            <div class="h-px flex-1 bg-emerald-900/40"></div>
          </div>

        </div>

        <!-- Time/Date Display -->
        <div class="text-center mt-6">
          <p class="text-3xl font-light text-emerald-100/80 tracking-widest font-mono">{{ currentTime }}</p>
          <p class="text-sm text-emerald-400/50 mt-1 font-mono">{{ currentDate }}</p>
        </div>

      </div>
    </div>

  </div>
</template>

<script setup lang="ts">
import { ref, onMounted, onUnmounted } from "vue";
import { useRouter } from "vue-router";

const router = useRouter();

const CORRECT_USERNAME = "Oydinoy";
const CORRECT_PASSWORD = "12345678";

const password = ref<string>("");
const showPassword = ref<boolean>(false);
const showDefaultAvatar = ref<boolean>(false);
const errorMessage = ref<string>("");
const isLoading = ref<boolean>(false);
const currentTime = ref<string>("");
const currentDate = ref<string>("");

let clockInterval: ReturnType<typeof setInterval>;

const updateClock = () => {
  const now = new Date();
  currentTime.value = now.toLocaleTimeString("uz-UZ", { hour: "2-digit", minute: "2-digit" });
  currentDate.value = now.toLocaleDateString("uz-UZ", { weekday: "long", year: "numeric", month: "long", day: "numeric" });
};

onMounted(() => {
  updateClock();
  clockInterval = setInterval(updateClock, 1000);
});

onUnmounted(() => {
  clearInterval(clockInterval);
});

const handleLogin = async () => {
  if (!password.value.trim()) return;

  isLoading.value = true;
  errorMessage.value = "";

  // Small delay for UX feedback
  await new Promise(resolve => setTimeout(resolve, 600));

  if (password.value === CORRECT_PASSWORD) {
    router.push("/explorer");
  } else {
    errorMessage.value = "Parol noto'g'ri. Qayta urinib ko'ring.";
    password.value = "";
    isLoading.value = false;
    
    // Shake animation hint
    const input = document.querySelector("input[type='password'], input[type='text']") as HTMLElement;
    if (input) {
      input.classList.add("animate-bounce");
      setTimeout(() => input.classList.remove("animate-bounce"), 500);
    }
  }
};
</script>
