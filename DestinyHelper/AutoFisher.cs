using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;
using WindowsInput;

namespace DestinyHelper
{



    public class AutoFisher
    {
        static string GetActiveApplicationName()
        {
            IntPtr hWnd = Screenshot.GetForegroundWindow();
            uint processId;
            Screenshot.GetWindowThreadProcessId(hWnd, out processId);

            Process process = Process.GetProcessById((int)processId);
            return process.ProcessName;
        }


        public static MatcherResults Run(Mat image, Settings settings)
        {
            if(settings.AutoFisherStatus != true)
            {
                settings = SetSettings(settings);
            }
            Rect ROI = GetROI(image, settings);
            image = image.SubMat(ROI);
            using (Mat trainerImage = new Mat("C:\\Users\\fjsch\\source\\repos\\DestinyHelper\\DestinyHelper\\Bilder\\FisherTrainer2.png"))
            {
                MatcherResults matcherResults = BildMatcher.MatchImages(image, trainerImage, ROI);
                return matcherResults;
            }
        }

        static Rect GetROI(Mat image, Settings settings)
        {
            int Left = (int)Math.Abs(image.Width / settings.ROI_Factors.Left);
            int Top = (int)Math.Abs(image.Height / settings.ROI_Factors.Top);
            int Right = (int)Math.Abs(image.Width / settings.ROI_Factors.Right);
            int Bottom = (int)Math.Abs(image.Height / settings.ROI_Factors.Bottom);
            Rect ROI = new Rect(Left, Top, Right, Bottom);
            return ROI;
        }

        static Settings SetSettings(Settings settings)
        {
            settings.ROI_Factors = new ROI(2.667, 1.521, 8.348, 18);
            return settings;
        }

        public static void HandleResults(MatcherResults matcherResults)
        {
            if(GetActiveApplicationName() == "destiny2")
            {
                InputSimulator simulator = new InputSimulator();

                simulator.Keyboard.Sleep(200);
                simulator.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.VK_E);
                simulator.Keyboard.Sleep(1500);
                simulator.Keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.VK_E);

            }
            Video.HandleResultTaskIsRunning = false;
            return;
        }
    }
}
