using DocNetPress.Development.Generator;
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
        private readonly List<IPostElement> _PageElements = new List<IPostElement>();

        /// <summary>
        /// List containing all <see cref="DocNetPress.Development.Generator.IPostElement"/>s responsible for property pages
        /// </summary>
        /// <remarks>
        /// The order of the elements in this list also gives the final order of the elements in the WordPress post
        /// </remarks>
        public List<IPostElement> PageElements
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
        /// Initializes an empty <see cref="DocNetPress.Generators.PostGenerator"/>-Instance
        /// </summary>
        public PostGenerator()
        {
            this.InitializeDefaultPageStructure();
        }

        /// <summary>
        /// Initializes a new <see cref="DocNetPress.Generators.PostGenerator"/>-Instance and sets the path to the documentation file
        /// </summary>
        /// <param name="documentationFileName">The path to the documentation file read by the <see cref="DocNetPress.Generators.PostGenerator"/></param>
        public PostGenerator(String documentationFileName) : this()
        {
            this.DocumentationFile = documentationFileName;
        }

        /// <summary>
        /// Initializes a new <see cref="DocNetPress.Generators.PostGenerator"/>-Instance and sets the path to the documentation file and the post type
        /// </summary>
        /// <param name="documentationFileName">The path to the documentation file read by the <see cref="DocNetPress.Generators.PostGenerator"/></param>
        /// <param name="postType">Whether the generator shall generate WordPress pages, posts or an entirely custom taxonomy</param>
        public PostGenerator(String documentationFileName, PostType postType)
            : this(documentationFileName)
        {
            this.PostType = postType;
        }

        /// <summary>
        /// Initializes a new <see cref="DocNetPress.Generators.PostGenerator"/>-Instance and sets the path to the documentation file, the post type and
        /// the custom taxonomy name
        /// </summary>
        /// <param name="documentationFileName">The path to the documentation file read by the <see cref="DocNetPress.Generators.PostGenerator"/></param>
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
        /// Adds an <see cref="DocNetPress.Generators.IPageElement"/> to the list of generators
        /// </summary>
        /// <param name="element">The <see cref="DocNetPress.Generators.IPageElement"/> to add</param>
        public void AddPageElement(IPostElement element)
        {
            this.PageElements.Add(element);
        }

        /// <summary>
        /// Adds a range of <see cref="DocNetPress.Generators.IPageElement"/> to the list of generators
        /// </summary>
        /// <param name="element">The <see cref="DocNetPress.Generators.IPageElement"/>s to add</param>
        public void AddPageElement(IEnumerable<IPostElement> elements)
        {
            this.PageElements.AddRange(elements);
        }

        /// <summary>
        /// Adds an <see cref="DocNetPress.Generators.IPageElement"/> to the list of generators at a given index
        /// </summary>
        /// <param name="element">The <see cref="DocNetPress.Generators.IPageElement"/> to add</param>
        public void AddPageElement(int index, IPostElement element)
        {
            this.PageElements.Insert(index, element);
        }

        /// <summary>
        /// Adds a range of <see cref="DocNetPress.Generators.IPageElement"/> to the list of generators at a given index
        /// </summary>
        /// <param name="element">The <see cref="DocNetPress.Generators.IPageElement"/>s to add</param>
        public void AddPageElement(int index, IEnumerable<IPostElement> elements)
        {
            this.PageElements.InsertRange(index, elements);
        }

        /// <summary>
        /// Sets the default page structure
        /// </summary>
        private void InitializeDefaultPageStructure()
        {
            
        }
    }
}
