using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CookComputing.XmlRpc;
using DocNetPress.XmlRpc.Posts;
using DocNetPress.XmlRpc.Media;

namespace DocNetPress.XmlRpc
{
    public interface IWordPress : IXmlRpcProxy
    {
        #region Posts

        [XmlRpcMethod("wp.getPost")]
        Post GetPost(
                [XmlRpcParameter("blog_id")]
                int blogID,
                String username,
                String password,
                [XmlRpcParameter("post_id")]
                int postID
            );

        [XmlRpcMethod("wp.getPost")]
        Post GetPost(
                [XmlRpcParameter("blog_id")]
                int blogID, 
                String username, 
                String password,
                [XmlRpcParameter("post_id")]
                int postID,
                CustomField[] fields
            );

        [XmlRpcMethod("wp.getPosts")]
        Post[] GetPosts(
                [XmlRpcParameter("blog_id")]
                int blogID, 
                String username, 
                String password
            );

        [XmlRpcMethod("wp.getPosts")]
        Post[] GetPosts(
                [XmlRpcParameter("blog_id")]
                int blogID,
                String username,
                String password,
                PostFilter filter
            );

        [XmlRpcMethod("wp.getPosts")]
        Post[] GetPosts(
                [XmlRpcParameter("blog_id")]
                int blogID, 
                String username, 
                String password,
                CustomField[] fields
            );

        [XmlRpcMethod("wp.getPosts")]
        Post[] GetPosts(
                [XmlRpcParameter("blog_id")]
                int blogID,
                String username,
                String password,
                PostFilter filter,
                CustomField[] fields
            );

        [XmlRpcMethod("wp.newPost")]
        String NewPost(
                [XmlRpcParameter("blog_id")]
                int blogID,
                string username,
                string password,
                PostInsert content
            );

        [XmlRpcMethod("wp.editPost")]
        bool EditPost(
                [XmlRpcParameter("blog_id")]
                int blogID,
                string username,
                string password,
                [XmlRpcParameter("post_id")]
                int postID, 
                PostInsert content
            );

        [XmlRpcMethod("wp.deletePost")]
        bool DeletePost(
                [XmlRpcParameter("blog_id")]
                int blogID,
                string username,
                string password,
                [XmlRpcParameter("post_id")]
                int postID
            );

        #endregion

        #region Media

        [XmlRpcMethod("wp.getMediaItem")]
        MediaItem GetMediaItem(
                [XmlRpcParameter("blog_id")]
                int blogID,
                string username,
                string password,
                [XmlRpcParameter("attachment_id")]
                int attachmentID
            );

        [XmlRpcMethod("wp.getMediaLibrary")]
        MediaItem[] GetMediaLibrary(
                [XmlRpcParameter("blog_id")]
                int blogID, 
                String username, 
                String password,
                MediaLibraryFilter filter
            );

        [XmlRpcMethod("wp.uploadFile")]
        MediaItemUploadResult UploadFile(
                [XmlRpcParameter("blogid")]
                int blogID,
                String username,
                String password,
                MediaItemUploadData data
            );

        #endregion

        #region Taxonomies

        #endregion
    }
}
