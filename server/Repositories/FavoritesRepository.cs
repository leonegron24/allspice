
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

    internal void DeleteFavorite(int favoriteFavoriteId)
    {
        string sql = "DELETE FROM favorites WHERE id = @Id LIMIT 1;";

        int rowsAffected = _db.Execute(sql, new { Id = favoriteFavoriteId });

        if (rowsAffected != 1)
        {
            throw new Exception(rowsAffected + " rows were affected and thats not good");
        }
    }

    internal List<Favorite> GetAccountFavorites(Account userInfo)
    {
        string sql = @"
        SELECT favorites.*, recipes.*
        FROM favorites
        INNER JOIN recipes ON favorites.recipe_id = recipes.id
        WHERE
        favorites.account_id = @Id;";

        List<Favorite> favorites = _db.Query(sql, (Favorite favorite, Recipe recipe) =>
        {
            favorite.Recipe = recipe;
            return favorite;
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