using DestinyHelper.Macros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DestinyHelper
{
    public class StartProject
    {
        public static void Start()
        {
            HotkeysManager.SetupSystemHook();
            MacroManager.LoadMacros();
        }
    }
}
