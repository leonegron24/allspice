<script setup>
import { ref, watch } from 'vue';
import { loadState, saveState } from '../utils/Store.js';
import Login from './Login.vue';

const theme = ref(loadState('theme') || 'light')

function toggleTheme() {
  theme.value = theme.value == 'light' ? 'dark' : 'light'
}

watch(theme, () => {
  document.documentElement.setAttribute('data-bs-theme', theme.value)
  saveState('theme', theme.value)
}, { immediate: true })

</script>

<template>
  <nav class="text-white text-bold border-vue">
    <div class="container-fluid text-center">
      <!-- collapse button -->
      <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbar-links"
        aria-controls="navbarText" aria-expanded="false" aria-label="Toggle navigation">
        <span class="mdi mdi-menu text-light"></span>
      </button>
      <!-- collapsing menu -->
      <div class="d-flex">
        <div class="collapse navbar-collapse text-end w-25 " id="navbar-links">
          <form class="d-flex w-25" role="search">
            <input class="form-control me-2" type="search" placeholder="Search..." aria-label="Search">
            <button class="btn btn-outline-success" type="submit">Search</button>
          </form>
          <div class="d-flex">
            <div class="ms-auto">
              <button class="btn text-light fs-2" @click="toggleTheme"
                :title="`Enable ${theme == 'light' ? 'dark' : 'light'} theme.`">
                <i v-if="theme == 'dark'" class="mdi mdi-weather-sunny"></i>
                <i v-if="theme == 'light'" class="mdi mdi-weather-night"></i>
              </button>
            </div>
            <Login />
          </div>
          <!-- LOGIN COMPONENT HERE -->
        </div>
      </div>
    </div>
  </nav>
</template>

<style lang="scss" scoped>
a {
  text-decoration: none;
}

.nav-link {
  text-transform: uppercase;
}

.navbar-nav .router-link-exact-active {
  border-bottom: 2px solid var(--bs-success);
  border-bottom-left-radius: 0;
  border-bottom-right-radius: 0;
}
</style>
