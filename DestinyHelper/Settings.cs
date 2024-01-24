using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DestinyHelper
{
    public class Settings
    {
        // Spielgröße geteilt durch die Werte ergibt das ROI 
        public ROI ROI_Factors {  get; set; }
        public int AnzeigHöhe = 600;
        public int AnzeigBreite = 800;

        public bool AutoFisherStatus;
    }

    public class ROI
    {
        public double Left { get; set; }
        public double Top { get; set; }
        public double Right { get; set; }
        public double Bottom { get; set; }

        public ROI(double left, double top, double right, double bottom)
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }
    }
}
