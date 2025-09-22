<script setup>
import { AppState } from '@/AppState.js';
import { Recipe } from '@/models/Recipe.js';
import { ingredientsService } from '@/services/IngredientsService.js';
import { Pop } from '@/utils/Pop.js';
import { computed, onMounted } from 'vue';

const props = defineProps({
    recipe: { type: Recipe, required: true }
})

const account = computed(() => AppState.account)


const ingredients = computed(() => AppState.ingredientsForRecipe)

</script>


<template>
    <div class="modal fade" :id="`recipeModal-${recipe.id}`" tabindex="-1" aria-labelledby="myModalLabel"
        aria-hidden="true">
        <div class="modal-dialog modal-xl">
            <div class="modal-content container-fluid">
                <div class="row">
                    <!-- IMAGE -->
                    <div class="col-md-6 modalImg" :style="{ backgroundImage: `url(${recipe.img})` }"></div>
                    <!-- RECIPE INFO -->
                    <div class="col-md-6 text-start p-4">
                        <!-- TITLE -->
                        <div class="d-flex justify-content-between">
                            <h5 class="modal-title" id="myModalLabel">{{ recipe.title }}</h5>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <!-- CREATOR -->
                        <div class="d-flex flex-column justify-content-start">
                            <p class="text-grey">by: {{ recipe.creator.name }}</p>
                            <!-- CATEGORY -->
                            <p class="bg-grey text-white rounded p-1 w-25 text-center">{{ recipe.category }}</p>
                        </div>
                        <!-- INGREDIENTS -->
                        <div v-for="ingredient in ingredients" :key="ingredient.id">
                            {{ ingredient.quantity }} {{ ingredient.quantity }}
                        </div>
                        <!-- INSTRUCTIONS -->
                        <div class="modal-body">
                            <h2 class="border-bottom">Instructions</h2>
                            {{ recipe.instructions }}
                        </div>
                    </div>

                </div>
                <div v-if="account?.id === recipe.creator.id" class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                    <button type="button" class="btn btn-primary">Save changes</button>
                </div>


            </div>
        </div>
    </div>
</template>


<style lang="scss" scoped>
.modalImg {
    background-position: center;
    background-size: cover;
    aspect-ratio: 1/1;
}
</style>