using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomerOrderProject.Utility
{
    public class JsonMessage
    {
        public string Result;
        public string Message;

        public JsonMessage(string Result, string Message)
        {
            this.Result = Result;
            this.Message = Message;
        }
    }
}