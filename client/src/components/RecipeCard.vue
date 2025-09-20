<script setup>
import { Recipe } from '@/models/Recipe.js';
import RecipeModal from './RecipeModal.vue';

defineProps({
    recipe: { type: Recipe, required: true }
})
</script>


<template>
    <div data-bs-toggle="modal" data-bs-target="#recipeModal" :style="{ backgroundImage: `url(${recipe.img})` }"
        class="btn recipe-card p-4 card justify-content-between shadow">
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