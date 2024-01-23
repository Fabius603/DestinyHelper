using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;

namespace DestinyHelper
{
    public class AutoFisher
    {
        public static Mat Run(Mat image)
        {
            Settings settings = SetSettings();
            image = ApplyROI(image, settings);
            return image;
        }

        static Mat ApplyROI(Mat image, Settings settings)
        {
            image = image.SubMat(new Rect(
                (int)Math.Abs(image.Width / settings.ROI_Factors.Left),
                (int)Math.Abs(image.Height / settings.ROI_Factors.Top),
                (int)Math.Abs(image.Width / settings.ROI_Factors.Right),
                (int)Math.Abs(image.Height / settings.ROI_Factors.Bottom)
                ));
            return image;

        }

        static Settings SetSettings()
        {
            Settings settings = new Settings();
            settings.ROI_Factors = new ROI(2.667, 1.521, 8.348, 18);
            return settings;
        }
    }
}
