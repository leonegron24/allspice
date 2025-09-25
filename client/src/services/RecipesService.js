import { logger } from "@/utils/Logger.js"
import { api } from "./AxiosService.js"
import { AppState } from "@/AppState.js"
import { Recipe } from "@/models/Recipe.js"
import { Pop } from "@/utils/Pop.js"

class RecipesService {

    async saveInstructions(recipeId, updateData) {
        const response = await api.put(`api/recipes/${recipeId}`, updateData)
        return response.data
    }

    async deleteRecipe(recipeId) {
        const confirm = await Pop.confirm("Are you sure you want to delete this recipe?")
        if (confirm) {
            const response = await api.delete(`api/recipes/${recipeId}`)
            console.log("Deleting recipe: ", response.data)
            const recipeToDelete = AppState.recipes.findIndex(rec => rec.id === recipeId)
            if (recipeToDelete > -1) {
                AppState.recipes.splice(recipeToDelete, 1)
            }
        }
    }

    async getActiveRecipe(recipeId) {
        const response = await api.get(`api/recipes/${recipeId}`)
        console.log('Active Recipe: ', response.data)
        AppState.activeRecipe = new Recipe(response.data)
    }

    async getRecipes() {
        const response = await api.get('api/recipes')
        logger.log('Got recipes 🥘', response.data)
        AppState.recipes = response.data.map(recipes => new Recipe(recipes))
    }

}
export const recipesService = new RecipesService()

