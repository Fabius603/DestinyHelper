using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
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
            if (AutoFisher.GetActiveApplicationName() != "destiny2")
            {
                InputSimulator simulator = new InputSimulator();


                for (int i = 0; i < macroInputs.KeyInput.Count; i++)
                {
                    VirtualKeyCode keyCode = GetVirtualKeyCode(macroInputs.KeyInput[i][0]);
                    switch (macroInputs.Instruction[i])
                    {
                        case "keydown":
                            simulator.Keyboard.KeyDown(keyCode);
                            break;

                        case "keyup":
                            simulator.Keyboard.KeyUp(keyCode);
                            break;

                        case "sleep":
                            simulator.Keyboard.Sleep(Int32.Parse(macroInputs.KeyInput[i]));
                            break;

                        default:
                            break;
                    }

                }
            }
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
