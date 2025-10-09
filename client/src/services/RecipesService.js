import { logger } from "@/utils/Logger.js"
import { api } from "./AxiosService.js"
import { AppState } from "@/AppState.js"
import { Recipe } from "@/models/Recipe.js"
import { Pop } from "@/utils/Pop.js"

class RecipesService {
    async queryRecipes(searchQuery) {
        console.log('searching for ', searchQuery)
        const lowerQuery = searchQuery.toLowerCase()
        AppState.filteredRecipes = AppState.recipes.filter(r => (r.category || '').toLowerCase().includes(lowerQuery) ||
            (r.title || '').toLowerCase().includes(lowerQuery))
        if (!searchQuery.trim()) {
            AppState.filteredRecipes = AppState.recipes
        }

    }

    async createRecipe(newRec) {
        const response = await api.post('api/recipes', newRec)
        console.log("new Recipe: ", response.data)
        AppState.recipes.push(new Recipe(response.data))
    }

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

    async getRecipes(filterValue, account) {
        const response = await api.get('api/recipes')
        const allRecipes = response.data.map(recipes => new Recipe(recipes))
        AppState.recipes = allRecipes
        if (filterValue === 'Home') {
            AppState.filteredRecipes = allRecipes
            logger.log('Fetching all recipes: ', AppState.recipes)
        } else if (filterValue === 'myRecipes') {
            logger.log('Fetching my recipes')
            AppState.filteredRecipes = allRecipes.filter(r => r.creatorId === account.id)
        } else if (filterValue === 'myFavorites') {
            console.log('Fetching my favorites')
            AppState.filteredRecipes = AppState.accountFavorites
        }
    }

}
export const recipesService = new RecipesService()

