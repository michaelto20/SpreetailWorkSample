namespace Spreetail.Core.Services.Command
{
    public interface ICommand
    {
        bool Validate(string[] inputsTokens);
        bool Execute();
    }
}
