using MediatR;

namespace CustomerApi.Domain.Commands
{
    public class CommandBase<T> : IRequest<T> where T : class
    {

    }
}
