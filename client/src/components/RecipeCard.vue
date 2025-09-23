<script setup>
import { Recipe } from '@/models/Recipe.js';
import RecipeModal from './RecipeModal.vue';
import { Pop } from '@/utils/Pop.js';
import { ingredientsService } from '@/services/IngredientsService.js';
import { logger } from '@/utils/Logger.js';
import { recipesService } from '@/services/RecipesService.js';

defineProps({
    recipe: { type: Recipe, required: true }
})

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

</script>


<template>
    <div data-bs-toggle="modal" data-bs-target="#recipeModal" :style="{ backgroundImage: `url(${recipe.img})` }"
        class="btn recipe-card p-md-4 card justify-content-between shadow" @click="getActiveRecipe(recipe.id)">
        <p class="blurry fs-md-4 p-md-1 shadow border border-white text-white w-25 rounded text-center">
            {{ recipe.category }}
        </p>
        <div>
            <div
                class=" p-md-1 blurry border shadow border-white align-items-center fs-md-3 row text-white title-height rounded">
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