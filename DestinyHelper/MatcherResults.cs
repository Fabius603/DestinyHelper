using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DestinyHelper
{
    public class MatcherResults
    {
        public bool Success { get; set; }
        public double RotationAngleDegrees { get; set; }
        public DMatch[] goodMatches { get; set; }
        public Point2f[] destination {  get; set; }
        public double[,] realPosition { get; set; }
        public Mat img { get; set; }
    }
}
