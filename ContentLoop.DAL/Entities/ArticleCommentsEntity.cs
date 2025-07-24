using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentLoop.DAL.Entities
{
    [Table("ArticleComments")]
    public class ArticleCommentsEntity
    {
        public Guid Id { get; set; }
        public Guid ArticleId { get; set; }
        public Guid AuthorId { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
