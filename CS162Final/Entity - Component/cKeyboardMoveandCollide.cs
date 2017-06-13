using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK.Input;
using System.Drawing;

namespace nullEngine.Entity___Component
{
    class cKeyboardMoveandCollide : KeyboardControl
    {
        private cCollider collider;
        private Managers.WorldManager wMan;

        public cKeyboardMoveandCollide(int speed, cCollider c) : base(speed)
        {
            collider = c;
            wMan = Managers.WorldManager.man;
        }

        public override void Run(renderable r)
        {
            if(r.active)
            {
                int xMove = 0;
                int yMove = 0;

                bool moved = false;

                if (Game.input.KeyHeld(Key.W))
                {
                    yMove -= speed;
                    moved = true;
                }
                if (Game.input.KeyHeld(Key.S))
                {
                    yMove += speed;
                    moved = true;
                }
                if (Game.input.KeyHeld(Key.A))
                {
                    xMove -= speed;
                    moved = true;
                }
                if (Game.input.KeyHeld(Key.D))
                {
                    xMove += speed;
                    moved = true;
                }

                if (moved)
                {
                    Point p = Managers.CollisionManager.WillItCollide(collider, xMove, yMove);

                    p.X += (int)r.pos.xPos;
                    p.Y += (int)r.pos.yPos;

                    int worldMoveX = 0;
                    int worldMoveY = 0;

                    if (p.X >= wMan.worldMaxX)
                    {
                        p.X = 100;
                        worldMoveX++;
                    }
                    if (p.X <= 0)
                    {
                        p.X = wMan.worldMaxX - 100;
                        worldMoveX--;
                    }

                    if (p.Y >= wMan.worldMaxY)
                    {
                        p.Y = 100;
                        worldMoveY++;
                    }
                    if (p.Y <= 0)
                    {
                        p.Y = wMan.worldMaxY - 100;
                        worldMoveY--;
                    }

                    r.setPos(p);

                    wMan.ChangeCurrentChunk(worldMoveX, worldMoveY);
                }
            }
        }
    }
}
