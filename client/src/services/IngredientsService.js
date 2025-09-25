import { AppState } from "@/AppState.js";
import { api } from "./AxiosService.js";
import { Ingredient } from "@/models/Ingredient.js";
import { Pop } from "@/utils/Pop.js";

class IngredientsService {

    async addIngredient(newIng) {
        const response = await api.post('api/ingredients', newIng)
        AppState.ingredientsForRecipe.push(new Ingredient(response.data))
    }

    async removeIngredient(ingredientId) {
        const confirm = await Pop.confirm("Remove Ingredients?")
        if (confirm) {
            const response = await api.delete(`api/ingredients/${ingredientId}`)
            console.log("Deleting ingredient: ", response.data)
            const ingredientToDelete = AppState.ingredientsForRecipe.findIndex(ing => ing.id == ingredientId)
            if (ingredientToDelete != null) {
                AppState.ingredientsForRecipe.splice(ingredientToDelete, 1)
            }
        }
    }

    async getIngredients(recipeId) {
        const response = await api.get(`api/recipes/${recipeId}/ingredients`)
        AppState.ingredientsForRecipe = response.data.map(pojo => new Ingredient(pojo))
        console.log("Ingredients for Recipe: ", AppState.ingredientsForRecipe)
    }
}
export const ingredientsService = new IngredientsService();
