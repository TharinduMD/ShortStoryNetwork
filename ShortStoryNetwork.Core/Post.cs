using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShortStoryNetwork.Core
{
    [Table("Post")]
    public class Post
    {
        [Key]
        public string PostId { get; set; }
        public string UserId { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public string Posts { get; set; } = string.Empty;
    }
}
