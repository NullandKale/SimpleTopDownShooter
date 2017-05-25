using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;

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
                    List<int> temp = CheckCollision(colliders[i]);
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

        public static Point WillItCollide(Entity___Component.cCollider c, int xMove, int yMove)
        {
            Point p = new Point(xMove, yMove);
            //Create two rects that corrispond to the rect if this move is allowed for each axis and check for collision
            Rectangle futureRectX = new Rectangle(c.rect.X + xMove, c.rect.Y, c.rect.Width, c.rect.Height);
            Rectangle futureRectY = new Rectangle(c.rect.X, c.rect.Y + yMove, c.rect.Width, c.rect.Height);

            bool collideX = man.CheckFutureCollision(futureRectX, c);
            bool collideY = man.CheckFutureCollision(futureRectY, c);

            if(collideX)
            {
                p.X = 0;
            }

            if(collideY)
            {
                p.Y = 0;
            }

            return p;
        }

        public List<int> CheckCollision(Entity___Component.cCollider c)
        {
            List<int> temp = new List<int>();
            for(int i = 0; i < colliders.Count; i++)
            {
                if (c.collides(colliders[i]) && c != colliders[i])
                {
                    temp.Add(i);
                }
            }
            return temp;
        }

        public Boolean CheckFutureCollision(Rectangle rect, Entity___Component.cCollider c)
        {
            for (int i = 0; i < colliders.Count; i++)
            {
                if (colliders[i].collides(rect) && c != colliders[i])
                {
                    return true;
                }
            }
            return false;
        }

    }

    public struct movements
    {
        public Point p;
        public bool collidedXNeg;
        public bool collidedYNeg;
        public bool collidedXPos;
        public bool collidedYPos;
    }
}
