using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebStressTool.Classes
{
    
    public class Macro
    {

        public int macroID;
        public string macroXPATH;
        public string macroText;
        public int macroWaitDelay;

        public MacroTypes macroType;

        public Macro(string _macroXPATH,string _macroText,MacroTypes _macroType)
        {
            macroXPATH = _macroXPATH;
            switch (_macroType)
            {
                case MacroTypes.Wait:
                    macroWaitDelay = Int32.Parse(_macroText);
                    break;
                case MacroTypes.Write:
                    macroText = _macroText;
                    break;
                case MacroTypes.Javascript:
                    macroText = _macroText;
                    break;
                default:
                    break;
            }
            macroType = _macroType;

            Random rnd = new Random();
            macroID = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds * rnd.Next();
        }


    }
}
