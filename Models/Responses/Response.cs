using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Responses
{
    public class Response
    {
        public string Code {  get; set; }
        public string Message { get; set; }

        public object Data { get; set; }

    }
}
