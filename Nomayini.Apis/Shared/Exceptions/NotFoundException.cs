namespace Nomayini.Apis;
public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message) { }
}