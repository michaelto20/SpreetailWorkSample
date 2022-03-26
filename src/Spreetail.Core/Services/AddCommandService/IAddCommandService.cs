using Spreetail.Core.Services.Command;

namespace Spreetail.Core.Services.AddCommandService
{
    public interface IAddCommandService<T, U> : IOperationCommand<T,U>
    {
    }
}
