using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System.Drawing;

namespace nullEngine.Entity___Component
{
    public abstract class renderable
    {
        public transform pos;
        public List<triangle> verts;
        protected List<iComponent> components;
        public Texture2D tex;
        public Color col;
        public bool active = true;
        public bool culled = false;
        public string tag;

        public abstract int getWidth();
        public abstract int getHeight();
        public abstract void update();
        public abstract void render();

        public Rectangle getRect()
        {
            return new Rectangle((int)pos.xPos, (int)pos.yPos, getWidth(), getHeight());
        }

        public void DistCulling()
        {
            if (Game.worldRect.IntersectsWith(getRect()))
            {
                culled = true;
            }
            else
            {
                culled = false;
            }
        }

        public void AddComponent(iComponent c)
        {
            //prevent adding multiple of the same component
            if(!components.Contains(c))
            {
                components.Add(c);
            }
        }

        public int FindComponent<T> ()
        {
            for(int i = 0; i < components.Count; i++)
            {
                Type compType = components[i].GetType();
                if (compType is T)
                {
                    return i;
                }
            }
            return -1;
        }

        public T GetComponent<T> ()
        {
            int temp = FindComponent<T>();
            if (temp == -1)
            {
                throw new Exception("Cannot Find Component");
            }
            return (T)components[FindComponent<T>()];
        }

        public void setPos(int x, int y)
        {
            pos.xPos = x;
            pos.yPos = y;
        }

        public void setRelativePos(float x, float y)
        {
            pos.xPos += x;
            pos.yPos += y;
        }
    }

    public class transform
    {
        public static int masterScale = 4;
        public float xPos = 0;
        public float yPos = 0;
        public float zPos = 0;

        public float rotZ = 0;

        public float xScale = 1;
        public float yScale = 1;

        public Matrix4 modelViewMatrix;

        public void updateMatrix()
        {
              modelViewMatrix = Matrix4.CreateScale(xScale * masterScale, yScale * masterScale, 1f) * 
                Matrix4.CreateRotationZ(rotZ) * 
                Matrix4.CreateTranslation(xPos, yPos, zPos);
        }
    }

    public struct triangle
    {
        public Vector2 a;
        public Vector2 b;
        public Vector2 c;
    }
}
