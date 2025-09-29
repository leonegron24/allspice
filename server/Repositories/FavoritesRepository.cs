
using Microsoft.AspNetCore.Localization;

namespace allspice.Repositories;

public class FavoritesRepository
{
    private readonly IDbConnection _db;

    public FavoritesRepository(IDbConnection db)
    {
        _db = db;
    }

    internal FavoriteRecipe CreateFavorite(int recipeId, Favorite favoriteData)
    {
        string sql = @"
    INSERT INTO favorites (recipe_id, account_id)
    VALUES (@RecipeId, @AccountId);

    SELECT 
    favorites.id AS FavoriteId,
    favorites.recipe_id AS RecipeId,
    recipes.*, 
    accounts.*
    FROM favorites
    JOIN recipes ON recipes.id = favorites.recipe_id
    JOIN accounts ON accounts.id = recipes.creator_id
    WHERE recipes.id = @RecipeId;
    ";

        FavoriteRecipe recipe = _db.Query(
            sql,
            (FavoriteRecipe recipe, Account account) =>
            {
                recipe.Creator = account;
                return recipe;
            }, favoriteData
        ).SingleOrDefault();

        return recipe;
    }

    internal void DeleteFavorite(int favoriteFavoriteId)
    {
        string sql = "DELETE FROM favorites WHERE id = @Id LIMIT 1;";

        int rowsAffected = _db.Execute(sql, new { Id = favoriteFavoriteId });

        if (rowsAffected != 1)
        {
            throw new Exception(rowsAffected + " rows were affected and thats not good");
        }
    }

    internal List<FavoriteRecipe> GetAccountFavorites(Account userInfo)
    {
        string sql = @"
        SELECT favorites.*, recipes.*
        FROM favorites
        INNER JOIN recipes ON favorites.recipe_id = recipes.id
        WHERE
        favorites.account_id = @Id;";

        List<FavoriteRecipe> favorites = _db.Query(sql, (Favorite favorite, FavoriteRecipe fRecipe) =>
        {
            fRecipe.FavoriteId = favorite.Id;
            return fRecipe;
        }, new { Id = userInfo.Id }).ToList();

        return favorites;
    }

    internal Favorite GetFavoriteById(int favoriteId)
    {
        string sql = @"
        SELECT * FROM favorites WHERE favorites.id = @FavoriteId;
        ";

        Favorite favorite = _db.Query<Favorite>(sql, new { FavoriteId = favoriteId }).SingleOrDefault();
        return favorite;
    }
}