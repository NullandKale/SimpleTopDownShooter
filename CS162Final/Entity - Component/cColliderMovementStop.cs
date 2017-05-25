using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nullEngine.Entity___Component
{
    class cColliderMovementStop : cCollider
    {
        private KeyboardControl key;
        int xCol;
        int yCol;

        public cColliderMovementStop(renderable r, KeyboardControl k) : base(r)
        {
            key = k;
            xCol = 0;
            yCol = 0;
        }

        public override void Run(renderable r)
        {
            base.Run(r);
            key.CollidingOn(xCol, yCol);
            xCol = 0;
            yCol = 0;
        }

        public override void callback(cCollider c)
        {
            if(c.rect.X > rect.X)
            {
                xCol = 1;
            }
            else if(c.rect.X < rect.X)
            {
                xCol = -1;
            }

            if(c.rect.Y > rect.Y)
            {
                yCol = 1;
            }
            else if(c.rect.Y < rect.Y)
            {
                yCol = -1;
            }
        }
    }
}
