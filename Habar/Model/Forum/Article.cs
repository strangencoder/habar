using Model.Forum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Article
    {
        public int Id { get; set; }
        public int PublisherId { get; set; }
        public int ThemeId { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        IEnumerable<Tag> Tags { get; set; }
        public DateTime PublishDate { get; set; }
        public bool IsEnable { get; set; }
    }
}
