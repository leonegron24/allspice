<script setup>
import { Recipe } from '@/models/Recipe.js';

defineProps({
    recipe: { type: Recipe, required: true }
})
</script>


<template>
    <div :style="{ backgroundImage: `url(${recipe.img})` }" class="recipe-card p-4 card shadow">
        <span class="bg-grey text-white">{{ recipe.category }}</span>
        <div class="row bg-grey text-white">{{ recipe.title }}</div>
    </div>
</template>


<style lang="scss" scoped>
.recipe-card {
    width: 100%; // 👈 controls size
    max-height: 40dvh; // 👈 prevents it from growing too tall
    aspect-ratio: 1/1;
    background-size: cover;
    background-position: center;
}
</style>