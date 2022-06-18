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

        

    }

}
