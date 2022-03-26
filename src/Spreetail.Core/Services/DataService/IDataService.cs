namespace Spreetail.Core.Services.DataService
{
    public interface IDataService<T, U>
    {
        bool Validate(string[] inputsTokens);
        bool Execute(T key, U value);
    }
}
