using DocNetPress.XmlRpc.Posts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocNetPress.Generators
{
    /// <summary>
    /// Generates WordPress-Pages from a given .NET-XML-Documentation file
    /// </summary>
    [Serializable]
    public class PageGenerator
    {
        /// <summary>
        /// The path to the documentation file
        /// </summary>
        public String DocumentationFile { get; set; }

        /// <summary>
        /// Sets the way the page is ordered
        /// </summary>
        public PageOrder PageOrder { get; set; }

        /// <summary>
        /// Backing field for ClassPageOrder
        /// </summary>
        private readonly ClassPagePart[] _CustomClassPageOrder = new ClassPagePart[Enum.GetNames(typeof(ClassPagePart)).Count()];

        /// <summary>
        /// Array setting the order of the class page
        /// </summary>
        public ClassPagePart[] CustomClassPageOrder
        {
            get
            {
                return _CustomClassPageOrder;
            }
        }

        /// <summary>
        /// Backing field for MethodPageOrder
        /// </summary>
        private readonly MethodPagePart[] _CustomMethodPageOrder = new MethodPagePart[Enum.GetNames(typeof(MethodPagePart)).Count()];

        /// <summary>
        /// Array setting the order of the method page
        /// </summary>
        public MethodPagePart[] CustomMethodPageOrder
        {
            get
            {
                return _CustomMethodPageOrder;
            }
        }

        /// <summary>
        /// Backing field for PropertyPageOrder
        /// </summary>
        private readonly PropertyPagePart[] _CustomPropertyPageOrder = new PropertyPagePart[Enum.GetNames(typeof(PropertyPagePart)).Count()];

        /// <summary>
        /// Array setting the order of the property / field page
        /// </summary>
        public PropertyPagePart[] CustomPropertyPageOrder
        {
            get
            {
                return _CustomPropertyPageOrder;
            }
        }

        /// <summary>
        /// Initializes a new <see cref="DocNetPress.Generators.PageGenerator"/>-Instance and sets the path to the documentation file
        /// </summary>
        /// <param name="documentationFileName">The path to the documentation file read by the <see cref="DocNetPress.Generators.PageGenerator"/></param>
        public PageGenerator(String documentationFileName, PageOrder pageOrder)
        {
            this.DocumentationFile = documentationFileName;
            this.PageOrder = pageOrder;
            this.InitializeDefaultPageStructure();
        }

        /// <summary>
        /// Extracts the XML file and generates <see cref="DocNetPress.XmlRpc.Posts.Post"/>-Instances from the documentation ready to push
        /// to a WordPress installation
        /// </summary>
        /// <returns>The generates <see cref="DocNetPress.XmlRpc.Posts.Post"/>s containing a well-formatted documentation</returns>
        public PostInsert[] GeneratePosts()
        {
            lock (DocumentationFile)
            {
                return null;
            }
        }

        /// <summary>
        /// Sets the default page structure
        /// </summary>
        private void InitializeDefaultPageStructure()
        {
            // We get the Types only a single time and not in every iteration to save processing time
            Type classPagePartType = typeof(ClassPagePart);
            Type methodPagePartType = typeof(MethodPagePart);
            Type propertyPagePartType = typeof(PropertyPagePart);

            // Now we assign the values as they are in the enum
            for (int i = 0; i < CustomClassPageOrder.Length; i++)
                CustomClassPageOrder[i] = (ClassPagePart)(Enum.GetValues(classPagePartType).GetValue(i));
            for (int i = 0; i < CustomMethodPageOrder.Length; i++)
                CustomMethodPageOrder[i] = (MethodPagePart)(Enum.GetValues(methodPagePartType).GetValue(i));
            for (int i = 0; i < CustomPropertyPageOrder.Length; i++)
                CustomPropertyPageOrder[i] = (PropertyPagePart)(Enum.GetValues(propertyPagePartType).GetValue(i));
        }
    }
}
