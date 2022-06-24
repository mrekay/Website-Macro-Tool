using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium.Chrome.ChromeDriverExtensions;
using System.Windows.Forms;
using System.IO;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using static config;
namespace WebStressTool.Classes
{
    public class ChromeDriverManager
    {
        IWebDriver webDriver;
        ChromeOptions chromeSettings;

        public bool InitializeDriver(object proxy_model = null, string user_agent = "", string userData = "", bool isMobile = false)
        {
            try
            {
                /* Declare Chrome Driver Service*/
                ChromeDriverService chromeDriverService = ChromeDriverService.CreateDefaultService(Application.StartupPath);
                chromeDriverService.HideCommandPromptWindow = true;

                /* Set Chrome Settings */
                chromeSettings = new ChromeOptions();
                chromeSettings.AddExcludedArgument("enable-automation");
                chromeSettings.AddArgument("--lang=en-us");
                chromeSettings.AddArgument("--disable-blink-features=AutomationControlled");
                chromeSettings.AddArgument("--disable-plugins-discovery");
                chromeSettings.AddArgument("--profile-directory=Default");

                if (isMobile) enterMobileMode();

                /* Set Prefences*/

                setUserAgent(chromeSettings,string.IsNullOrEmpty(user_agent) ? "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/98.0.4758.102 Safari/537.36 Edg/98.0.1108.62" : user_agent);
                setProxy(chromeSettings, proxy_model);
                setProfile(chromeSettings, userData);

                /* Declare WebDriver */
                webDriver = new ChromeDriver(chromeDriverService, chromeSettings);


                /* Wait for browser */
                setBrowserMode(chromeSettings);
                Task.Delay(2000);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tarayıcı Başlatılamadı. Lütfen aynı kullanıcı üzerinde tarayıcı açık olup olmadığını kontrol edin.","Hata",MessageBoxButtons.OK,MessageBoxIcon.Error);
                //ExceptionManager.showExceptionMessage("Tarayıcı Başlatılamadı. Lütfen aynı kullanıcı üzerinde tarayıcı açık olup olmadığını kontrol edin.", 1, "İşlem Başarısız", true);
            }
            return (webDriver != null);

        }

        private void setBrowserMode(ChromeOptions chromeSettings)
        {
            if (BROWSER_MODE == "HEADLESS") chromeSettings.AddArgument("--headless");
            else if (BROWSER_MODE == "MINIMIZED") webDriver.Manage().Window.Minimize();
        }
        private void setUserAgent(ChromeOptions chromeSettings, string userAgent)
        {
            chromeSettings.AddArgument("--user-agent=" + userAgent);
        }
        private void setProxy(ChromeOptions chromeSettings, object proxy_model = null)
        {
           /* if (proxy_model != null)
            {
                chromeSettings.AddHttpProxy(proxy_model.ProxyIpAdress, proxy_model.ProxyPort.ToInt32(), proxy_model.ProxyUsername, proxy_model.ProxyPassword);
            }*/
        }
        private void setProfile(ChromeOptions chromeSettings, string userData = "")
        {
            if (!string.IsNullOrEmpty(userData))
            {
                chromeSettings.AddArgument(@"--user-data-dir=" + ProfileLocation + userData + "\\");
            }
        }

        public void enterMobileMode()
        {
            chromeSettings.EnableMobileEmulation("iPhone SE");
        }

        public  void Navigate(string URL)
        {
            try
            {
                if (!isDriverDisposed())
                    webDriver.Navigate().GoToUrl(URL);
                 Task.Delay(500);
            }
            catch {  Task.Delay(500); return; }

        }

        public void Refresh()
        {
            try
            {
                webDriver.Navigate().Refresh();
            }
            catch { throw; }
        }
        public void Die()
        {
            if (webDriver != null)
                webDriver.Quit();
        }
        public IWebDriver GetWebDriver()
        {
            try
            {
                return this.webDriver;
            }
            catch { throw; }
        }

        public IWebElement GetElementByXpath(string xPath)
        {
            try
            {
                return GetWebDriver().FindElement(By.XPath(@xPath));
            }
            catch { return null; }
        }
        public IWebElement GetElementByID(string id)
        {
            try
            {
                return GetWebDriver().FindElement(By.Id(id));
            }
            catch { return null; }
        }

        public string GetHTML(string xPath)
        {
            try
            {
                return GetElementByXpath(xPath).GetAttribute("innerHTML");
            }
            catch { return null; }
        }

        public string GetHTML(IWebElement Element)
        {
            try
            {
                return Element.GetAttribute("innerHTML");
            }
            catch { return null; }
        }

        public void WaitNavigation()
        {
            new WebDriverWait(GetWebDriver(), TimeSpan.FromSeconds(20)).Until(d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));
        }

        public bool isDriverDisposed()
        {
            bool isClosed = false;
            try
            {
                webDriver.Url.ToString();
            }
            catch
            {
                isClosed = true;
            }

            return isClosed;
        }

        public string runJavaScript(string javaScript)
        {
            try
            {
                if (javaScript == null) return null;

                string returVal = null;
                IJavaScriptExecutor js = (IJavaScriptExecutor)webDriver;
                returVal = (string)js.ExecuteScript(javaScript);
                return returVal == null ? null : returVal;
            }
            catch
            {
                return null;
            }
        }

        public string getDocumentTitle()
        {
            try
            {
                return (string)this.webDriver.Title.ToString();
            }
            catch { throw; }
        }
        public string getUrl()
        {
            try
            {
                if (webDriver is null) return null;
                return this.webDriver.Url.ToString();
            }
            catch { return null; }
        }

        public void ClearCookies()
        {
            try
            {
                if (!isDriverDisposed())
                    webDriver.Manage().Cookies.DeleteAllCookies();
            }
            catch { throw; }
            Thread.Sleep(7000);
        }

        public IWebDriver getBrowser()
        {
            try
            {
                return this.webDriver;
            }
            catch { return null; }
        }

        public IWebElement getElementById(string id)
        {
            try
            {
                if (webDriver is null) return null;
                return this.webDriver.FindElement(By.Id(id));
            }
            catch (Exception)
            {
                return null;
            }
        }

        public string getClassNameById(string id)
        {
            try
            {
                if (webDriver is null) return "";
                return this.getElementById(id).GetAttribute("className");
            }
            catch { return ""; }
        }
        public bool setValueById(string id, string innerText)
        {
            try
            {
                if (this.isDriverDisposed()) return false;
                if (webDriver is null) return false;
                if (this.getElementById(id) is null) return false;
                this.getElementById(id).Clear();
                this.getElementById(id).SendKeys(innerText);
                return true;
            }
            catch { return false; }
        }      
        public bool setValueByXpath(string xpath, string innerText)
        {
            try
            {
                if (this.isDriverDisposed()) return false;
                if (webDriver is null) return false;
                if (this.GetElementByXpath(xpath) is null) return false;
                this.GetElementByXpath(xpath).Clear();
                this.GetElementByXpath(xpath).SendKeys(innerText);
                return true;
            }
            catch { return false; }
        }
        public string getInnerTextById(string id)
        {
            try
            {
                if (webDriver is null) return "";
                if (this.getElementById(id) is null) return "";
                return this.getElementById(id).GetAttribute("innerText");
            }
            catch { return ""; }

        }
        public string getHtmlById(string id)
        {
            try
            {
                if (webDriver is null) return "";
                if (this.getElementById(id) is null) return "";
                return this.getElementById(id).GetAttribute("innerHTML");
            }
            catch { return ""; }
        }
        public void clickById(string id)
        {
            try
            {
                if (webDriver is null) return;
                if (this.getElementById(id) is null) return;
                this.getElementById(id).Click();
            }
            catch { return; }
        }
        public void clickByXpath(string xpath)
        {
            try
            {
                if (webDriver is null) return;
                if (this.GetElementByXpath(xpath) is null) return;
                this.GetElementByXpath(xpath).Click();
            }
            catch(Exception ex) {
                Console.WriteLine("Click By Xpath durumunda hata oluştu ayrıntılar \n"+ex.ToString());
            }
        }

        public bool clickByClassName(string id)
        {
            try
            {
                if (webDriver is null) return false;
                this.webDriver.FindElement(By.ClassName(id)).Click();
                return true;
            }
            catch { return false; }
        }
    }
}
