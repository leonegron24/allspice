<script setup>
import { ref, watch } from 'vue';
import Login from './Login.vue';
import { Pop } from '@/utils/Pop.js';
import { recipesService } from '@/services/RecipesService.js';


let query = ref('')

async function queryRecipes() {
  try {
    const searchQuery = query.value
    await recipesService.queryRecipes(searchQuery)
    query.value = ''
  }
  catch (error) {
    Pop.error(error);
  }
}


</script>

<template>
  <nav class="text-white text-bold border-vue">
    <div class="container-fluid">
      <div class="row p-2 w-100 setupNav align-items-center">
        <form @submit.prevent="queryRecipes" class="col-3 d-flex" role="search">
          <input v-model="query" class="form-control me-2" type="search" placeholder="Search..." aria-label="Search">
        </form>
        <div class="col-1">
          <Login />
        </div>
      </div>
    </div>
  </nav>
</template>

<style lang="scss" scoped>
a {
  text-decoration: none;
}

.setupNav {
  justify-content: end
}

.nav-link {
  text-transform: uppercase;
}

.navbar-nav .router-link-exact-active {
  border-bottom: 2px solid var(--bs-success);
  border-bottom-left-radius: 0;
  border-bottom-right-radius: 0;
}

.form-control {
  width: 16ch;
  margin-left: auto;
  transition: width .3s ease;

  &:focus {
    width: 100%;
  }
}

@media (max-width: 1100px) {
  .setupNav {
    justify-content: space-between;
  }

}
</style>
