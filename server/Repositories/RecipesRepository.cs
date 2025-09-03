


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

    internal List<Recipe> GetAllRecipes()
    {
        string sql = @"
        SELECT recipes.*, accounts.*
        FROM recipes
        INNER JOIN accounts ON accounts.id = recipes.creator_id
        ";

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
}