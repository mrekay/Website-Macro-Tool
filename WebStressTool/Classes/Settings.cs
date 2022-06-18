using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStressTool.Classes.Model;

namespace WebStressTool.Classes
{
    [Serializable]
    public class Settings
    {

        
        public LoopManager loopManager;
        public Model_URLS registeredSites;
        public int max_loop_limit;
        

    }
}
