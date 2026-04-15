namespace CollectionDomain.Interfaces.Services;

public interface IProcessEvent
{
    Task<bool> Process(string message);
}
