using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nullEngine.Managers
{
    class CollisionManager
    {
        public static CollisionManager man;
        public List<Entity___Component.cCollider> colliders;

        public CollisionManager()
        {
            if(man == null)
            {
                man = this;
            }
            else
            {
                Console.WriteLine("Singleton Failure @ CollisionManager");
            }
            colliders = new List<Entity___Component.cCollider>();
        }

        public bool checkCollision(Entity___Component.cCollider c)
        {
            bool temp = false;
            for(int i = 0; i < colliders.Count; i++)
            {
                if (c.collides(colliders[i]))
                {
                    temp = true;
                }
            }
            return temp;
        }

    }
}
