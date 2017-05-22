using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;
using OpenTK.Input;
using System.Drawing;

namespace nullEngine
{
    class ButtonManager
    {
        public List<Button> Buttons = new List<Button>();

        public ButtonManager()
        {
            Game.window.UpdateFrame += update;
        }

        public void update(object sender, FrameEventArgs e)
        {
            bool doLeft = Game.input.isClickedRising(MouseButton.Left);
            bool doRight = Game.input.isClickedRising(MouseButton.Right);
            bool doMiddle = Game.input.isClickedRising(MouseButton.Middle);

            if(doLeft || doRight || doMiddle)
            {
                for(int i = 0; i < Buttons.Count; i++)
                {
                    if(Buttons[i].t.active)
                    {
                        if (Buttons[i].button == MouseButton.Right && doRight)
                        {
                            if (isWithin(Buttons[i], Game.input.mousePos))
                            {
                                Buttons[i].onClick.Invoke();
                            }
                        }

                        if (Buttons[i].button == MouseButton.Left && doLeft)
                        {
                            if (isWithin(Buttons[i], Game.input.mousePos))
                            {
                                Buttons[i].onClick.Invoke();
                            }
                        }

                        if (Buttons[i].button == MouseButton.Middle && doMiddle)
                        {
                            if (isWithin(Buttons[i], Game.input.mousePos))
                            {
                                Buttons[i].onClick.Invoke();
                            }
                        }
                    }
                }
            }
        }

        public bool isWithin(Button b, Point p)
        {
            p = Game.ScreenToWorldSpace(p);
            return (p.X > (int)b.background.pos.xPos && p.Y > (int)b.background.pos.yPos && p.X < ((int)b.background.pos.xPos + b.background.width * 4) && p.Y < ((int)b.background.pos.yPos + b.background.height * 4));
        }

        public void Add(Button b)
        {
            Buttons.Add(b);
        }
    }
}
