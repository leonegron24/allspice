namespace allspice.Services;

public class IngredientsService
{
    private readonly IngredientsRepository _repository;
    public IngredientsService(IngedientsRepository repository)
    {
        _repository = repository;
    }
}