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
        public static Dictionary<String, MacroInputs> AktiveMacros = new Dictionary<String, MacroInputs>();
        public static Dictionary<String, GlobalHotkey> AktiveMacrosList = new Dictionary<String, GlobalHotkey>();
        public static Dictionary<String, Key> MacroBindings = new Dictionary<String, Key>();

        public static void ActivateHotkey(string name)
        {
            GlobalHotkey newHotkey = new GlobalHotkey(ModifierKeys.None, MacroBindings[name], () => MacroExecuter.Execute(name));
            HotkeysManager.AddHotkey(newHotkey);
            AddMacroToDictionary(name);
            AddMacroToMacroDictionary(name, newHotkey);
        }

        public static void DeactivateHotkey(string name)
        {
            GlobalHotkey newHotkey = MacroManager.AktiveMacrosList[name];
            HotkeysManager.RemoveHotkey(newHotkey);
            RemoveHotkeyFromDictionary(name);
        }

        static void RemoveHotkeyFromDictionary(string name)
        {
            AktiveMacros.Remove(name);
            AktiveMacrosList.Remove(name);
        }

        static void AddMacroToMacroDictionary(string name, GlobalHotkey newHotkey)
        {
            AktiveMacrosList[name] = newHotkey;
        }

        static void AddMacroToDictionary(string name)
        {
            MacroInputs macroInputs = MacroExecuter.ReadFile(name);
            AktiveMacros[name] = macroInputs;
        }

        public static string NameSubstring(string name)
        {
            name = name.Substring(0, name.Length - 12);
            return name;
        }

        public static void LoadMacros()
        {
            MacroBindings["HunterSkate"] = Key.V;
            MacroBindings["HunterBodenSkate"] = Key.T;
            MacroBindings["WarlockSkate"] = Key.V;
            MacroBindings["WarlockBodenSkate"] = Key.T;
        }
    }
}
