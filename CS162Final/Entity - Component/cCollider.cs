using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;

namespace nullEngine.Entity___Component
{
    class cCollider : iComponent
    {
        public renderable rRef;
        public Rectangle rect;

        public cCollider(renderable r)
        {
            Managers.CollisionManager.man.colliders.Add(this);
            rRef = r;
            rect = new Rectangle((int)rRef.pos.xPos, (int)rRef.pos.yPos, rRef.getWidth(), rRef.getHeight());
        }

        public void Run(renderable r)
        {

        }

        public bool collides(cCollider c1)
        {
            if(this.rRef == null || c1.rRef == null)
            {
                return false;
            }

            return rect.IntersectsWith(c1.rect);
        }
    }
}
