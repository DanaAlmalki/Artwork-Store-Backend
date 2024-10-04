using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend_Teamwork.src.Utils
{
    public class PaginationOptions
    {
        // Pagination
        public int PageSize { get; set; } = 2;
        public int PageNumber { get; set; } = 1;

        // Search
        public string Search { get; set; } = string.Empty;

        // Sort
        public string SortOrder { get; set; } = string.Empty; // "", "name_desc", "date_desc", "date", "price_desc", "price"

        // Price range
        public decimal? LowPrice { get; set; } = 0;
        public decimal? HighPrice { get; set; } = 10000;

        // Date range !! give an error in postgresql
        //public DateTime? StartDate { get; set; } = DateTime.MinValue;
        //public DateTime? EndDate { get; set; } = DateTime.Now;
    }
}
