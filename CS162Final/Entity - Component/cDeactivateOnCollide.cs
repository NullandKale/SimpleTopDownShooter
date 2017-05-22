using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nullEngine.Entity___Component
{
    class cDeactivateOnCollide : cCollider
    {

        public cDeactivateOnCollide(renderable r) : base (r)
        {

        }

        public override void callback(cCollider c)
        {
            c.rRef.active = false;
        }
    }
}
