import { AppState } from "@/AppState.js"
import { api } from "./AxiosService.js"
import { Favorite } from "@/models/Favorite.js"
import { Recipe } from "@/models/Recipe.js"

class FavoriteService {
    async getFavorites() {
        console.log("Service getting favorites")
    }

    async toggleFavoriteRecipe(recipeId) {
        const favoriteIndex = AppState.accountFavorites.findIndex(rec => rec.id === recipeId)
        if (favoriteIndex !== -1) {
            const favoriteId = AppState.accountFavorites[favoriteIndex].id
            console.log("Deleting favoriteId", favoriteId)
            const response = await api.delete(`api/favorites/${favoriteId}`)
            AppState.accountFavorites.splice(favoriteIndex, 1)
            return response.data
        } else {
            console.log("recipeId", recipeId)
            const response = await api.post('api/favorites', { recipeId: recipeId })
            AppState.accountFavorites.push(new Recipe(response.data))
            console.log("Favorites in AppState: ", AppState.accountFavorites)
            return response.data
        }
    }



}
export const favoriteService = new FavoriteService()