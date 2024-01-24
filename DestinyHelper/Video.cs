using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DestinyHelper
{
    public class Video
    {
        public static bool HandleResultTaskIsRunning = false;

        public static void PlayVideo()
        {
            Settings settings = new Settings();

            while (MainWindow.isrunning)
            {
                Mat screenshot = Screenshot.GetScreenshot();
                if (screenshot != null)
                {
                    MatcherResults matcherResults = AutoFisher.Run(screenshot, settings);

                    if(!Video.HandleResultTaskIsRunning && matcherResults.Success)
                    {
                        Video.HandleResultTaskIsRunning = true;
                        Task HandleResultTask = new Task(() => AutoFisher.HandleResults(matcherResults));
                        HandleResultTask.Start();
                    } 

                    BildAnzeigen(matcherResults.img, settings);

                    // Bilder aus Arbeitsspeicher löschen --!! SEHR WICHTIG !!--
                    screenshot.Dispose();
                    GC.Collect();
                }
            }
            Cv2.DestroyAllWindows();
        }

        static void BildAnzeigen(Mat image, Settings settings)
        {
            image = ResizeImage(image, settings);
            Cv2.ImShow("DestinyHelperPreview", image);
            Cv2.WaitKey(1);
        }

        static Mat ResizeImage(Mat inputImage, Settings settings)
        {
            double aspectRatio = (double)inputImage.Width / inputImage.Height;

            int newHeight = (int)(settings.AnzeigBreite / aspectRatio);

            if (newHeight > settings.AnzeigHöhe)
            {
                newHeight = settings.AnzeigHöhe;
                int newWidth = (int)(settings.AnzeigHöhe * aspectRatio);
                Cv2.Resize(inputImage, inputImage, new Size(newWidth, newHeight));
            }
            else
            {
                Cv2.Resize(inputImage, inputImage, new Size(settings.AnzeigBreite, newHeight));
            }

            return inputImage;
        }
    }
}
