using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharedFlat.Sample
{
    [Table(nameof(Blog))]
    public class Blog
    {
        public int BlogId { get; set; }
        public virtual List<Post> Posts { get; set; } = new List<Post>();
        public string Name { get; set; }
        public string Url { get; set; }
        public DateTime Creation { get; set; }
    }
}