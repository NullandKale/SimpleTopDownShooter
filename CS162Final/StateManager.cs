using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;

namespace OpenTKTest1.StateMachines
{
    abstract class StateManager
    {
        public iState CurrentState;
        public abstract void update(object sender, FrameEventArgs e);
    }
}
