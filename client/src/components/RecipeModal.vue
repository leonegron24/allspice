<script setup>
import { AppState } from '@/AppState.js';
import { Recipe } from '@/models/Recipe.js';
import { ingredientsService } from '@/services/IngredientsService.js';
import { recipesService } from '@/services/RecipesService.js';
import { logger } from '@/utils/Logger.js';
import { Pop } from '@/utils/Pop.js';
import { Modal } from 'bootstrap';
import { computed, onMounted, ref } from 'vue';
import EditRecipModal from './EditRecipModal.vue';

// const props = defineProps({
//     recipe: { type: Recipe, required: true }
// })

const account = computed(() => AppState.account)
const recipe = computed(() => AppState.activeRecipe)

const ingredients = computed(() => AppState.ingredientsForRecipe)
let beingEdited = ref(true)

let toggleEditMenu = ref(false);

function editOptions() {
    console.log("toggleEditMenu Value: ", toggleEditMenu)
    toggleEditMenu.value = !toggleEditMenu.value
}
function closeEditFriendly() {
    beingEdited.value = false
}

function toggleEditRecipe() {
    console.log('Editing recipe')
    if (beingEdited.value === true) {
        beingEdited.value = false
    } else {
        beingEdited.value = true
    }
    console.log("Editing? : ", beingEdited.value)
}

async function deleteRecipe(recipeId) {
    try {
        console.log("Deleting Recipe...")
        await recipesService.deleteRecipe(recipeId)
        const modalElement = document.getElementById('recipeModal')
        const modal = Modal.getInstance(modalElement)
        modal.hide()
        Pop.toast("Recipe Deleted!")
    }
    catch (error) {
        Pop.error(error);
        logger.error(error)
    }
}

async function removeIngredient(ingredientId) {
    try {
        console.log("Ingredient Id: ", ingredientId)
        await ingredientsService.removeIngredient(ingredientId)
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
                    <div class="col-md-6 text-start">
                        <!-- TITLE -->
                        <div class="d-flex align-items-center justify-content-between p-4">
                            <div class="d-flex align-items-center">
                                <h5 class="modal-title" id="myModalLabel">{{ recipe.title }}</h5>
                                <i v-if="account?.id === recipe.creatorId" @click="editOptions()"
                                    class="mdi mdi-menu fs-4 mx-2 btn"></i>
                                <div v-if="toggleEditMenu">
                                    <button v-if="!beingEdited" @click="toggleEditRecipe()"
                                        class="btn btn-sm pill btn-grey m-2">Edit
                                        Recipe</button>
                                    <button v-if="beingEdited" @click="toggleEditRecipe()"
                                        class="btn btn-sm pill btn-grey m-2">Cancel
                                        Edit</button>
                                    <button @click="deleteRecipe(recipe.id)"
                                        class="btn text-danger btn-sm pill btn-grey btn-danger">Delete
                                        Recipe</button>
                                </div>
                            </div>

                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"
                                @click="closeEditFriendly()"></button>
                        </div>
                        <div class="p-4">
                            <!-- CREATOR -->
                            <p class="text-grey">by: {{ recipe.creator.name }}</p>
                            <!-- CATEGORY -->
                            <p class="bg-grey text-white rounded p-1 w-25 text-center">{{ recipe.category }}</p>
                        </div>
                        <!-- EDITABLE CONTENT BELOW -->
                        <div class="mx-0 p-4" v-if="!beingEdited">
                            <!-- INGREDIENTS -->
                            <div v-for="ingredient in ingredients" :key="ingredient.id">
                                {{ ingredient.quantity }} {{ ingredient.name }}
                            </div>
                            <!-- INSTRUCTIONS -->
                            <div class="pt-4">
                                <h2>Instructions</h2>
                                {{ recipe.instructions }}
                            </div>
                        </div>
                        <div class="p-4" v-if="beingEdited">
                            <div v-for="ingredient in ingredients" :key="ingredient.id">
                                <i @click="removeIngredient(ingredient.id)" class="btn mdi mdi-cancel"></i>{{
                                    ingredient.quantity }}
                                {{ ingredient.name }}
                            </div>
                            <EditRecipModal />
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