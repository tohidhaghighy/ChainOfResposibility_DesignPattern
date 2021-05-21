using ChainofResponsibility_DesignPattern.Infrastructure.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChainofResponsibility_DesignPattern.Infrastructure.Services
{
    public class Handler : IHandler
    {
        private IHandler _nextHandler;
        public virtual Dictionary<string, dynamic> Handle(Dictionary<string, dynamic> request)
        {
            if (this._nextHandler != null)
            {
                return this._nextHandler.Handle(request);
            }
            else
            {
                return request;
            }
        }

        public IHandler SetNext(IHandler handler)
        {
            this._nextHandler = handler;
            return handler;
        }
    }
}
