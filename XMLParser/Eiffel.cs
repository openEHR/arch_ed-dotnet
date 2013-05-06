using System;
using System.Diagnostics;
using EiffelKernel = EiffelSoftware.Library.Base.kernel;

namespace XMLParser
{
    public class Eiffel
    {
        [DebuggerStepThrough()]
        public static EiffelKernel.@string.STRING_8 String(string s)
        {
            return EiffelKernel.@string.Create.STRING_8.make_from_cil(s);
        }
    }
}
