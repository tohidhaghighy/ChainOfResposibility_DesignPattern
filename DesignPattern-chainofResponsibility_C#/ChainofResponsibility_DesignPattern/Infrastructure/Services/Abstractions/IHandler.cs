using System;
using System.Collections.Generic;
using System.Text;

namespace ChainofResponsibility_DesignPattern.Infrastructure.Services.Abstractions
{
    public interface IHandler
    {
        IHandler SetNext(IHandler handler);

        Dictionary<string, dynamic> Handle(Dictionary<string,dynamic> request);
    }
}
