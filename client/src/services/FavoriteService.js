import { AppState } from "@/AppState.js"
import { api } from "./AxiosService.js"
import { FavoriteRecipe } from "@/models/FavoriteRecipe.js"
import { Pop } from "@/utils/Pop.js"
import { logger } from "@/utils/Logger.js"

class FavoriteService {

    async getFavorites() {
        try {
            console.log("Service getting favorites")
            const response = await api.get('account/favorites')
            AppState.accountFavorites = response.data.map(r => new FavoriteRecipe(r))
        }
        catch (error) {
            Pop.error(error);
            logger.error(error)
        }
    }

    async toggleFavoriteRecipe(recipeId) {
        const favoriteIndex = AppState.accountFavorites.findIndex(rec => rec.id === recipeId)
        if (favoriteIndex !== -1) {
            const favoriteId = AppState.accountFavorites[favoriteIndex].favoriteId
            console.log("Deleting favoriteId", favoriteId)
            const response = await api.delete(`api/favorites/${favoriteId}`)
            AppState.accountFavorites.splice(favoriteIndex, 1)
            return response.data
        } else {
            console.log("recipeId", recipeId)
            const response = await api.post('api/favorites', { recipeId: recipeId })
            AppState.accountFavorites.push(new FavoriteRecipe(response.data))
            console.log("Favorites in AppState: ", AppState.accountFavorites)
            return response.data
        }
    }



}
export const favoriteService = new FavoriteService()