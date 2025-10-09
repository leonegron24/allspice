<script setup>
import { computed } from 'vue';
import { AppState } from '../AppState.js';
import { AuthService } from '../services/AuthService.js';
import { ref, watch } from 'vue';
import { loadState, saveState } from '../utils/Store.js';

const identity = computed(() => AppState.identity)
const account = computed(() => AppState.account)

function login() {
  AuthService.loginWithRedirect()
}
function logout() {
  AuthService.logout()
}
function toggleTheme() {
  theme.value = theme.value == 'light' ? 'dark' : 'light'
}
const theme = ref(loadState('theme') || 'light')
watch(theme, () => {
  document.documentElement.setAttribute('data-bs-theme', theme.value)
  saveState('theme', theme.value)
}, { immediate: true })

</script>

<template>
  <span class="navbar-text">
    <button class="btn selectable text-white" @click="login" v-if="!identity">
      Login
    </button>
    <div v-else>
      <div class="dropdown">
        <div role="button" class="rounded selectable no-select" data-bs-toggle="dropdown" aria-expanded="false"
          title="open account menu">
          <div v-if="account?.picture || identity?.picture">
            <img :src="account?.picture || identity?.picture" alt="account photo" height="40" class="user-img" />
          </div>
        </div>
        <div class="dropdown-menu dropdown-menu-sm-end dropdown-menu-start p-0" role="menu" title="account menu">
          <div class="list-group">
            <RouterLink :to="{ name: 'Account' }">
              <div class="list-group-item dropdown-item list-group-item-action">
                Manage Account
              </div>
            </RouterLink>
            <div class="list-group-item dropdown-item list-group-item-action text-danger selectable" @click="logout">
              <i class="mdi mdi-logout"></i>
              logout
            </div>
            <button class="bg-grey btn text-light fs-2" @click="toggleTheme"
              :title="`Enable ${theme == 'light' ? 'dark' : 'light'} theme.`">
              <i v-if="theme == 'dark'" class="mdi mdi-weather-sunny"></i>
              <i v-if="theme == 'light'" class="mdi mdi-weather-night"></i>
            </button>
          </div>
        </div>
      </div>
    </div>
  </span>
</template>

<style lang="scss" scoped>
.user-img {
  height: 7vh;
  width: 7vh;
  border-radius: 100px;
  object-fit: cover;
  object-position: center;
}

@media (max-width: 750px) {
  .user-img {
    height: 4vh;
    width: 4vh;
    border-radius: 100px;
    object-fit: cover;
    object-position: center;
  }
}
</style>
