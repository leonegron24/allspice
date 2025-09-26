<script setup>
import { Recipe } from '@/models/Recipe.js';
import RecipeModal from './RecipeModal.vue';
import { Pop } from '@/utils/Pop.js';
import { ingredientsService } from '@/services/IngredientsService.js';
import { logger } from '@/utils/Logger.js';
import { recipesService } from '@/services/RecipesService.js';
import { AppState } from '@/AppState.js';
import { computed, ref } from 'vue';
import { favoriteService } from '@/services/FavoriteService.js';

defineProps({
    recipe: { type: Recipe, required: true }
})

const account = computed(() => AppState.account)

const isFavorited = (recipeId) => {
    return AppState.accountFavorites.some(fav => fav.recipeId === recipeId)
}



async function getActiveRecipe(recipeId) {
    try {
        await recipesService.getActiveRecipe(recipeId)
        await ingredientsService.getIngredients(recipeId)
    }
    catch (error) {
        Pop.error(error);
        logger.error(error)
    }
}

async function toggleFavoriteRecipe(recipeId) {
    try {
        console.log("Favoriting this recipe!", recipeId)
        await favoriteService.toggleFavoriteRecipe(recipeId)
    }
    catch (error) {
        Pop.error(error);
        logger.error(error)
    }
}

</script>


<template>
    <div :style="{ backgroundImage: `url(${recipe.img})` }"
        class="recipe-card p-md-4 card justify-content-between shadow">
        <div class="d-flex justify-content-between align-items-center">
            <p class="blurry fs-md-4 p-md-1 shadow border border-white text-white w-25 rounded text-center">
                {{ recipe.category }}
            </p>
            <i @click="toggleFavoriteRecipe(recipe.id)" class="text-danger p-0 bg-primary btn btn-pill fs-1 mdi"
                :class="isFavorited(recipe.id) ? 'mdi-heart' : 'mdi-heart-outline'">
            </i>


        </div>
        <div>
            <div data-bs-toggle="modal" data-bs-target="#recipeModal" @click="getActiveRecipe(recipe.id)"
                class="btn p-md-1 blurry border shadow border-white align-items-center fs-md-3 row text-white title-height rounded">
                {{ recipe.title }}
            </div>
        </div>
    </div>
</template>


<style lang="scss" scoped>
.recipe-card {
    min-height: 40dvh;
    aspect-ratio: 1/1;
    background-size: cover;
    background-position: center;
}

.title-height {
    min-height: 5dvh;
}

.blurry {
    backdrop-filter: blur(10px);
    background-color: rgba(161, 141, 141, 0.3);
}
</style>