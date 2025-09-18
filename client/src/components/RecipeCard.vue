<script setup>
import { Recipe } from '@/models/Recipe.js';

defineProps({
    recipe: { type: Recipe, required: true }
})
</script>


<template>
    <div :style="{ backgroundImage: `url(${recipe.img})` }" class="recipe-card p-4 card justify-content-between shadow">
        <p class="blurry text-white w-25 rounded text-center">{{ recipe.category }}</p>
        <div>
            <div class="blurry row text-white title-height rounded">{{ recipe.title }}</div>
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