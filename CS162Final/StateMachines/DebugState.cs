using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using nullEngine.Entity___Component;
using System.Drawing;

namespace nullEngine.StateMachines
{
    class DebugState : iState
    {
        public iState previousState;

        public List<Action> updaters;

        TextureAtlas overworldTileAtlas;
        Button[] tiles;

        Point windowPos;
        Point lastWorldPos;

        public DebugState()
        {
            updaters = new List<Action>();

            overworldTileAtlas = new TextureAtlas("Content/overworld.png", 21, 9, 16, 16, 0);

            tiles = new Button[189];

            for (int i = 0; i < 189; i++)
            {
                tiles[i] = new Button(32 ,i.ToString(), overworldTileAtlas.getTile(i), i.ToString(), OpenTK.Input.MouseButton.Left, this);
                tiles[i].SetPos(((i % 21) * 64 + (i % 21 * 72)), ((i / 21) * 64) + (i / 21 * 72));
                updaters.Add(tiles[i].update);
            }
        }

        public void enter()
        {
            Console.WriteLine("Entered Debug State");
            windowPos = new Point(Game.worldCenterX, Game.worldCenterY);
            if(lastWorldPos != Point.Empty)
            {
                Game.SetWindowCenter(lastWorldPos.X, lastWorldPos.Y);
            }
        }

        public void update()
        {
            for(int i = 0; i < updaters.Count; i++)
            {
                updaters[i].Invoke();
            }

            if(Game.input.KeyFallingEdge(OpenTK.Input.Key.Escape))
            {
                exitDebugState();
            }

            MoveScreen();
        }

        public void exitDebugState()
        {
            Console.WriteLine("Exiting Debug State");
            lastWorldPos = new Point(Game.worldCenterX, Game.worldCenterY);
            Game.SetWindowCenter(windowPos.X, windowPos.Y);
            GameStateManager.man.CurrentState = previousState;
            previousState.enter();
        }

        public void MoveScreen()
        {
            int halfWindowWidth = Game.windowRect.Width / 2;
            int halfWindowHeight = Game.windowRect.Height / 2;

            int moveX = 0;
            int moveY = 0;
            int speed = 5;

            moveX = (Game.windowRect.Width / 2) + Game.windowRect.X;
            moveY = (Game.windowRect.Height / 2) + Game.windowRect.Y;

            if(Game.input.KeyHeld(OpenTK.Input.Key.A))
            {
                moveX -= speed;
            }

            if(Game.input.KeyHeld(OpenTK.Input.Key.D))
            {
                moveX += speed;
            }

            if(Game.input.KeyHeld(OpenTK.Input.Key.W))
            {
                moveY -= speed;
            }

            if (Game.input.KeyHeld(OpenTK.Input.Key.S))
            {
                moveY += speed;
            }

            Game.SetWindowCenter(moveX, moveY);
        }
    }
}
