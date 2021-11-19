using System;
using System.Collections.Generic;

namespace Eshop.Data
{
    public partial class Article
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Image { get; set; }
        public int? IdComment { get; set; }

        public virtual Comment IdCommentNavigation { get; set; }
    }
}
