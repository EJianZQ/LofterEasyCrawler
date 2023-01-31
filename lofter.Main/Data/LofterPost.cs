using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lofter.Main.Data
{
    public class SinglePost
    {
        public int Day { get; set; }
        public int Month { get; set; }
        public string? Text { get; set; }
        public string? ImgUrl { get; set; }
        public string? Tag { get; set; }
    }

    public class PostCollection
    {
        public PostCollection(int index,List<SinglePost> posts)
        {
            this.PageIndex = index;
            this.Posts = posts;
        }
        public int PageIndex { get; set; }
        public List<SinglePost> Posts { get; set; }
    }
}
