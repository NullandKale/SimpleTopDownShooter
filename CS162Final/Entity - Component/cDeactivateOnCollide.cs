using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nullEngine.Entity___Component
{
    class cDeactivateOnCollide : cCollider
    {
        List<int> colliding;

        public cDeactivateOnCollide(renderable r) : base (r)
        {

        }

        public override void Run(renderable r)
        {
            base.Run(r);

            colliding = Managers.CollisionManager.man.CheckCollision(this);

            for(int i = 0; i < colliding.Count; i++)
            {
                if(Managers.CollisionManager.man.boundingBoxes[this.key][i].rRef.tag != "Player")
                {
                    Managers.CollisionManager.man.boundingBoxes[this.key][i].rRef.active = false;
                    Console.WriteLine(Managers.CollisionManager.man.boundingBoxes[this.key][i].rRef.pos.xPos + " " + Managers.CollisionManager.man.boundingBoxes[this.key][i].rRef.pos.yPos);
                }
            }
        }
    }
}
