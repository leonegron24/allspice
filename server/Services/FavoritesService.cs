


namespace allspice.Services;

public class FavoritesService
{
    private readonly FavoritesRepository _favoritesRepository;
    private readonly RecipesService _recipesService;

    public FavoritesService(FavoritesRepository favoritesRepository, RecipesService recipesService)
    {
        _favoritesRepository = favoritesRepository;
        _recipesService = recipesService;
    }

    internal FavoriteRecipe CreateFavorite(Favorite favoriteData)
    {
        Recipe favoritedRecipe = _recipesService.GetRecipeById(favoriteData.RecipeId);
        FavoriteRecipe favorite = _favoritesRepository.CreateFavorite(favoriteData);
        return favorite;
    }

    internal void DeleteFavorite(int favoriteFavoriteId, Account userInfo)
    {
        Favorite favoriteToDelete = GetFavoriteById(favoriteFavoriteId);
        if (favoriteToDelete.AccountId != userInfo.Id)
        {
            throw new Exception($"You can not delete another users favorite, {userInfo.Name}");
        }
        _favoritesRepository.DeleteFavorite(favoriteFavoriteId);
    }

    internal List<FavoriteRecipe> GetAccountFavorites(Account userInfo)
    {
        List<FavoriteRecipe> favorites = _favoritesRepository.GetAccountFavorites(userInfo);
        return favorites;
    }

    private Favorite GetFavoriteById(int favoriteId)
    {
        Favorite favorite = _favoritesRepository.GetFavoriteById(favoriteId);
        return favorite;
    }

}