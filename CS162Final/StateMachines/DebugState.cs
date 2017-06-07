using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using nullEngine.Entity___Component;

namespace nullEngine.StateMachines
{
    class DebugState : iState
    {
        public iState previousState;

        public List<Action> updaters;

        TextureAtlas overworldTileAtlas;
        Button[] tiles;

        public DebugState()
        {
            updaters = new List<Action>();

            overworldTileAtlas = new TextureAtlas("Content/overworld.png", 21, 9, 16, 16, 0);

            tiles = new Button[189];

            for (int i = 0; i < 189; i++)
            {
                tiles[i] = new Button(" ", overworldTileAtlas.getTile(i), i.ToString(), OpenTK.Input.MouseButton.Left, this);
                tiles[i].SetPos((i % 21) * 48, (i / 21) * 48);
                updaters.Add(tiles[i].update);
            }
        }

        public void enter()
        {
            Console.WriteLine("Entered Debug State");
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
        }

        public void exitDebugState()
        {
            Console.WriteLine("Exiting Debug State");
            GameStateManager.man.CurrentState = previousState;
            previousState.enter();
        }
    }
}
