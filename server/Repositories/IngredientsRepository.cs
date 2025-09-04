
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
}