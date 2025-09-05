
using Microsoft.AspNetCore.Localization;

namespace allspice.Repositories;

public class FavoritesRepository
{
    private readonly IDbConnection _db;

    public FavoritesRepository(IDbConnection db)
    {
        _db = db;
    }

    internal Recipe CreateFavorite(int recipeId, Favorite favoriteData)
    {
        string sql = @"
    INSERT INTO favorites (recipe_id, account_id)
    VALUES (@RecipeId, @AccountId);

    SELECT recipes.*, accounts.*
    FROM recipes
    JOIN accounts ON accounts.id = recipes.creator_id
    WHERE recipes.id = @RecipeId;
    ";

        Recipe recipe = _db.Query<Recipe, Account, Recipe>(
            sql,
            (recipe, account) =>
            {
                recipe.Creator = account;
                return recipe;
            },
            new { RecipeId = recipeId, favoriteData.AccountId },
            splitOn: "Id"
        ).SingleOrDefault();

        return recipe;
    }

}