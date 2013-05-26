using CookComputing.XmlRpc;
using DocNetPress.Development.XmlRpc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocNetPress.Development.Generator
{
    /// <summary>
    /// Uploads generated Documentation posts into a WordPress-Installation while automatically taking care of double entries
    /// </summary>
    [Serializable]
    public class PostUploader
    {
        /// <summary>
        /// Backing field for WordPressXmlRpcApiUrl
        /// </summary>
        private String _WordPressXmlRpcApiUrl = String.Empty;

        /// <summary>
        /// The URL to the target WordPress's XML-RPC interface
        /// </summary>
        public String WordPressXmlRpcApiUrl
        {
            get
            {
                return _WordPressXmlRpcApiUrl;
            }
            set
            {
                _WordPressXmlRpcApiUrl = value;
                WordPressXmlRpcInterface.Url = value;
            }
        }

        /// <summary>
        /// Backing field for WordPressXmlRpcInterface
        /// </summary>
        private readonly IWordPress _WordPressXmlRpcInterface = XmlRpcProxyGen.Create<IWordPress>();

        /// <summary>
        /// The interface to the 
        /// </summary>
        public IWordPress WordPressXmlRpcInterface
        {
            get
            {
                return _WordPressXmlRpcInterface;
            }
        }

        /// <summary>
        /// Initializes an empty <see cref="DocNetPress.Development.Generators.PostUploader"/>
        /// </summary>
        public PostUploader() { }

        /// <summary>
        /// Initializes a new <see cref="DocNetPress.Development.Generators.PostUploader"/>
        /// </summary>
        /// <param name="wordPressXmlRpcApiUrl">The URL to the target installation's XML-RPC interface</param>
        public PostUploader(String wordPressXmlRpcApiUrl)
        {
            this.WordPressXmlRpcApiUrl = wordPressXmlRpcApiUrl;
        }

        public String ResolveMemberNameToUrl(String fullMemberName)
        {
            
        }

        public Uri ResolveMemberNameToUri(String fullMemberName)
        {
            return new Uri(this.ResolveMemberNameToUrl(fullMemberName));
        }
    }
}
