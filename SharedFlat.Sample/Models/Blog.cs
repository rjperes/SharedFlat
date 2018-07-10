using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharedFlat.Sample.Models
{
    [Table(nameof(Blog))]
    public class Blog : ITenantEntity
    {
        public Blog(int blogId, string name, DateTime creationDate, string url, string author)
        {
            this.BlogId = blogId;
            this.Name = name;
            this.CreationDate = creationDate;
            this.Url = url;
            this.Author = author;
        }

        public int BlogId { get; set; }
        [Required]
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        [Required]
        public string Url { get; set; }
        [Required]
        public string Author { get; set; }
        public virtual ICollection<Post> Posts { get; } = new HashSet<Post>();
        public override string ToString() => Name;
    }
}
