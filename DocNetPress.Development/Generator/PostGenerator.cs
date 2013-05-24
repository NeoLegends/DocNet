using DocNetPress.Development.Generator.Extensions;
using DocNetPress.Development.XmlRpc.Posts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocNetPress.Development.Generator
{
    /// <summary>
    /// Generates WordPress-Pages from a given .NET-XML-Documentation file
    /// </summary>
    [Serializable]
    public class PostGenerator
    {
        /// <summary>
        /// The path to the documentation file
        /// </summary>
        public String DocumentationFile { get; set; }

        /// <summary>
        /// Backing field for PropertyPageElements
        /// </summary>
        private readonly List<IPageElement> _PageElements = new List<IPageElement>();

        /// <summary>
        /// List containing all <see cref="DocNetPress.Development.Generator.IPostElement"/>s responsible for property pages
        /// </summary>
        /// <remarks>
        /// The order of the elements in this list also gives the final order of the elements in the WordPress post
        /// </remarks>
        public List<IPageElement> PageElements
        {
            get
            {
                return _PageElements;
            }
        }

        /// <summary>
        /// Backing field for PostType
        /// </summary>
        private PostType _PostType = PostType.Page;

        /// <summary>
        /// Whether the generated post will be a page, a post or something entirely custom
        /// </summary>
        public PostType PostType
        {
            get
            {
                return _PostType;
            }
            set
            {
                _PostType = value;
            }
        }

        /// <summary>
        /// If we're dealing with a custom post type, insert it's name here
        /// </summary>
        public String CustomPostTypeName { get; set; }

        /// <summary>
        /// The <see cref="DocNetPress.Generator.Extensions.IPageElement"/> at the specific index
        /// </summary>
        /// <param name="index">The index of the <see cref="DocNetPress.Generator.Extensions.IPageElement"/></param>
        /// <returns>The <see cref="DocNetPress.Generator.Extensions.IPageElement"/> at the given index</returns>
        public IPageElement this[int index]
        {
            get
            {
                return this.PageElements[index];
            }
            set
            {
                this.PageElements[index] = value;
            }
        }

        /// <summary>
        /// Initializes an empty <see cref="DocNetPress.Generator.PostGenerator"/>-Instance
        /// </summary>
        public PostGenerator()
        {
            this.InitializeDefaultPageStructure();
        }

        /// <summary>
        /// Initializes a new <see cref="DocNetPress.Generator.PostGenerator"/>-Instance and sets the path to the documentation file
        /// </summary>
        /// <param name="documentationFileName">The path to the documentation file read by the <see cref="DocNetPress.Generator.PostGenerator"/></param>
        public PostGenerator(String documentationFileName) : this()
        {
            this.DocumentationFile = documentationFileName;
        }

        /// <summary>
        /// Initializes a new <see cref="DocNetPress.Generator.PostGenerator"/>-Instance and sets the path to the documentation file and the post type
        /// </summary>
        /// <param name="documentationFileName">The path to the documentation file read by the <see cref="DocNetPress.Generator.PostGenerator"/></param>
        /// <param name="postType">Whether the generator shall generate WordPress pages, posts or an entirely custom taxonomy</param>
        public PostGenerator(String documentationFileName, PostType postType)
            : this(documentationFileName)
        {
            this.PostType = postType;
        }

        /// <summary>
        /// Initializes a new <see cref="DocNetPress.Generator.PostGenerator"/>-Instance and sets the path to the documentation file, the post type and
        /// the custom taxonomy name
        /// </summary>
        /// <param name="documentationFileName">The path to the documentation file read by the <see cref="DocNetPress.Generator.PostGenerator"/></param>
        /// <param name="postType">Whether the generator shall generate WordPress pages, posts or an entirely custom taxonomy</param>
        /// <param name="customPostTypeName">The name of the taxonomy to insert the posts into</param>
        public PostGenerator(String documentationFileName, PostType postType, String customPostTypeName)
            : this(documentationFileName, postType)
        {
            this.CustomPostTypeName = customPostTypeName;
        }

        /// <summary>
        /// Extracts the XML file and generates <see cref="DocNetPress.XmlRpc.Posts.Post"/>-Instances from the documentation ready to push
        /// to a WordPress installation
        /// </summary>
        /// <returns>The generates <see cref="DocNetPress.XmlRpc.Posts.Post"/>s containing a well-formatted documentation</returns>
        public PostInsert[] Generate()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Adds an <see cref="DocNetPress.Generator.Extensions.IPageElement"/> to the list of generators
        /// </summary>
        /// <param name="element">The <see cref="DocNetPress.Generator.Extensions.IPageElement"/> to add</param>
        public void AddPageElement(IPageElement element)
        {
            this.PageElements.Add(element);
        }

        /// <summary>
        /// Adds a range of <see cref="DocNetPress.Generator.Extensions.IPageElement"/> to the list of generators
        /// </summary>
        /// <param name="element">The <see cref="DocNetPress.Generator.Extensions.IPageElement"/>s to add</param>
        public void AddPageElement(IEnumerable<IPageElement> elements)
        {
            this.PageElements.AddRange(elements);
        }

        /// <summary>
        /// Adds an <see cref="DocNetPress.Generator.Extensions.IPageElement"/> to the list of generators at a given index
        /// </summary>
        /// <param name="element">The <see cref="DocNetPress.Generator.Extensions.IPageElement"/> to add</param>
        public void AddPageElement(int index, IPageElement element)
        {
            this.PageElements.Insert(index, element);
        }

        /// <summary>
        /// Adds a range of <see cref="DocNetPress.Generator.Extensions.IPageElement"/> to the list of generators at a given index
        /// </summary>
        /// <param name="element">The <see cref="DocNetPress.Generator.Extensions.IPageElement"/>s to add</param>
        public void AddPageElement(int index, IEnumerable<IPageElement> elements)
        {
            this.PageElements.InsertRange(index, elements);
        }

        /// <summary>
        /// Removes all <see cref="DocNetPress.Generator.Extensions.IPageElement"/>s
        /// </summary>
        public void ClearPageElements()
        {
            this.PageElements.Clear();
        }

        /// <summary>
        /// Sets the default page structure
        /// </summary>
        private void InitializeDefaultPageStructure()
        {
            
        }
    }
}
