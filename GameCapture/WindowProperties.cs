using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCapture
{
    internal class WindowProperties
    {
        public string WindowName { get; set; } = "destiny2";
        public int Width {  get; set; }
        public int Height { get; set; }
        public OpenCvSharp.Point Upperleft { get; set; }
        public OpenCvSharp.Point Lowerright { get; set; }
    }
}
