import { logger } from "@/utils/Logger.js"
import { api } from "./AxiosService.js"

class RecipesService {

    async getRecipes() {
        const response = await api.get('api/recipes')
        logger.log('Got recipes 🥘', response.data)
    }

}
export const recipesService = new RecipesService()