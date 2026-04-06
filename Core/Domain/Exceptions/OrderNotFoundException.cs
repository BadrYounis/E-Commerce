namespace Domain.Exceptions;
public sealed class OrderNotFoundException : NotFoundException
{
    public OrderNotFoundException(Guid id) : base($"Order With Id: {id} Not Found")
    {
    }
}