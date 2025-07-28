using ContentLoop.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentLoop.BLL.Models
{
    public class ArticleModel
    {
        public Guid Id { get; set; }
        public Guid AuthorId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Description { get; set; }
        public string AuthorName { get; set; }
        public DateTime CreatedAt { get; set; }
        public int ViewsCount { get; set; }
    }
}
