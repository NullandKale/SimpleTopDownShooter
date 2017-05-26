using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;

namespace nullEngine.Entity___Component
{
    public class cCollider : iComponent
    {
        public renderable rRef;
        public Rectangle rect;
        public Point key;


        public cCollider(renderable r)
        {
            Managers.CollisionManager.addCollider(this);
            rRef = r;
            rect = new Rectangle((int)rRef.pos.xPos, (int)rRef.pos.yPos, rRef.getWidth(), rRef.getHeight());
        }

        public virtual void Run(renderable r)
        {
            if((int)r.pos.xPos != rect.X || (int)r.pos.yPos != rect.Y)
            {
                rect.X = (int)r.pos.xPos;
                rect.Y = (int)r.pos.yPos;
                Managers.CollisionManager.moveCollider(this);
            }
        }

        public bool collides(cCollider c1)
        {
            if(this.rRef == null || c1.rRef == null || c1 == this)
            {
                return false;
            }

            return rect.IntersectsWith(c1.rect);
        }

        public bool collides(Rectangle otherRect)
        {
            if(this.rect == otherRect)
            {
                return false;
            }

            return rect.IntersectsWith(otherRect);
        }

        public virtual void callback(cCollider c)
        {

        }
    }
}
