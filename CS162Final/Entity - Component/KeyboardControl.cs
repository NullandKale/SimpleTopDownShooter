using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK.Input;

namespace nullEngine.Entity___Component
{
    class KeyboardControl : iComponent
    {
        private int speed;

        public KeyboardControl(int speed)
        {
            this.speed = speed;
        }

        public void Run(renderable r)
        {
            if(Game.input.KeyHeld(Key.W))
            {
                r.pos.yPos -= speed;
            }

            if (Game.input.KeyHeld(Key.S))
            {
                r.pos.yPos += speed;
            }

            if (Game.input.KeyHeld(Key.A))
            {
                r.pos.xPos -= speed;
            }

            if (Game.input.KeyHeld(Key.D))
            {
                r.pos.xPos += speed;
            }
        }
    }
}
