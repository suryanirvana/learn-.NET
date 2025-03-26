using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace dotback.Models
{
    public class Comment
    {
        // comment ID
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; } = DateTime.Now;

        // foreign key
        public int? StockId { get; set; }

        // navigator
        public Stock? Stock { get; set; }
    }
}