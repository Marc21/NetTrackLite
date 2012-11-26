using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetInjector
{
    public class InjectionException : Exception
    {
        public InjectionException()
            : base()
        {
        }

        public InjectionException(string message)
            : base(message+" error"+NativeInterop.GetLastError())
        {
        }
    }
}
