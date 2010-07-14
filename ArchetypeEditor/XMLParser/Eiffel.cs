using System;
using System.Diagnostics;
using EiffelKernel = EiffelSoftware.Library.Base.kernel;

namespace XMLParser
{
    public class Eiffel
    {
        [DebuggerStepThrough()]
        public static EiffelKernel.STRING_8 String(string s)
        {
            return EiffelKernel.Create.STRING_8.make_from_cil(s);
        }
    }
}
