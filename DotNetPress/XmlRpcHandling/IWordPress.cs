using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CookComputing.XmlRpc;

namespace DotNetPress.XmlRpcHandling
{
    public interface IWordPress : IXmlRpcProxy
    {
        [XmlRpcMethod("wp.getPost")]
        Post GetPost(
                [XmlRpcMember("blog_id")]
                int blogID, 
                String username, 
                String password,
                [XmlRpcMember("post_id")]
                int postID, 
                CustomField[] fields
            );

        [XmlRpcMethod("wp.getPosts")]
        Post[] GetPosts(
                [XmlRpcMember("blog_id")]
                int blogID, 
                String username, 
                String password, 
                Filter filter
            );

        String InsertPost(
                int blog_id,
                string username,
                string password,
            );
    }
}
