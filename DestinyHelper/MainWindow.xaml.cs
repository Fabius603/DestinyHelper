using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using OpenCvSharp;

namespace DestinyHelper
{
    public partial class MainWindow : System.Windows.Window
    {
        public static bool isrunning = false;

        public MainWindow()
        {
            InitializeComponent();
        }

        public void btnRun_Click(object sender, RoutedEventArgs e)
        {
            if(!isrunning)
            {
                btnRun.Content = "Stop!";
                StartVideo();
            }
            else
            {
                btnRun.Content = "Start!";
            }
            isrunning = !isrunning;
        }

        static async Task StartVideo()
        {
            Task PlayVideoTask = new Task(Video.PlayVideo);
            PlayVideoTask.Start();

        }
    }
}