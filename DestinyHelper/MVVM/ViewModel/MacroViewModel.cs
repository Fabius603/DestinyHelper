using DestinyHelper.Core;
using DestinyHelper.Macros;
using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace DestinyHelper.MVVM.ViewModel
{
    class MacroViewModel : ObservableObject
    {
        private string _hunterSkateHotkey;
        private string _hunterBodenSkateHotkey;
        private string _warlockSkateHotkey;
        private string _warlockBodenSkateHotkey;

        public string HunterSkateHotkey
        {
            get { return _hunterSkateHotkey; }
            set
            {
                _hunterSkateHotkey = value;
                OnPropertyChanged();
            }
        }

        public string HunterBodenSkateHotkey
        {
            get { return _hunterBodenSkateHotkey; }
            set
            {
                _hunterBodenSkateHotkey = value;
                OnPropertyChanged();
            }
        }

        public string WarlockSkateHotkey
        {
            get { return _warlockSkateHotkey; }
            set
            {
                _warlockSkateHotkey = value;
                OnPropertyChanged();
            }
        }
        public string WarlockBodenSkateHotkey
        {
            get { return _warlockBodenSkateHotkey; }
            set
            {
                _warlockBodenSkateHotkey = value;
                OnPropertyChanged();
            }
        }

        public MacroViewModel()
        {
            HunterSkateHotkey = GetEnumName(MacroManager.MacroBindings["HunterSkate"]);
            HunterBodenSkateHotkey = GetEnumName(MacroManager.MacroBindings["HunterBodenSkate"]);
            WarlockSkateHotkey = GetEnumName(MacroManager.MacroBindings["WarlockSkate"]);
            WarlockBodenSkateHotkey = GetEnumName(MacroManager.MacroBindings["WarlockBodenSkate"]);
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
