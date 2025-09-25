<script setup>
import { AppState } from '@/AppState.js';
import { Recipe } from '@/models/Recipe.js';
import { Pop } from '@/utils/Pop.js';
import { computed, ref } from 'vue';

const recipe = computed(() => AppState.activeRecipe)

let newIngredient = ref({
    name: '',
    quantity: ''
})

let editableInstructions = ref(recipe.value.instructions)

async function addIngredient() {
    try {
        console.log("adding Ingredient")
    }
    catch (error) {
        Pop.error(error);
    }
}

async function saveInstructions() {
    try {
        console.log("Saving Instruction Edits...")
    }
    catch (error) {
        Pop.error(error);
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