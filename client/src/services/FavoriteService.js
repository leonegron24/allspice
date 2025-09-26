import { AppState } from "@/AppState.js"
import { api } from "./AxiosService.js"
import { Favorite } from "@/models/Favorite.js"

class FavoriteService {

    async toggleFavoriteRecipe(recipeId) {
        const favoriteIndex = AppState.accountFavorites.findIndex(fav => fav.recipeId === recipeId)
        if (favoriteIndex !== -1) {
            const favoriteId = AppState.accountFavorites[favoriteIndex].id
            console.log("favoriteId", favoriteId)
            const response = await api.delete(`api/favorites/${favoriteId}`)
            AppState.accountFavorites.splice(favoriteIndex, 1)
            return response.data
        } else {
            console.log("recipeId", { recipeId })
            const response = await api.post('api/favorites', { recipeId })
            AppState.accountFavorites.push(new Favorite(response.data))
            return response.data
        }
    }



}
export const favoriteService = new FavoriteService()