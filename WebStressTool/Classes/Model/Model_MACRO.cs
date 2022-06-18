using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebStressTool.Classes.Model
{
    [Serializable]
    public class Model_MACRO
    {
        public int macroID;
        public string macroXPATH;
        public string macroText;
        public int macroWaitDelay;

        public MacroTypes macroType;
    }
}
