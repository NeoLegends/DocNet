using DocNetPress.XmlRpc.Posts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocNetPress.Generators
{
    public class PageGenerator
    {
        public String DocumentationFile { get; set; }

        public PageGenerator(String documentationFileName)
        {

        }

        public Post[] GeneratePosts()
        {
            lock (DocumentationFile)
            {
                return null;
            }
        }
    }
}
