using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using YobitClient.Models;

namespace YobitClient
{
    public class YobitResponse<T>
    {
        public T Result { get; set; }
        public string RawJson { get; set; }
    }
}
