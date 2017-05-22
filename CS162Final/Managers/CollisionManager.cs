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

        bool waitingPeriodOver;

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
            waitingPeriodOver = false;
        }

        public void update()
        {
            if(waitingPeriodOver)
            {
                for (int i = 0; i < colliders.Count; i++)
                {
                    List<int> temp = checkCollision(colliders[i]);
                    for (int j = 0; j < temp.Count; j++)
                    {
                        colliders[i].callback(colliders[temp[j]]);
                    }
                }
            }
            else
            {
                if(Game.tick == 19)
                {
                    waitingPeriodOver = true;
                }
            }
        }

        public List<int> checkCollision(Entity___Component.cCollider c)
        {
            List<int> temp = new List<int>();
            for(int i = 0; i < colliders.Count; i++)
            {
                if (c.collides(colliders[i]))
                {
                    temp.Add(i);
                }
            }
            return temp;
        }

    }
}
