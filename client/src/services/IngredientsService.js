import { AppState } from "@/AppState.js";
import { api } from "./AxiosService.js";
import { Ingredient } from "@/models/Ingredient.js";

class IngredientsService {

    async getIngredients(recipeId) {
        console.log("recipe Id: ", recipeId)
        const response = await api.get(`api/recipes/${recipeId}/ingredients`)
        console.log('ingredients data!', response.data)
        AppState.ingredientsForRecipe = response.data.map(pojo => new Ingredient(pojo))
    }



}
export const ingredientsService = new IngredientsService();
