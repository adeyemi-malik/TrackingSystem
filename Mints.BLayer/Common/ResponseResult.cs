using System;
using System.Collections.Generic;
using System.Text;

namespace Mints.BLayer.Models.Common
{
    public class ResponseResult
    {
        public ResponseResult()
        {
            Data = new Dictionary<string, string>();
        }

        public Status Status { get; set; }

        public IDictionary<string, string> Data { get; set; }
    }

    public enum Status
    {
        Success,
        Fail
    }
}
