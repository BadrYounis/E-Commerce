namespace Domain.Exceptions;
public sealed class BasketNotFoundException : NotFoundException
{
    public BasketNotFoundException(string id) : base($"Basket with id: {id} not found")
    {
        
    }
}