using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nullEngine.Entity___Component
{
    class cDeactivateOnCollide : cCollider
    {
        List<cCollider> colliding;
        renderable PC;

        public cDeactivateOnCollide(renderable r, renderable player) : base (r)
        {
            PC = player;
        }

        public override void Run(renderable r)
        {
            base.Run(r);

            colliding = Managers.CollisionManager.man.CheckCollision(this);

            //if(colliding.Count > 0)
            //{
            //    if(Managers.CollisionManager.man.boundingBoxes.ContainsKey(this.key))
            //    {
            //        if (col.Count > 0)
            //        {
            //            if (col[colliding[0]].rRef != PC)
            //            {
            //                col[].rRef.active = false;
            //            }
            //        }
            //    }
            //}

            //This was intended to allow the object to collide with more than one thing at a time, but it causes a crash whenever there is more than one object in the list.
            for (int i = 0; i < colliding.Count; i++)
            {
                if (colliding[i].rRef != PC)
                {
                    colliding[i].rRef.active = false;
                }
            }
        }
    }
}
