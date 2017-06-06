﻿using System;
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

        public cKeyboardMoveandCollide(int speed, cCollider c) : base(speed)
        {
            collider = c;
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
                    r.setRelativePos(p);
                }
            }
        }
    }
}
