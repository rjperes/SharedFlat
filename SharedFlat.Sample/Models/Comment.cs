using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharedFlat.Sample
{
    [Table(nameof(Comment))]
    public class Comment
    {
        public int CommentId { get; set; }
        public virtual Post Post { get; set; }
        public DateTime Timestamp { get; set; }
        public string Text { get; set; }
    }
}
