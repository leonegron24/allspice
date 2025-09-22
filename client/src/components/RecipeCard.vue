<script setup>
import { Recipe } from '@/models/Recipe.js';
import RecipeModal from './RecipeModal.vue';
import { Pop } from '@/utils/Pop.js';
import { ingredientsService } from '@/services/IngredientsService.js';

defineProps({
    recipe: { type: Recipe, required: true }
})

async function getIngredients(recipeId) {
    try {
        await ingredientsService.getIngredients(recipeId)
    }
    catch (error) {
        Pop.error(error);
    }
}
</script>


<template>
    <div data-bs-toggle="modal" :data-bs-target="`#recipeModal-${recipe.id}`"
        :style="{ backgroundImage: `url(${recipe.img})` }"
        class="btn recipe-card p-4 card justify-content-between shadow" @click="getIngredients(recipe.id)">
        <RecipeModal :recipe="recipe" />
        <p class="blurry fs-4 p-1 shadow border border-white text-white w-25 rounded text-center">{{ recipe.category }}
        </p>
        <div>
            <div
                class=" p-1 blurry border shadow border-white align-items-center fs-3 row text-white title-height rounded">
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