using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebStressTool.libs
{
    public static class functions
    {

        public static int GetRandomID()
        {
            var rnd = new Random();
            return (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds * rnd.Next();
        }

        public static Form1 GetMainForm()
        {
            return Application.OpenForms[0] as Form1;
        }

    }
}
