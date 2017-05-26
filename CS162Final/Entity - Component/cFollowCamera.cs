using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nullEngine.Entity___Component
{
    class cFollowCamera : iComponent
    {
        int speed;
        int currentPlace;
        bool doLerp;

        public cFollowCamera(int speed, bool lerp)
        {
            this.speed = speed;
            currentPlace = 0;
            doLerp = lerp;
        }


        public void Run(renderable r)
        {
            if(doLerp)
            {
                Game.SetWindowCenter(lerp(Game.worldCenterX, (int)r.pos.xPos), lerp(Game.worldCenterY, (int)r.pos.yPos));
            }
            else
            {
                Game.SetWindowCenter((int)r.pos.xPos, (int)r.pos.yPos);
            }
        }

        int lerp(int v0, int v1)
        {
            if(currentPlace > speed)
            {
                currentPlace = 0;
            }
            float t = (float)currentPlace / (float)speed;
            currentPlace++;

            return (int)((1f - t) * (float)v0 + t * (float)v1);
        }
    }
}
