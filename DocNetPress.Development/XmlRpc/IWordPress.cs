using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CookComputing.XmlRpc;
using DocNetPress.XmlRpc.Posts;
using DocNetPress.XmlRpc.Media;
using DocNetPress.XmlRpc.Taxonomies;

namespace DocNetPress.XmlRpc
{
    /// <summary>
    /// Interface used to communicate with WordPress's XML-RPC interface
    /// </summary>
    /// <remarks>
    /// This interface does not contain the full spectrum of WordPress's XML-RPC API, it only
    /// contains the required members for DocNetPress to work
    /// </remarks>
    public interface IWordPress : IXmlRpcProxy
    {
        #region Posts

        /// <summary>
        /// Gets a specific post
        /// </summary>
        /// <param name="blogID">The ID of the blog to get the post from</param>
        /// <param name="username">The username to log in with</param>
        /// <param name="password">The password of the user</param>
        /// <param name="postID">The ID of the post to get</param>
        /// <returns>a <see cref="DocNetPress.XmlRpc.Posts.Post"/> containing the post information</returns>
        [XmlRpcMethod("wp.getPost")]
        PostRetreive GetPost(
                [XmlRpcParameter("blog_id")]
                int blogID,
                String username,
                String password,
                [XmlRpcParameter("post_id")]
                int postID
            );

        /// <summary>
        /// Gets a specific post filtered by post fields
        /// </summary>
        /// <param name="blogID">The ID of the blog to get the post from</param>
        /// <param name="username">The username to log in with</param>
        /// <param name="password">The password of the user</param>
        /// <param name="postID">The ID of the post to get</param>
        /// <param name="fields">The <see cref="DocNetPress.XmlRpc.Posts.CustomField"/>s to filter the posts by</param>
        /// <returns>A <see cref="DocNetPress.XmlRpc.Posts.Post"/> containing the post information</returns>
        [XmlRpcMethod("wp.getPost")]
        PostRetreive GetPost(
                [XmlRpcParameter("blog_id")]
                int blogID, 
                String username, 
                String password,
                [XmlRpcParameter("post_id")]
                int postID,
                CustomField[] fields
            );

        /// <summary>
        /// Gets all posts of a specific blog
        /// </summary>
        /// <param name="blogID">The ID of the blog to get the posts from</param>
        /// <param name="username">The username to log in with</param>
        /// <param name="password">The password of the user</param>
        /// <returns>An array of <see cref="DocNetPress.XmlRpc.Posts.Post"/>s with all posts the blog currently has</returns>
        [XmlRpcMethod("wp.getPosts")]
        PostRetreive[] GetPosts(
                [XmlRpcParameter("blog_id")]
                int blogID, 
                String username, 
                String password
            );

        /// <summary>
        /// Gets all posts that match a specific <see cref="DocNetPress.XmlRpc.Posts.PostFilter"/>
        /// </summary>
        /// <param name="blogID">The ID of the blog to get the posts from</param>
        /// <param name="username">The username to log in with</param>
        /// <param name="password">The password of the user</param>
        /// <param name="filter">The <see cref="DocNetPress.XmlRpc.Posts.PostFilter"/> filtering the results</param>
        /// <returns>An array of <see cref="DocNetPress.XmlRpc.Posts.Post"/>s with all matches</returns>
        [XmlRpcMethod("wp.getPosts")]
        PostRetreive[] GetPosts(
                [XmlRpcParameter("blog_id")]
                int blogID,
                String username,
                String password,
                PostFilter filter
            );

        /// <summary>
        /// Gets all posts that match specific <see cref="DocNetPress.XmlRpc.Posts.CustomField"/>s
        /// </summary>
        /// <param name="blogID">The ID of the blog to get the posts from</param>
        /// <param name="username">The username to log in with</param>
        /// <param name="password">The password of the user</param>
        /// <param name="fields">The <see cref="DocNetPress.XmlRpc.Posts.CustomField"/>s to filter the posts by</param>
        /// <returns>An array of <see cref="DocNetPress.XmlRpc.Posts.Post"/>s with all matches</returns>
        [XmlRpcMethod("wp.getPosts")]
        PostRetreive[] GetPosts(
                [XmlRpcParameter("blog_id")]
                int blogID, 
                String username, 
                String password,
                CustomField[] fields
            );

        /// <summary>
        /// Gets all posts that match specific <see cref="DocNetPress.XmlRpc.Posts.CustomField"/>s and a <see cref="DocNetPress.XmlRpc.Posts.PostFilter"/>
        /// </summary>
        /// <param name="blogID">The ID of the blog to get the posts from</param>
        /// <param name="username">The username to log in with</param>
        /// <param name="password">The password of the user</param>
        /// <param name="fields">The <see cref="DocNetPress.XmlRpc.Posts.CustomField"/>s to filter the posts by</param>
        /// <param name="filter">The <see cref="DocNetPress.XmlRpc.Posts.PostFilter"/> to filter the posts by</param>
        /// <returns>An array of <see cref="DocNetPress.XmlRpc.Posts.Post"/>s with all matches</returns>
        [XmlRpcMethod("wp.getPosts")]
        PostRetreive[] GetPosts(
                [XmlRpcParameter("blog_id")]
                int blogID,
                String username,
                String password,
                PostFilter filter,
                CustomField[] fields
            );

        /// <summary>
        /// Inserts a new post into WordPress
        /// </summary>
        /// <param name="blogID">The ID of the blog to insert the post into</param>
        /// <param name="username">The username to log in with</param>
        /// <param name="password">The password of the user</param>
        /// <param name="content">The <see cref="DocNetPress.XmlRpc.Posts.PostInsert"/> with all required WordPress-Post information</param>
        /// <returns>The ID of the newly created post</returns>
        [XmlRpcMethod("wp.newPost")]
        String NewPost(
                [XmlRpcParameter("blog_id")]
                int blogID,
                String username,
                String password,
                PostInsert content
            );

        /// <summary>
        /// Edits a specific post
        /// </summary>
        /// <param name="blogID">The ID of the blog to get the post from</param>
        /// <param name="username">The username to log in with</param>
        /// <param name="password">The password of the user</param>
        /// <param name="postID">The unique ID of the post to edit</param>
        /// <param name="content">The <see cref="DocNetPress.XmlRpc.Posts.PostInsert"/> with all required WordPress-Post information</param>
        /// <returns>Whether the edit was successful or not</returns>
        [XmlRpcMethod("wp.editPost")]
        bool EditPost(
                [XmlRpcParameter("blog_id")]
                int blogID,
                String username,
                String password,
                [XmlRpcParameter("post_id")]
                int postID, 
                PostInsert content
            );

        /// <summary>
        /// Deletes a specific post
        /// </summary>
        /// <param name="blogID">The ID of the blog to get the post from</param>
        /// <param name="username">The username to log in with</param>
        /// <param name="password">The password of the user</param>
        /// <param name="postID">The unique ID of the post to delete</param>
        /// <returns>Whether the deletion was successful or not</returns>
        [XmlRpcMethod("wp.deletePost")]
        bool DeletePost(
                [XmlRpcParameter("blog_id")]
                int blogID,
                String username,
                String password,
                [XmlRpcParameter("post_id")]
                int postID
            );

        #endregion

        #region Media

        /// <summary>
        /// Gets a specific media item
        /// </summary>
        /// <param name="blogID">The ID of the blog to get the media item from</param>
        /// <param name="username">The username to log in with</param>
        /// <param name="password">The password of the user</param>
        /// <param name="attachmentID">The media item's ID</param>
        /// <returns>A <see cref="DocNetPress.XmlRpc.Media.MediaItem"/> containing the data of the received item</returns>
        [XmlRpcMethod("wp.getMediaItem")]
        MediaItem GetMediaItem(
                [XmlRpcParameter("blog_id")]
                int blogID,
                String username,
                String password,
                [XmlRpcParameter("attachment_id")]
                int attachmentID
            );

        /// <summary>
        /// Gets all items in the media library
        /// </summary>
        /// <param name="blogID">The ID of the blog to get the media items from</param>
        /// <param name="username">The username to log in with</param>
        /// <param name="password">The password of the user</param>
        /// <param name="filter">The <see cref="DocNetPress.XmlRpc.Media.MediaLibraryFilter"/> to filter the media items by</param>
        /// <returns>The filtered list of media items</returns>
        [XmlRpcMethod("wp.getMediaLibrary")]
        MediaItem[] GetMediaLibrary(
                [XmlRpcParameter("blog_id")]
                int blogID, 
                String username, 
                String password,
                MediaLibraryFilter filter
            );

        /// <summary>
        /// Uploads a specific media item to WordPress
        /// </summary>
        /// <param name="blogID">The ID of the blog to upload the media items to</param>
        /// <param name="username">The username to log in with</param>
        /// <param name="password">The password of the user</param>
        /// <param name="data">The <see cref="DocNetPress.XmlRpc.Media.MediaItemUploadData"/> containing the data to be uploaded</param>
        /// <returns>A <see cref="DocNetPress.XmlRpc.Media.MediaItemUploadResult"/>-instance containing the link to the uploaded item, and some further data</returns>
        [XmlRpcMethod("wp.uploadFile")]
        MediaItemUploadResult UploadMediaItem(
                [XmlRpcParameter("blogid")]
                int blogID,
                String username,
                String password,
                MediaItemUploadData data
            );

        #endregion

        #region Taxonomies

        /// <summary>
        /// Gets a specific taxonomy
        /// </summary>
        /// <param name="blogID">The ID of the blog to get the taxonomy entry from</param>
        /// <param name="username">The username to log in with</param>
        /// <param name="password">The password of the user</param>
        /// <param name="taxonomy">The name of the taxonomy</param>
        /// <returns>A <see cref="DocNetPress.XmlRpc.Taxonomies.Taxonomy"/>-Instance with the information about the received taxonomy</returns>
        [XmlRpcMethod("wp.getTaxonomy")]
        Taxonomy GetTaxonomy(
                [XmlRpcParameter("blog_id")]
                int blogID,
                String username,
                String password,
                String taxonomy
            );

        /// <summary>
        /// Gets all taxonomies of a specific blog
        /// </summary>
        /// <param name="blogID">The ID of the blog to get the taxonomy entries from</param>
        /// <param name="username">The username to log in with</param>
        /// <param name="password">The password of the user</param>
        /// <returns>An array of <see cref="DocNetPress.XmlRpc.Taxonomies.Taxonomy"/>s</returns>
        [XmlRpcMethod("wp.getTaxonomies")]
        Taxonomy[] GetTaxonomies(
                [XmlRpcParameter("blog_id")]
                int blogID,
                String username,
                String password
            );

        /// <summary>
        /// Gets a specific taxonomy term by it's ID
        /// </summary>
        /// <param name="blogID">The ID of the blog to get the term entry from</param>
        /// <param name="username">The username to log in with</param>
        /// <param name="password">The password of the user</param>
        /// <param name="termID">The ID of the taxonomy term to get</param>
        /// <returns>A <see cref="DocNetPress.XmlRpc.Taxonomies.TermReceive"/>-Instance containing information about the received term</returns>
        [XmlRpcMethod("wp.getTerm")]
        TermRetreive GetTerm(
                [XmlRpcParameter("blog_id")]
                int blogID,
                String username,
                String password,
                [XmlRpcParameter("term_id")]
                int termID
            );

        /// <summary>
        /// Gets all terms of a specific taxonomy
        /// </summary>
        /// <param name="blogID">The ID of the blog to get the terms from</param>
        /// <param name="username">The username to log in with</param>
        /// <param name="password">The password of the user</param>
        /// <param name="taxonomy">The taxonomy to get the terms from</param>
        /// <returns>An array of <see cref="DocNetPress.XmlRpc.Taxonomies.TermReceive"/>-Instances</returns>
        [XmlRpcMethod("wp.getTerms")]
        TermRetreive[] GetTerms(
                [XmlRpcParameter("blog_id")]
                int blogID,
                String username,
                String password,
                String taxonomy
            );

        /// <summary>
        /// Gets all terms of a specific taxonomy filtered by a <see cref="DocNetPress.XmlRpc.Taxonomies.TermFilter"/>
        /// </summary>
        /// <param name="blogID">The ID of the blog to get the terms from</param>
        /// <param name="username">The username to log in with</param>
        /// <param name="password">The password of the user</param>
        /// <param name="taxonomy">The taxonomy to get the terms from</param>
        /// <param name="filter">A <see cref="DocNetPress.XmlRpc.Taxonomies.TermFilter"/>-Instance filtering the result</param>
        /// <returns>
        /// An array of <see cref="DocNetPress.XmlRpc.Taxonomies.TermReceive"/>-Instances filtered by the given <see cref="DocNetPress.XmlRpc.Taxonomies.TermFilter"/>
        /// </returns>
        [XmlRpcMethod("wp.getTerms")]
        TermRetreive[] GetTerms(
                [XmlRpcParameter("blog_id")]
                int blogID,
                String username,
                String password,
                String taxonomy,
                TermFilter filter
            );

        /// <summary>
        /// Adds a new taxonomy term
        /// </summary>
        /// <param name="blogID">The ID of the blog to insert the new term into</param>
        /// <param name="username">The username to log in with</param>
        /// <param name="password">The password of the user</param>
        /// <param name="content">The <see cref="DocNetPress.XmlRpc.Taxonomies.TermInsert"/>-Instance containing all required information about the newly created term</param>
        /// <returns>The unique ID of the newly created taxonomy term</returns>
        [XmlRpcMethod("wp.newTerm")]
        String NewTerm(
                [XmlRpcParameter("blog_id")]
                int blogID,
                String username,
                String password,
                TermInsert content
            );

        /// <summary>
        /// Edits a given taxonomy term
        /// </summary>
        /// <param name="blogID">The ID of the blog of the term to edit</param>
        /// <param name="username">The username to log in with</param>
        /// <param name="password">The password of the user</param>
        /// <param name="termID">The ID of the taxonomy term to edit</param>
        /// <param name="content">A <see cref="DocNetPress.XmlRpc.Taxonomies.TermInsert"/>-Instance that replaces the term's information</param>
        /// <returns>Whether the edit was successful or not</returns>
        [XmlRpcMethod("wp.editTerm")]
        bool EditTerm(
                [XmlRpcParameter("blog_id")]
                int blogID,
                String username,
                String password,
                [XmlRpcParameter("term_id")]
                int termID,
                TermInsert content
            );

        /// <summary>
        /// Deletes a specific taxonomy term
        /// </summary>
        /// <param name="blogID">The ID of the blog of the term to edit</param>
        /// <param name="username">The username to log in with</param>
        /// <param name="password">The password of the user</param>
        /// <param name="termID">The ID of the taxonomy term to delete</param>
        /// <returns>Whether the deletion was successful or not</returns>
        [XmlRpcMethod("wp.deleteTerm")]
        bool DeleteTerm(
                [XmlRpcParameter("blog_id")]
                int blogID,
                String username,
                String password,
                [XmlRpcParameter("term_id")]
                int termID
            );

        #endregion
    }
}
