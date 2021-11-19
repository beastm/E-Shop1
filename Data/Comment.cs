using System;
using System.Collections.Generic;

namespace Eshop.Data
{
    public partial class Comment
    {
        public Comment()
        {
            Article = new HashSet<Article>();
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }

        public virtual ICollection<Article> Article { get; set; }
    }
}
