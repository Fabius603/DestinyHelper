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
        public static void PlayVideo()
        {
            Settings settings = new Settings();

            while (MainWindow.isrunning)
            {
                Mat screenshot = Screenshot.GetScreenshot();
                if (screenshot != null)
                {
                    screenshot = AutoFisher.Run(screenshot);
                    BildAnzeigen(screenshot, settings);

                    // Bilder aus Arbeitsspeicher löschen --!! SEHR WICHTIG !!--
                    screenshot.Dispose();
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
