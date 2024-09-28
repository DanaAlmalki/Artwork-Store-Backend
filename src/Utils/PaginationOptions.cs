using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend_Teamwork.src.Utils
{
    public class PaginationOptions
    {
        public int Limit { get; set; } = 0;
        public int Offset { get; set; } = 2;
        public string Search { get; set; } = string.Empty;
    }
}
