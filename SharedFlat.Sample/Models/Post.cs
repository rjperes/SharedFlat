using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharedFlat.Sample
{
    [Table(nameof(Post))]
    public class Post
    {
        public int BlogId { get; set; }
        public int PostId { get; set; }
        public virtual Blog Blog { get; set; }
        public string Title { get; set; }
        public DateTime Timestamp { get; set; }
        public string Body { get; set; }
        public string Url { get; set; }
        public virtual List<Comment> Comments { get; set; } = new List<Comment>();
    }
}