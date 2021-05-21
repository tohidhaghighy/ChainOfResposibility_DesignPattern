using ChainofResponsibility_DesignPattern.Infrastructure.Services;
using ChainofResponsibility_DesignPattern.Infrastructure.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ChainofResponsibility_DesignPattern
{
    class NumericValueHandler : Handler
    {
        public override Dictionary<string, dynamic> Handle(Dictionary<string, dynamic> request)
        {
            List<string> keys = request.Keys.ToList();
            foreach (var item in keys)
            {
                if (request[item].GetType()!=typeof(int))
                {
                    request.Remove(item);
                }
            }
            return base.Handle(request);
        }
    }

    class LimitedValueHandler : Handler
    {
        float min, max = 0;
        public LimitedValueHandler(float Min,float Max)
        {
            this.min = Min;
            this.max = Max;
        }
        public override Dictionary<string, dynamic> Handle(Dictionary<string, dynamic> request)
        {
            List<string>  keys = request.Keys.ToList();
            foreach (var item in keys)
            {
                if (request[item]<min || request[item] > max)
                {
                    request.Remove(item);
                }
            }
            return base.Handle(request);
        }
    }

    class PowerNumberHandler : Handler
    {
        public override Dictionary<string, dynamic> Handle(Dictionary<string, dynamic> request)
        {
            List<string> keys = request.Keys.ToList();
            foreach (var item in keys)
            {
                request[item]=Convert.ToInt32(request[item]) ^2;
            }
            return base.Handle(request);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var numerichandler = new NumericValueHandler();
            var limitedhandler = new LimitedValueHandler(0, 100);
            var powervaluehandler = new PowerNumberHandler();

            var dictionary = new Dictionary<string, dynamic>();
            dictionary.Add("a", 2);
            dictionary.Add("b", 5);
            dictionary.Add("c", "a");
            dictionary.Add("d", 1025);
            dictionary.Add("e", 20);
            dictionary.Add("f", 30);

            numerichandler.SetNext(limitedhandler);
            limitedhandler.SetNext(powervaluehandler);

            var a= numerichandler.Handle(dictionary);
            a.Select(i => $"{i.Key}: {i.Value}").ToList().ForEach(Console.WriteLine);
            
        }
    }
}
