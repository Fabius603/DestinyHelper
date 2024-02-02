using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DestinyHelper.Macros;
using OpenCvSharp;
using WK.Libraries.HotkeyListenerNS;

namespace DestinyHelper
{
    public partial class MainWindow : System.Windows.Window
    {
        public MainWindow()
        {
            StartProject.Start();
            InitializeComponent();
            Closing += MainWindow_Closing;
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            HotkeysManager.ShutdownSystemHook();
        }
    }
}