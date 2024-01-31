using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DestinyHelper.Macros
{
    public class MacroManager
    {
        public static void ActivateAllHotkeys()
        {
            GlobalHotkey newHotkey = new GlobalHotkey(ModifierKeys.None, Key.V, MacroExecuter.Execute);
            HotkeysManager.AddHotkey(newHotkey);
        }
    }
}
