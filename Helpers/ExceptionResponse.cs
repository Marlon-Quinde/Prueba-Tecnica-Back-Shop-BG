using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers
{
    public class ExceptionResponse: Exception
    {
        public ExceptionResponse(string error): base(error)
        {
            
        }
    }
}
