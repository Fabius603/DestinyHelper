using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using OpenCvSharp;
using System.Windows;
using System.Runtime.InteropServices;


namespace DestinyHelper
{
    public class Screenshot
    {
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        static extern bool GetWindowRect(IntPtr hWnd, out RECT rect);

        public static Mat GetScreenshot()
        {
            using (Bitmap screenshot = CaptureApplicationScreenshot("Destiny2"))
            {
                if (screenshot == null)
                {
                    return null;
                }

            Mat matImage = ConvertBitmapToMat(screenshot);

            return matImage;
            }
        }

        static IntPtr FindWindowByProcessName(string processName)
        {
            IntPtr hWnd = IntPtr.Zero;
            foreach (var process in System.Diagnostics.Process.GetProcessesByName(processName))
            {
                hWnd = process.MainWindowHandle;
                if (hWnd != IntPtr.Zero)
                    break;
            }
            return hWnd;
        }

        static Bitmap CaptureApplicationScreenshot(string processName)
        {
            IntPtr hWnd = FindWindowByProcessName(processName);

            if (hWnd != IntPtr.Zero)
            {
                RECT windowRect;
                GetWindowRect(hWnd, out windowRect);

                int width = windowRect.Right - windowRect.Left;
                int height = windowRect.Bottom - windowRect.Top;

                Bitmap screenshot = new Bitmap(width, height);

                using (Graphics graphics = Graphics.FromImage(screenshot))
                {
                    graphics.CopyFromScreen(windowRect.Left, windowRect.Top, 0, 0, new System.Drawing.Size(width, height));
                }

                return screenshot;
            }

            return null;
        }

        static Mat ConvertBitmapToMat(Bitmap bitmap)
        {
            Mat matImage = OpenCvSharp.Extensions.BitmapConverter.ToMat(bitmap);

            return matImage;
        }
    }
}
