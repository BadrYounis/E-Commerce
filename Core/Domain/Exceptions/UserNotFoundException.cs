namespace Domain.Exceptions;
public sealed class UserNotFoundException : NotFoundException
{
    public UserNotFoundException(string email):base($"User With This Email: {email} Not Found")
    {
        
    }
}