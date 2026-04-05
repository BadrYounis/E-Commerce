namespace Domain.Exceptions;
public class DeliveryMethodNotFoundException : NotFoundException
{
    public DeliveryMethodNotFoundException(int id) : base($"Delivery Method With Id: {id} Not Found")
    {

    }
}
