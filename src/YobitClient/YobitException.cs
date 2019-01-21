using System;
using System.Collections.Generic;
using System.Text;

namespace YobitClient
{
    public class YobitException : Exception
    {
        public YobitException(string message) : base(message)
        {
            
        }
    }
}
