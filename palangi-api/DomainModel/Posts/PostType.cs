using System;
using System.Collections.Generic;
using System.Text;

namespace DomainModel.Posts
{
   public  class PostType
    {
        public Guid PostTypeId { get; set; }
        public string Title { get; set; }
        public string Extra { get; set; }
    }
}
