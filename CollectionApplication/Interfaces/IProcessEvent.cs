namespace CollectionApplication.Interfaces;

public interface IProcessEvent
{
    Task<bool> Process(string message);
}
