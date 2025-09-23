<script setup>
import { AppState } from '@/AppState.js';
import { Recipe } from '@/models/Recipe.js';
import { ingredientsService } from '@/services/IngredientsService.js';
import { logger } from '@/utils/Logger.js';
import { Pop } from '@/utils/Pop.js';
import { computed, onMounted, ref } from 'vue';

// const props = defineProps({
//     recipe: { type: Recipe, required: true }
// })

const account = computed(() => AppState.account)
const recipe = computed(() => AppState.activeRecipe)

const ingredients = computed(() => AppState.ingredientsForRecipe)

let toggleEditMenu = ref(false);
function editOptions() {
    console.log("toggleEditMenu Value: ", toggleEditMenu)
    toggleEditMenu.value = !toggleEditMenu.value
}

async function editRecipe() {
    try {
        console.log('Editing recipe')

    }
    catch (error) {
        Pop.error(error);
        logger.error(error)
    }
}

async function deleteRecipe() {
    try {
        console.log("Deleting Recipe...")
    }
    catch (error) {
        Pop.error(error);
        logger.error(error)
    }
}

</script>


<template>
    <div class="modal fade" data-bs-backdrop="static" data-bs-keyboard="false" id="recipeModal" tabindex="-1"
        aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-xl">
            <div class="modal-content container-fluid" v-if="recipe">
                <div class="row">
                    <!-- IMAGE -->
                    <div class="col-md-6 modalImg" :style="{ backgroundImage: `url(${recipe.img})` }"></div>

                    <!-- RECIPE INFO -->
                    <div class="col-md-6 text-start p-4">
                        <!-- TITLE -->
                        <div class="d-flex justify-content-between">
                            <div class="d-flex">
                                <h5 class="modal-title" id="myModalLabel">{{ recipe.title }}</h5>
                                <i v-if="account.id === recipe.creatorId" @click="editOptions()"
                                    class="mdi mdi-menu fs-4 mx-2 btn"></i>
                                <div v-if="toggleEditMenu">
                                    <button @click="editRecipe()" class="btn pill btn-primary mb-1">Edit Recipe</button>
                                    <button @click="deleteRecipe()" class="btn pill btn-danger">Delete Recipe</button>
                                </div>
                            </div>

                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div class="d-flex flex-column justify-content-start">
                            <!-- CREATOR -->
                            <p class="text-grey">by: {{ recipe.creator.name }}</p>
                            <!-- CATEGORY -->
                            <p class="bg-grey text-white rounded p-1 w-25 text-center">{{ recipe.category }}</p>
                        </div>
                        <!-- INGREDIENTS -->
                        <div v-for="ingredient in ingredients" :key="ingredient.id">
                            {{ ingredient.quantity }} {{ ingredient.name }}
                        </div>
                        <!-- INSTRUCTIONS -->
                        <div class="modal-body">
                            <h2 class="border-bottom">Instructions</h2>
                            {{ recipe.instructions }}
                        </div>
                    </div>

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