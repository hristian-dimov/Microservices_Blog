using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogSystem.API.Domain
{
    [Table("PostCategory")]
    public class PostCategory
    {
        public Guid PostId { get; set; }

        public Guid CategoryId { get; set; }

        public virtual Post Post { get; set; }

        public virtual Category Category { get; set; }
    }
}