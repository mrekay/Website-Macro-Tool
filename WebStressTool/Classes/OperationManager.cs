using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebStressTool.Classes
{
    public class OperationManager
    {

        RegisteredSites registeredSites;
        LoopManager loopManager;

        bool Active = false, SingleShot = false;
        public bool Complete = false;
        int loopIndex = 0, siteIndex = 0, macroIndex = 0, LoopID = -1;

        public OperationManager(RegisteredSites registeredSites, LoopManager loopManager, int _LoopID = -1)
        {
            this.registeredSites = registeredSites;
            this.loopManager = loopManager;

            if (_LoopID > -1) { SingleShot = true; LoopID = _LoopID; }
        }

        public async void Start()
        {
            Active = true;
            Operation();
        }

        public void Stop()
        {
            Active = false;
        }

        ChromeDriverManager chromeDriverManager = new ChromeDriverManager();
        private void Operation()
        {

            foreach (var loop in loopManager.Loops)
            {
                if (loopIndex != LoopID && LoopID > -1) { loopIndex++; continue; }

                if (!Active) break;
                InitalizeDriver();
                Thread.Sleep(250);
                if (!Active) break;
                SiteLoop();
                Thread.Sleep(250);
                if (!Active) break;
                chromeDriverManager.Die();
                if (!Active) break;
                Thread.Sleep(250);
                loopIndex++;

            }
            Complete = true;
        }

        private void InitalizeDriver()
        {
            chromeDriverManager.InitializeDriver(userData: loopManager.Loops[loopIndex].variables[0].Value);
            Thread.Sleep(250);
        }

        private void SiteLoop()
        {
            foreach (var site in registeredSites.Get())
            {
                if (!Active) break;
                chromeDriverManager.Navigate(site.href);
                chromeDriverManager.WaitNavigation();
                Thread.Sleep(250);
                MacroLoop(site);
                Thread.Sleep(250);
            }
        }

        private void MacroLoop(Url loopURL)
        {

            foreach (var macro in loopURL.macros)
            {
                if (!Active) break;
                MakeMacroOperation(macro, loopURL.macros);
            }
        }

        public void MakeMacroOperation(Macro macro, List<Macro> macros,string value = "")
        {
            if (!Active) return;
            switch (macro.macroType)
            {
                case MacroTypes.Click:
                    chromeDriverManager.clickByXpath(ReplaceValue(macro.macroXPATH,value));
                    Thread.Sleep(250);
                    break;
                case MacroTypes.Write:
                    chromeDriverManager.clickByXpath(ReplaceValue(macro.macroXPATH, value));
                    chromeDriverManager.setValueByXpath(ReplaceValue(macro.macroXPATH, value), get_write_param(macro.macroText));
                    Thread.Sleep(250);
                    break;
                case MacroTypes.Javascript:
                    chromeDriverManager.runJavaScript(get_write_param(macro.macroText));
                    Thread.Sleep(250);
                    break;
                case MacroTypes.Wait:
                    Thread.Sleep(macro.macroWaitDelay);
                    break;
                case MacroTypes.Goto:
                    int loopCount = Convert.ToInt32( macro.macroText);
                    for (int i = 1; i <= loopCount; i++)
                    {
                        MakeMacroOperation(macros[Int32.Parse(macro.macroXPATH)-1], macros,i.ToString());
                        Thread.Sleep(2500);
                    }
                    break;
                case MacroTypes.Pause:
                    Stop();
                    break;
            }
        }

        public string get_write_param(string text)
        {
            var write_param = text;
            if (text.StartsWith(":"))
            {
                write_param = SearchForLoopDictionary(write_param);
            }
            return write_param;
        }
        public string ReplaceValue(string text,string value)
        {
            return text.Replace("@gindex", value);
        }
        private string SearchForLoopDictionary(string variableName)
        {

            foreach (var variable in loopManager.Loops[loopIndex].variables)
            {

                var substringed_text = variableName.Split(':')[1];
                if (variable.Name.ToLower() == substringed_text.ToLower()) return variable.Value;

            }

            return variableName;
        }
        
    }
}
