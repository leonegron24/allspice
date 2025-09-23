import { AppState } from "@/AppState.js";
import { api } from "./AxiosService.js";
import { Ingredient } from "@/models/Ingredient.js";

class IngredientsService {

    async getIngredients(recipeId) {
        const response = await api.get(`api/recipes/${recipeId}/ingredients`)
        AppState.ingredientsForRecipe = response.data.map(pojo => new Ingredient(pojo))
        console.log("Ingredients for Recipe: ", AppState.ingredientsForRecipe)
    }
}
export const ingredientsService = new IngredientsService();
