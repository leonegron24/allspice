<script setup>
import { recipesService } from '@/services/RecipesService.js';
import { logger } from '@/utils/Logger.js';
import { Pop } from '@/utils/Pop.js';
import { Modal } from 'bootstrap';
import { ref } from 'vue';


let newRecipe = ref({
    title: '',
    category: '',
    img: ''
})

async function createRecipe() {
    try {
        console.log("creating recipe")
        const newRec = { ...newRecipe.value }
        await recipesService.createRecipe(newRec)

        newRecipe.value = {
            title: '',
            category: '',
            img: ''
        }

        const modalElement = document.getElementById('createRecipeModal')
        const modal = Modal.getInstance(modalElement)
        modal.hide()
        Pop.success('Recipe Created!')
    }
    catch (error) {
        logger.log(error)
        Pop.error(error);
    }
}

</script>


<template>
    <div class="modal fade" data-bs-backdrop="static" data-bs-keyboard="false" id="createRecipeModal" tabindex="-1"
        aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-ml">
            <div class="modal-content container-fluid">
                <h2 class="row text-white bg-success text-start border-bottom p-2 mb-4">New Recipe</h2>
                <form @submit.prevent="createRecipe" class="container">
                    <div class="text-start row">
                        <div class="col-md-6">
                            <label for="title" class="form-label">Title</label>
                            <input maxlength="30" v-model="newRecipe.title" type="text" id="title" class="form-control"
                                placeholder="Title..." />
                        </div>
                        <div class="col-md-6">
                            <label for="category" class="form-label">Category</label>
                            <select v-model="newRecipe.category" id="category" class="form-select">
                                <option disabled value="">Select a category</option>
                                <option value="breakfast">Breakfast</option>
                                <option value="lunch">Lunch</option>
                                <option value="dinner">Dinner</option>
                                <option value="dessert">Dessert</option>
                                <option value="snack">Snack</option>
                            </select>
                        </div>
                        <div class="col-md-12 mt-2">
                            <label for="img" class="form-label">Image</label>
                            <input v-model="newRecipe.img" type="text" id="img" class="form-control"
                                placeholder="Image Url..." />
                            <!-- Image Preview -->
                            <div v-if="newRecipe.img" class="mt-2 text-center">
                                <p class="mb-1">Preview:</p>
                                <img :src="newRecipe.img" alt="Recipe Image Preview" class="img-fluid rounded"
                                    style="max-height: 200px;">
                            </div>
                        </div>
                    </div>
                    <div class="text-end">
                        <button type="button" class="btn btn-white btn-sm border"
                            data-bs-dismiss="modal">Cancel</button>
                        <button type="submit" class="m-2 btn btn-sm btn-success">Submit</button>
                    </div>
                </form>
            </div>
        </div>
    </div>
</template>


<style lang="scss" scoped></style>