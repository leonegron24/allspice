



namespace allspice.Repositories;


public class IngredientsRepository
{
    private readonly IDbConnection _db;

    public IngredientsRepository(IDbConnection db)
    {
        _db = db;
    }

    internal Ingredient CreateIngredient(Ingredient ingredientData)
    {
        string sql = @"
        INSERT INTO
        ingredients (name, quantity, recipe_id)
        values (@Name, @Quantity, @RecipeId);

        SELECT ingredients.*, recipes.*
        FROM ingredients
        INNER JOIN recipes ON recipes.id = ingredients.recipe_id
        WHERE
        ingredients.id = LAST_INSERT_ID();
        ";

        Ingredient newIngredient = _db.Query(sql, (Ingredient ingredient, Recipe recipe) =>
        {
            ingredient.Recipe = recipe;
            return ingredient;
        }, ingredientData).SingleOrDefault();

        return newIngredient;
    }

    internal void DeleteIngredient(int ingredientId)
    {
        string sql = "DELETE FROM ingredients WHERE id = @IngredientId LIMIT 1;";

        int rowsAffected = _db.Execute(sql, new { IngredientID = ingredientId });

        if (rowsAffected != 1)
        {
            throw new Exception(rowsAffected + " rows were affected and thats not good");
        }
    }

    internal Ingredient GetIngredientById(int ingredientId)
    {
        string sql = @"
        Select * FROM ingredients WHERE ingredients.id = @IngredientId;";

        Ingredient ingredient = _db.Query<Ingredient>(sql, new { IngredientId = ingredientId }).SingleOrDefault();
        return ingredient;
    }

    internal List<Ingredient> GetIngredientsForRecipe(Recipe recipe)
    {
        string sql = @"
        Select ingredients.*, recipes.*
        From ingredients
        INNER JOIN recipes ON recipes.id = ingredients.recipe_id
        WHERE
        recipes.id = @Id;";

        List<Ingredient> ingredients = _db.Query(sql, (Ingredient ingredient, Recipe recipe) =>
        {
            ingredient.Recipe = recipe;
            return ingredient;
        }, recipe).ToList();

        return ingredients;
    }
}