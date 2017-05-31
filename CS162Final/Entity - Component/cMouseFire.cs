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
        float stepAmount;
        float step;
        float speed;

        public cMouseFire(quad fireable, float projSpeed, renderable player)
        {
            objToFire = fireable;
            pc = player;

            speed = projSpeed;
            step = 0;
            fired = false;
        }

        public void Run(renderable r)
        {
            if(Game.input.isClickedRising(OpenTK.Input.MouseButton.Left))
            {
                fired = true;
                mousePos = Game.input.mousePos;
                mousePos = Game.ScreenToWorldSpace(mousePos);
                playerPos = new Point((int)pc.pos.xPos, (int)pc.pos.yPos);
                float hypo = dist(playerPos.X, mousePos.X, playerPos.Y, mousePos.Y);
                stepAmount = speed / hypo;
            }

            if(fired)
            {
                objToFire.active = true;

                if(step > 3)
                {
                    step = 0;
                    objToFire.active = false;
                    fired = false;
                }

                float[] move = lerp(mousePos.X, playerPos.X, mousePos.Y, playerPos.Y);
                objToFire.setPos((int)move[0], (int)move[1]);
            }
        }

        float[] lerp(float u1, float u0, float v1, float v0)
        {
            float[] temp = new float[2];
            step += stepAmount;
            temp[0] = ((1 - step) * u0) + step * u1;
            temp[1] = ((1 - step) * v0) + step * v1;
            return temp;
        }

        float dist(float x1, float x2, float y1, float y2)
        {
            float dx = Math.Abs(x1 - x2);
            float dy = Math.Abs(y1 - y2);
            return (float)Math.Sqrt(Math.Pow(dx, 2) + Math.Pow(dy, 2));
        }

        // Do not use, incomplete
        float angle(float x1, float x2, float y1, float y2, float dist)
        {
            float oposite = Math.Abs(y2 - y1);
            float adjacent = Math.Abs(x2 - x1);
            double angle = Math.Asin(oposite / dist);
            return (float)-angle;
            //return (float)(angle / 360d);
        }
    }
}
