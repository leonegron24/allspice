import { logger } from "@/utils/Logger.js"
import { api } from "./AxiosService.js"
import { AppState } from "@/AppState.js"
import { Recipe } from "@/models/Recipe.js"

class RecipesService {

    async getRecipes() {
        const response = await api.get('api/recipes')
        logger.log('Got recipes 🥘', response.data)
        AppState.recipes = response.data.map(recipes => new Recipe(recipes))
    }

}
export const recipesService = new RecipesService()

