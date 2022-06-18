using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStressTool.Classes.Model;

namespace WebStressTool.Classes
{
    public static class SettingsConverter
    {

        public static Model_URLS ConvertToModel(RegisteredSites registeredSites)
        {
            var returnVal = new Model_URLS();
            foreach (var item in registeredSites.URLs)
            {
                Model_URL MURL = new Model_URL();
                MURL.href = item.href;
                MURL.unique = item.unique;
                foreach(var iMacro in item.macros)
                {
                    Model_MACRO MMACRO = new Model_MACRO();
                    MMACRO.macroID = iMacro.macroID;
                    MMACRO.macroText = iMacro.macroText;
                    MMACRO.macroXPATH = iMacro.macroXPATH;
                    MMACRO.macroWaitDelay = iMacro.macroWaitDelay;
                    MMACRO.macroType = iMacro.macroType;
                    MURL.macros.Add(MMACRO);
                }
                returnVal.URLs.Add(MURL);
            }
            return returnVal;
        }

        public static RegisteredSites ConvertToClass(Model_URLS registeredSites)
        {
            var returnVal = new RegisteredSites();
            foreach (var item in registeredSites.URLs)
            {
                Url MURL = new Url(item.href);
                MURL.unique = item.unique;
                foreach (var iMacro in item.macros)
                {
                    Macro MMACRO = new Macro(iMacro.macroXPATH, (!string.IsNullOrEmpty(iMacro.macroText) ? iMacro.macroText : iMacro.macroWaitDelay.ToString()), iMacro.macroType);
                    MMACRO.macroID = iMacro.macroID;
                    MMACRO.macroWaitDelay = iMacro.macroWaitDelay;
                    MURL.macros.Add(MMACRO);
                }
                returnVal.URLs.Add(MURL);
            }
            return returnVal;
        }

    }
}
