using DestinyHelper.Macros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace DestinyHelper.MVVM.View
{
    public partial class MacroView : UserControl
    {
        public MacroView()
        {
            InitializeComponent();
        }

        public void ToggleMacro_Click(object sender, RoutedEventArgs e)
        {
            ToggleButton clickedButton = sender as ToggleButton;
            string name = MacroManager.NameSubstring(clickedButton.Name);
            if (!MacroExecuter.aktiveMacros.ContainsKey(name))
            {
                clickedButton.Content = "Aktiviert";
                MacroManager.ActivateHotkey(name);
            }
            else
            {
                clickedButton.Content = "Deaktiviert";
                MacroManager.DeactivateHotkey(name);
            }
        }
    }
}
