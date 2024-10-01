using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend_Teamwork.src.Utils
{
    public class PaginationOptions
    {
        // Pagination
        public int Limit { get; set; } = 2;
        public int Offset { get; set; } = 0;
        // Search
        public string Search { get; set; } = string.Empty;
        // Sort
        public string SortOrder { get; set; } = string.Empty; // "", "name_desc", "date_desc", "date", "price_desc", "price"
        // Price range
        public double? LowPrice { get; set; } = 0;
        public double? HighPrice { get; set; } = 10000;
        // Date range
        public DateTime? StartDate { get; set; } = DateTime.MinValue;
        public DateTime? EndDate { get; set; } = DateTime.Now;
    }
}
