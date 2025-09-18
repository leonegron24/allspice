



namespace allspice.Repositories;

public class RecipesRepository
{
    private readonly IDbConnection _db;
    public RecipesRepository(IDbConnection db)
    {
        _db = db;
    }

    internal Recipe CreateRecipe(Recipe recipeData)
    {
        string sql = @"
        INSERT INTO
        recipes (title, instructions, img, category, creator_id)
        values ( @Title, @Instructions, @Img, @Category, @CreatorId);

        SELECT recipes.*, accounts.*
        FROM recipes
        INNER JOIN accounts ON accounts.id = recipes.creator_id
        WHERE
        recipes.id = LAST_INSERT_ID();
        ";

        Recipe recipe = _db.Query(sql, (Recipe recipe, Account account) =>
        {
            recipe.Creator = account;
            return recipe;
        }, recipeData).SingleOrDefault();

        return recipe;
    }

    internal void DeleteRecipe(int recipeId)
    {
        string sql = "DELETE FROM recipes WHERE id = @RecipeId LIMIT 1;";

        int rowsAffected = _db.Execute(sql, new { RecipeId = recipeId });

        if (rowsAffected != 1)
        {
            throw new Exception(rowsAffected + " rows were affected and thats not good");
        }
    }

    internal List<Recipe> GetAllRecipes()
    {
        string sql = @"
        SELECT recipes.*, accounts.*, COUNT(favorites.id) AS favoriteCount
        FROM
        recipes
        INNER JOIN accounts ON accounts.id = recipes.creator_id
        LEFT JOIN favorites ON favorites.recipe_id = recipes.id
        GROUP BY
        recipes.id;";

        List<Recipe> recipes = _db.Query(sql, (Recipe recipe, Account account) =>
        {
            recipe.Creator = account;
            return recipe;
        }).ToList();

        return recipes;
    }

    internal Recipe GetRecipeById(int recipeId)
    {
        string sql = @"
        SELECT recipes.*, accounts.*
        FROM recipes
        INNER JOIN accounts ON accounts.id = recipes.creator_id
        WHERE
        recipes.id = @RecipeId;
        ";

        Recipe queriedRecipe = _db.Query(sql, (Recipe recipe, Account account) =>
        {
            recipe.Creator = account;
            return recipe;
        }, new { RecipeId = recipeId }).SingleOrDefault();

        return queriedRecipe;
    }

    internal void UpdateRecipe(Recipe recipe)
    {
        string sql = @"
        UPDATE recipes 
        SET 
        title = @Title,
        instructions = @Instructions,
        img = @Img,
        category = @Category
        WHERE id = @Id LIMIT 1
        ;";

        int rowsAffected = _db.Execute(sql, recipe);

        if (rowsAffected != 1)
        {
            throw new Exception(rowsAffected + " rows were affected and thats not good");
        }
    }
}