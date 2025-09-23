

namespace allspice.Services;

public class IngredientsService
{
    private readonly IngredientsRepository _ingredientsRepository;
    private readonly RecipesService _recipesService;
    public IngredientsService(IngredientsRepository ingredientsRepository, RecipesService recipesService)
    {
        _ingredientsRepository = ingredientsRepository;
        _recipesService = recipesService;
    }

    internal Ingredient CreateIngredient(Ingredient ingredientData, Account userInfo)
    {
        Recipe recipe = _recipesService.GetRecipeById(ingredientData.RecipeId);
        if (recipe.CreatorId != userInfo.Id)
        {
            throw new Exception($"You can not edit another users recipe, {userInfo.Name}");
        }
        Ingredient ingredient = _ingredientsRepository.CreateIngredient(ingredientData);
        return ingredient;
    }

    internal void DeleteIngredient(int ingredientId, Account userInfo)
    {
        Ingredient ingredient = GetIngredientById(ingredientId);
        Recipe recipe = _recipesService.GetRecipeById(ingredientId);
        if (recipe.CreatorId != userInfo.Id)
        {
            throw new Exception($"You can not delete ingredients from another users recipe, {userInfo.Name}");
        }

        _ingredientsRepository.DeleteIngredient(ingredientId);
    }

    private Ingredient GetIngredientById(int ingredientId)
    {
        Ingredient ingredient = _ingredientsRepository.GetIngredientById(ingredientId);
        if (ingredient == null)
        {
            throw new Exception($"There is no ingredient with an ID of: {ingredientId}");
        }
        return ingredient;
    }

    internal List<Ingredient> GetIngredientsForRecipe(int recipeId)
    {
        Recipe recipe = _recipesService.GetRecipeById(recipeId);
        List<Ingredient> ingredients = _ingredientsRepository.GetIngredientsForRecipe(recipeId);
        return ingredients;
    }
}