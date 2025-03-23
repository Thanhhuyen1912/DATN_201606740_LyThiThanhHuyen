using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Result
{
    public class CResult
    {
        public int Code { get; set; }

        public string Message { get; set; }

        public object Data { get; set; }
        public CResult() { }
        public CResult(int code, string message, object data)
        {
            Code = code;
            Message = message;
            Data = data;
        }
        public CResult(int code, string message)
        {
            Code = code;
            Message = message;
        }
    }
}
