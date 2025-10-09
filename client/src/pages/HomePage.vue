<script setup>
import { AppState } from '@/AppState.js';
import CreateRecipeModal from '@/components/CreateRecipeModal.vue';
import Navbar from '@/components/Navbar.vue';
import RecipeCard from '@/components/RecipeCard.vue';
import RecipeModal from '@/components/RecipeModal.vue';
import { favoriteService } from '@/services/FavoriteService.js';
import { recipesService } from '@/services/RecipesService.js';
import { logger } from '@/utils/Logger.js';
import { Pop } from '@/utils/Pop.js';
import { computed, onMounted, ref } from 'vue';

const account = computed(() => AppState.account)
const recipes = computed(() => AppState.filteredRecipes)
let filter = ref('Home')

function setFilter(filterKey) {
  filter.value = filterKey
  getRecipes(filterKey)
}

onMounted(() => {
  getRecipes(filter.value)
})

async function getRecipes(filterKey) {
  try {
    await recipesService.getRecipes(filterKey, account.value)
  }
  catch (error) {
    Pop.error(error);
    logger.error("[Could not fetch Recipe!]", error.message)
  }
}



</script>

<template>
  <!-- Header -->
  <section class="bg-cooking m-4 rounded position-relative shadow align-items-center">
    <header>
      <Navbar />
    </header>
    <RouterLink :to="{ name: 'Home' }"
      class="justify-content-center d-flex align-items-center text-light text-decoration-none">
      <div class="text-center p-4 text-white">
        <h1>All-Spice</h1>
        <h4>Cherish Your Family</h4>
        <h4>And Theri Cooking</h4>
      </div>
    </RouterLink>
    <div
      class="container-fluid text-center bg-white border border-black rounded setWidth  inImageBox align-content-center shadow">
      <div class="row justify-content-between">
        <div class="text-success col-md-4 col-3 btn p-0" @click="setFilter('Home')">Home</div>
        <div class="text-success col-md-4 col-4 btn p-0" @click="setFilter('myRecipes')">My Recipes</div>
        <div class="text-success col-md-4 col-3 btn p-0" @click="setFilter('myFavorites')">Favorites</div>
      </div>
    </div>
  </section>

  <!-- Recipe Cards -->
  <section class="container-fluid">
    <div class="row">
      <div class="col-md-4 pb-4 d-flex justify-content-around" v-for="recipe in recipes" :key="recipe.id">
        <RecipeCard :recipe="recipe" />
      </div>
    </div>
    <RecipeModal />
  </section>

  <div v-if="account" class="text-end sticky-bottom p-4">
    <button data-bs-toggle="modal" data-bs-target="#createRecipeModal" class="btn btn-success fs-1 rounded-pill"> <i
        class="mdi mdi-plus"></i>
    </button>
  </div>
  <CreateRecipeModal />


</template>

<style scoped lang="scss">
.bg-cooking {
  background-image: url('https://images.unsplash.com/photo-1466637574441-749b8f19452f?w=800&auto=format&fit=crop&q=60&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxzZWFyY2h8NHx8Y29va2luZ3xlbnwwfHwwfHx8MA%3D%3D');
  background-position: center;
}

.inImageBox {
  position: relative;
  top: 1vh;
  min-height: 4vh;
}

.setWidth {
  max-width: 25%;
}

@media (max-width: 900px) {
  .setWidth {
    max-width: 75%;
  }

}
</style>
