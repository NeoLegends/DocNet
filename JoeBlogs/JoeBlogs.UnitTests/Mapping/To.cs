using System;
using NUnit.Framework;

namespace JoeBlogs.Tests.Mapping
{
    public class To
    {
        [Test]
        public void can_map_to_author_from_xmlRpcAuthor()
        {
            var author = new XmlRpcAuthor();
            author.display_name = "Joe Blogs";
            author.user_login = "joeBlogs";
            author.user_email = "joe@blogs.com";
            author.user_id = "1234";

            var result = Map.To.Author(author);

            Assert.AreEqual(author.display_name, result.DisplayName);
            Assert.AreEqual(author.user_email, result.EmailAddress);
            Assert.AreEqual(author.user_login, result.LoginName);
            Assert.AreEqual(author.user_id, result.UserID);
        }

        [Test]
        public void can_map_to_category_from_xmlRpcCategory()
        {
            var category = new XmlRpcCategory()
                               {
                                   categoryId = "1234",
                                   categoryName = "Test Category Name",
                                   description = "This is a test category",
                                   htmlUrl = "www.test.com/cat",
                                   parentId = "11",
                                   rssUrl = "www.testrssurl.com",
                                   title = "Test Category"
                               };

            var result = Map.To.Category(category);

            Assert.AreEqual(category.categoryId, result.CategoryID.ToString());
            Assert.AreEqual(category.parentId, result.ParentCategoryID.ToString());
            Assert.AreEqual(category.categoryName, result.Name);
            Assert.AreEqual(category.description, result.Description);
            Assert.AreEqual(category.htmlUrl, result.HtmlUrl);
            Assert.AreEqual(category.rssUrl, result.RSSUrl);
            Assert.AreEqual(category.title, result.Name);
        }

        [Test]
        public void can_map_to_comment_from_xmlRpcComment()
        {
            var statusEnum = CommentStatus.Approve;
            var statusEnumName = Enum.GetName(typeof(CommentStatus), statusEnum);

            var xmlRpcComment = new XmlRpcComment
                                    {
                                        author = "test author",
                                        author_email = "test@test.com",
                                        author_url = "www.test.com",
                                        parent = "0",
                                        content = "This is some test content",
                                        post_id = "234",
                                    };

            var result = Map.To.Comment(xmlRpcComment);

            Assert.AreEqual(xmlRpcComment.author, result.AuthorName);
            Assert.AreEqual(xmlRpcComment.author_email, result.AuthorEmail);
            Assert.AreEqual(xmlRpcComment.author_url, result.AuthorUrl);
            Assert.AreEqual(xmlRpcComment.parent, result.ParentCommentID);
            Assert.AreEqual(xmlRpcComment.content, result.Content);
        }
    }
}