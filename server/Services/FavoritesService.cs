

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

    internal Recipe CreateFavorite(Favorite favoriteData)
    {
        Recipe favoritedRecipe = _recipesService.GetRecipeById(favoriteData.RecipeId);
        int recipeId = favoritedRecipe.Id;
        Recipe favorite = _favoritesRepository.CreateFavorite(recipeId, favoriteData);
        return favorite;
    }

    internal List<Favorite> GetAccountFavorites(Account userInfo)
    {
        List<Favorite> favorites = _favoritesRepository.GetAccountFavorites(userInfo);
        return favorites;
    }

}