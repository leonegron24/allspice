<script setup>
import { AppState } from '@/AppState.js';
import { Recipe } from '@/models/Recipe.js';
import { ingredientsService } from '@/services/IngredientsService.js';
import { recipesService } from '@/services/RecipesService.js';
import { logger } from '@/utils/Logger.js';
import { Pop } from '@/utils/Pop.js';
import { computed, ref } from 'vue';

const recipe = computed(() => AppState.activeRecipe)
const ingredients = computed(() => AppState.ingredientsForRecipe)

let newIngredient = ref({
    name: '',
    quantity: ''
})

let editableInstructions = ref(recipe.value.instructions)

async function addIngredient() {
    try {
        console.log("adding Ingredient")
        const newIng = { name: newIngredient.value.name, quantity: newIngredient.value.quantity, recipeId: recipe.value.id }
        await ingredientsService.addIngredient(newIng)
    }
    catch (error) {
        Pop.error(error);
    }
}

async function saveInstructions() {
    try {
        const confirm = await Pop.confirm("Save your changes?")
        if (confirm) {
            const updateData = { instructions: editableInstructions.value }

            await recipesService.saveInstructions(recipe.value.id, updateData)
            recipe.value.instructions = editableInstructions.value // update locally so modal shows it
            Pop.success('Instructions saved!')
        }
    } catch (error) {
        logger.error(error)
        Pop.error(error.message || 'Failed to save instructions')
    }
}


</script>


<template>
    <!-- FORM: Add new ingredient -->
    <form @submit.prevent="addIngredient" class="mb-3">
        <div class="d-flex gap-2">
            <input v-model="newIngredient.quantity" type="text" class="form-control" placeholder="Quantity" />
            <input v-model="newIngredient.name" type="text" class="form-control" placeholder="Ingredient name" />
            <button type="submit" class="btn btn-sm btn-primary">Add</button>
        </div>
    </form>

    <!-- EDIT INSTRUCTIONS -->
    <div class="mb-3">
        <label class="fs-2 form-label fw-bold">Instructions</label>
        <textarea v-model="editableInstructions" rows="5" class="form-control"></textarea>
    </div>

    <!-- SAVE BUTTON -->
    <button @click="saveInstructions" class="btn btn-success">Save Changes</button>

</template>


<style lang="scss" scoped></style>