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
        public static void ActivateHotkey(string name)
        {
            GlobalHotkey newHotkey = new GlobalHotkey(ModifierKeys.None, Key.V, () => MacroExecuter.Execute(name));
            HotkeysManager.AddHotkey(newHotkey);
            AddMacroToDictionary(name);
            AddMacroToMacroDictionary(name, newHotkey);
        }

        public static void DeactivateHotkey(string name)
        {
            GlobalHotkey newHotkey = MacroExecuter.aktiveMacrosList[name];
            HotkeysManager.RemoveHotkey(newHotkey);
            RemoveHotkeyFromDictionary(name);
        }

        static void RemoveHotkeyFromDictionary(string name)
        {
            MacroExecuter.aktiveMacros.Remove(name);
            MacroExecuter.aktiveMacrosList.Remove(name);
        }

        static void AddMacroToMacroDictionary(string name, GlobalHotkey newHotkey)
        {
            MacroExecuter.aktiveMacrosList[name] = newHotkey;
        }

        static void AddMacroToDictionary(string name)
        {
            MacroInputs macroInputs = MacroExecuter.ReadFile(name);
            MacroExecuter.aktiveMacros[name] = macroInputs;
        }

        public static string NameSubstring(string name)
        {
            name = name.Substring(0, name.Length - 12);
            return name;
        }
    }
}
