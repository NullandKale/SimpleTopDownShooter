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
        public Dictionary<Point, List<Entity___Component.cCollider>> boundingBoxes;
        int boundSize;

        public CollisionManager(int minBoundSize)
        {
            if(man == null)
            {
                man = this;
            }
            else
            {
                throw new SingletonException(this);
            }
            boundSize = minBoundSize;
            boundingBoxes = new Dictionary<Point, List<Entity___Component.cCollider>>();
        }

        public void update()
        {

        }

        public static void addCollider(Entity___Component.cCollider c)
        {
            Point key = getKey(c.rect);
            if(man.boundingBoxes.ContainsKey(key))
            {
                man.boundingBoxes[key].Add(c);
            }
            else
            {
                man.boundingBoxes.Add(key, new List<Entity___Component.cCollider>());
                man.boundingBoxes[key].Add(c);
            }
            c.key = key;
        }

        public static void removeCollider(Entity___Component.cCollider c)
        {
            man.boundingBoxes[c.key].Remove(c);
        }

        public static void moveCollider(Entity___Component.cCollider c)
        {
            removeCollider(c);
            addCollider(c);
        }

        public static Point getKey(Rectangle rect)
        {
            return new Point(rect.X / man.boundSize, rect.Y / man.boundSize);
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

        public List<Entity___Component.cCollider> CheckCollision(Entity___Component.cCollider c)
        {
            List<Entity___Component.cCollider> temp = new List<Entity___Component.cCollider>();
            Point cKey = getKey(c.rect);

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    Point key = new Point(cKey.X + i, cKey.Y + j);
                    if (boundingBoxes.ContainsKey(key))
                    {
                        for (int k = 0; k < boundingBoxes[key].Count; k++)
                        {
                            if (c.collides(boundingBoxes[key][k]) && c != boundingBoxes[key][k])
                            {
                                temp.Add(boundingBoxes[key][k]);
                            }
                        }
                    }
                }
            }

            return temp;
        }

        public Boolean CheckForLeaveWorld(Rectangle rect)
        {
            if(rect.X + rect.Width > Game.worldRect.Right)
            {
                return true;
            }

            if(rect.X < Game.worldRect.Left)
            {
                return true;
            }

            if(rect.Y + rect.Height > Game.worldRect.Bottom)
            {
                return true;
            }

            if(rect.Y < Game.worldRect.Top)
            {
                return true;
            }

            return false;

        }

        public Boolean CheckFutureCollision(Rectangle rect, Entity___Component.cCollider c)
        {
            Point cKey = getKey(rect);
            if(CheckForLeaveWorld(rect))
            {
                return true;
            }
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    Point key = new Point(cKey.X + i, cKey.Y + j);
                    if (boundingBoxes.ContainsKey(key))
                    {
                        for (int k = 0; k < boundingBoxes[key].Count; k++)
                        {
                            if (boundingBoxes[key][k].collides(rect, c) && c != boundingBoxes[key][k])
                            {
                                return true;
                            }
                        }
                    }
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
