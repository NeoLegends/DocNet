using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DocNetPress
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Initializes a new Main Window
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TBLocalDocumentationFileBrowse_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog()
                                 {
                                     DefaultExt = "xml",
                                     DereferenceLinks = true,
                                     Multiselect = false,
                                     Title = "Choose your generated XML-Documentation file"
                                 };
            ofd.ShowDialog();
            TBLocalDocumentationFile.Text = ofd.FileName.Trim();
        }
    }
}
