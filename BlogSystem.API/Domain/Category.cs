using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogSystem.API.Domain
{
    [Table("Category")]
    public class Category
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}
