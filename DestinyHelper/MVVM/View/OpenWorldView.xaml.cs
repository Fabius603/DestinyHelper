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

namespace DestinyHelper.MVVM.View
{
    public partial class AutoFisherView : UserControl
    {
        public static bool fishingisrunning = false;

        public AutoFisherView()
        {
            InitializeComponent();
        }

        public void Fishingrun_Click(object sender, RoutedEventArgs e)
        {
            if (!fishingisrunning)
            {
                FishingToggleButton.Content = "Stop";
                StartVideo();
            }
            else
            {
                FishingToggleButton.Content = "Start";
            }
            fishingisrunning = !fishingisrunning;
        }

        static async Task StartVideo()
        {
            Task PlayVideoTask = new Task(Video.PlayVideo);
            PlayVideoTask.Start();
        }

    }
}
