namespace allspice.Services;

public class RecipesService
{
    private readonly RecipesRepository _recipesRepository;

    public RecipesService(RecipesRepository recipesRepository)
    {
        _recipesRepository = recipesRepository;
    }

    internal Recipe CreateRecipe(Recipe recipeData)
    {
        Recipe recipe = _recipesRepository.CreateRecipe(recipeData);
        return recipe;
    }

    internal void DeleteRecipe(int recipeId, Account userInfo)
    {
        Recipe recipe = GetRecipeById(recipeId);
        if (userInfo.Id != recipe.CreatorId)
        {
            throw new Exception($"You can not update this recipe, {userInfo.Name}");
        }
        _recipesRepository.DeleteRecipe(recipeId);
    }

    internal List<Recipe> GetAllRecipes()
    {
        List<Recipe> recipes = _recipesRepository.GetAllRecipes();
        return recipes;
    }

    internal Recipe GetRecipeById(int recipeId)
    {
        Recipe recipe = _recipesRepository.GetRecipeById(recipeId);
        if (recipe == null)
        {
            throw new Exception($"Recipe with Id of, {recipeId}, does not exist");
        }
        return recipe;
    }

    internal Recipe UpdateRecipe(Recipe recipeUpdateData, Account userInfo, int recipeId)
    {
        Recipe recipe = GetRecipeById(recipeId);
        if (userInfo.Id != recipe.CreatorId)
        {
            throw new Exception($"You can not update this recipe, {userInfo.Name}");
        }

        recipe.Title = recipeUpdateData.Title ?? recipe.Title;
        recipe.Instructions = recipeUpdateData.Instructions ?? recipe.Title;
        recipe.Img = recipeUpdateData.Img ?? recipe.Img;
        recipe.Category = recipeUpdateData.Category ?? recipe.Category;

        _recipesRepository.UpdateRecipe(recipe);
        return recipe;
    }
}