using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using WebStressTool.libs;

namespace WebStressTool.Classes
{
    
    public class Url
    {

        public string href;
        public int unique;
        public List<Macro> macros = new List<Macro>();

        public Url(string _href)
        {

            unique = functions.GetRandomID();

            href = _href;

        }

        internal void Duplicate(int index)
        {
            var _macro = macros[index];
            var macro_text = !string.IsNullOrEmpty(_macro.macroText) ? _macro.macroText : _macro.macroWaitDelay.ToString();
            Macro n_macro = new Macro(_macro.macroXPATH,macro_text,_macro.macroType);
            macros.Add(n_macro);
            functions.GetMainForm().SomethingChanged = true;
        }
        internal void Duplicate(int[] index)
        {
            foreach(int i in index) {
                var _macro = macros[i];
                var macro_text = !string.IsNullOrEmpty(_macro.macroText) ? _macro.macroText : _macro.macroWaitDelay.ToString();
                Macro n_macro = new Macro(_macro.macroXPATH, macro_text, _macro.macroType);
                macros.Add(n_macro);
            }
            functions.GetMainForm().SomethingChanged = true;
        }
    }

}
