namespace Domain.Exceptions;
public class ProductNotFoundException : NotFoundException
{
    public ProductNotFoundException(int id) : base($"Product with id: {id} not found")
    {

    }
}