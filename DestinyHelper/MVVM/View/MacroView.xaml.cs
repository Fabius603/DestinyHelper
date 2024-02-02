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
            if (!MacroManager.AktiveMacros.ContainsKey(name))
            {
                MacroManager.ActivateHotkey(name);
            }
            else
            {
                MacroManager.DeactivateHotkey(name);
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (!string.IsNullOrEmpty(textBox.Text))
            {
                string newText = char.ToUpper(textBox.Text[0]) + textBox.Text.Substring(1);
                if (textBox.Text != newText)
                {
                    textBox.Text = newText;
                    textBox.CaretIndex = textBox.Text.Length; // Setzt den Cursor ans Ende des Textes
                }
                bool isKey = UpdateMacroBindings(textBox.Name, newText, textBox);
                if (isKey)
                {
                    SolidColorBrush white = new SolidColorBrush(Colors.White);
                    textBox.Foreground = white;
                }
                else
                {
                    SolidColorBrush red = new SolidColorBrush(Colors.Red);
                    textBox.Foreground = red;
                }
            }
        }

        static bool UpdateMacroBindings(string name, string value, TextBox textBox)
        {
            Key key = GetEnumValue(value);
            if (key != 0)
            {
                MacroManager.MacroBindings[name] = key;
                try
                {
                    MacroManager.DeactivateHotkey(name);
                    MacroManager.ActivateHotkey(name);
                }
                catch { }
                return true;
            }
            else
            {
                try
                {
                    MacroManager.DeactivateHotkey(name);
                }
                catch { }
                return false;
            }
        }

        public static string GetEnumName(Key enumValue)
        {
            return Enum.GetName(typeof(Key), enumValue);
        }

        public static Key GetEnumValue(string enumName)
        {
            try
            {
                return (Key)Enum.Parse(typeof(Key), enumName);
            }
            catch
            {
                return 0;
            }
        }
    }
}
