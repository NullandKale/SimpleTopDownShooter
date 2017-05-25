using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;

namespace nullEngine.Entity___Component
{
    public class cMouseFire : iComponent
    {
        quad objToFire;
        renderable pc;
        Point mousePos;
        Point playerPos;

        bool fired;
        float speed;
        float stepAmount;
        float step;

        public cMouseFire(quad fireable, float projSpeed, float stepAmount, renderable player)
        {
            objToFire = fireable;
            speed = projSpeed;
            pc = player;
            this.stepAmount = stepAmount;

            step = 0;
            fired = false;
        }

        public void Run(renderable r)
        {
            if(Game.input.isClickedRising(OpenTK.Input.MouseButton.Left))
            {
                fired = true;
                mousePos = Game.input.mousePos;
                playerPos = new Point((int)pc.pos.xPos, (int)pc.pos.yPos);
            }

            if(fired)
            {
                objToFire.active = true;
                float newY = lerp(mousePos.Y, playerPos.Y, step);
                float newX = lerp(mousePos.X, playerPos.X, step);
                step += stepAmount;
                objToFire.setRelativePos(newX * Game.frameTime, newY * Game.frameTime);

                if(step > 3)
                {
                    Console.WriteLine("Reloading!");
                    fired = false;
                    step = 0;
                }
            }
        }

        float lerp(float v0, float v1, float t)
        {
            return (1 - t) * v0 + t * v1;
        }
    }
}
