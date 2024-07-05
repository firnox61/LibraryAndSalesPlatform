using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Aspects.Autofac.Logging
{
    public class LogAspect : MethodInterception
    {
        protected override void OnBefore(IInvocation invocation)
        {
            Log.Information($"Calling method {invocation.Method.Name} with parameters {string.Join(", ", invocation.Arguments.Select(a => (a ?? "").ToString()))}");
        }

        protected override void OnAfter(IInvocation invocation)
        {
            Log.Information($"Method {invocation.Method.Name} completed");
        }

        protected override void OnException(IInvocation invocation, Exception e)
        {
            Log.Error(e, $"An error occurred in method {invocation.Method.Name}");
        }
    }
}
