using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerApi.Domain.Commands
{
    public class CommandBase<T> : IRequest<T> where T : class
    {

    }
}
