using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nullEngine.StateMachines
{
    interface iState
    {
        void enter();
        void update();
    }
}
