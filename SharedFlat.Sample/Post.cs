using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharedFlat.Sample
{
    [Table(nameof(Post))]
    public class Post : ITenantEntity
    {
        public int PostId { get; set; }
        [Required]
        public virtual Blog Blog { get; set; }
        public DateTime Timestamp { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Body { get; set; }
        public override string ToString() => Title;
        public State State { get; set; }
    }
}
