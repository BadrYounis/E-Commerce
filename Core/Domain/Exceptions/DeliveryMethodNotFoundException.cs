namespace Domain.Exceptions;
public sealed class DeliveryMethodNotFoundException : NotFoundException
{
    public DeliveryMethodNotFoundException(int id) : base($"Delivery Method With Id: {id} Not Found")
    {

    }
}
