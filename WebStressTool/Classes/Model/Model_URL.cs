using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebStressTool.Classes.Model
{
    public class Model_URL
    {

        public string href;
        public int unique;
        public List<Model_MACRO> macros = new List<Model_MACRO>();

    }
}
