using MediatR;

namespace CustomerApi.Domain.Queries
{
    public abstract class QueryBase<TResult> : IRequest<TResult> where TResult : class
    {

    }
}
