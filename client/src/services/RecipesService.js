import { logger } from "@/utils/Logger.js"
import { api } from "./AxiosService.js"
import { AppState } from "@/AppState.js"
import { Recipe } from "@/models/Recipe.js"

class RecipesService {

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

