using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestWebApp
{
    public interface IClock
    {
        DateTime GetTime();
    }
}
