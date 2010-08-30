using System;
using System.Diagnostics;
using EiffelKernel = EiffelSoftware.Library.Base.Kernel;

namespace XMLParser
{
    public class Eiffel
    {
        [DebuggerStepThrough()]
        public static EiffelKernel.String_8 String(string s)
        {
            return EiffelKernel.Create.String_8.MakeFromCil(s);
        }
    }
}
