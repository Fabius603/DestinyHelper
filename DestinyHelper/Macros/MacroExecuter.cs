using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Shapes;
using WindowsInput;
using WindowsInput.Native;

namespace DestinyHelper.Macros
{

    public class MacroExecuter
    {
        public Dictionary<String, MacroInputs> aktiveMacros { get; set; }

        [DllImport("user32.dll")]
        public static extern short VkKeyScan(char ch);

        public static void Execute()
        {
            Thread MacroThread = new Thread(() =>
            {
                MacroInputs macroInputs = Choose();
                if (macroInputs != null)
                {
                    PressKeys(macroInputs);
                }
            });
            MacroThread.Start();
        }

        static void PressKeys(MacroInputs macroInputs)
        {
            if (AutoFisher.GetActiveApplicationName() == "destiny2")
            {
                InputSimulator simulator = new InputSimulator();

                VirtualKeyCode keyCode;
                for (int i = 0; i < macroInputs.KeyInput.Count; i++)
                {
                    switch (macroInputs.Instruction[i])
                    {
                        case "keydown":
                            keyCode = ConvertToKeyCode(macroInputs.KeyInput[i]);
                            simulator.Keyboard.KeyDown(keyCode);
                            break;

                        case "keyup":
                            keyCode = ConvertToKeyCode(macroInputs.KeyInput[i]);
                            simulator.Keyboard.KeyUp(keyCode);
                            break;

                        case "sleep":
                            simulator.Keyboard.Sleep(Int32.Parse(macroInputs.KeyInput[i]));
                            break;

                        case "mousedown":
                            if(macroInputs.KeyInput[i] == "RIGHT")
                            {
                                simulator.Mouse.RightButtonDown();
                            }
                            else if(macroInputs.KeyInput[i] == "LEFT")
                            {
                                simulator.Mouse.LeftButtonDown();
                            }
                            break;

                        case "mouseup":
                            if (macroInputs.KeyInput[i] == "RIGHT")
                            {
                                simulator.Mouse.RightButtonUp();
                            }
                            else if (macroInputs.KeyInput[i] == "LEFT")
                            {
                                simulator.Mouse.LeftButtonUp();
                            }
                            break;

                        default:
                            break;
                    }

                }
            }
        }

        public static VirtualKeyCode ConvertToKeyCode(string key)
        {
            return (VirtualKeyCode)Enum.Parse(typeof(VirtualKeyCode), key);
        }

        static VirtualKeyCode GetVirtualKeyCode(char character)
        {
            short keyScanResult = VkKeyScan(character);
            byte keyCode = (byte)(keyScanResult & 0xFF);
            return (VirtualKeyCode)keyCode;
        }
        public static MacroInputs Choose()
        {
            MacroInputs macroInputs = ReadFile("MacroTesting");
            return macroInputs;
        }

        static MacroInputs ReadFile(string MacroName)
        {
            try
            {
                StreamReader sr = new StreamReader($"Macros\\MacroDateien\\{MacroName}.txt");
                String line = "";
                MacroInputs macroInputs = new MacroInputs();
                while (line != null)
                {
                    line = sr.ReadLine();
                    if (line != null)
                    {
                        string[] inputs = line.Split(" ");
                        macroInputs.Instruction.Add(inputs[0]);
                        macroInputs.KeyInput.Add(inputs[1]);
                    }
                }
                sr.Close();
                return macroInputs;
            }
            catch (Exception e)
            {
                MessageBox.Show("Failed to read file!");
                return null;
            }
        }
    }

    public class MacroInputs
    {
        public List<string> KeyInput = new List<string>();
        public List<string> Instruction = new List<string>();
    }
}
