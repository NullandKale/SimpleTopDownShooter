using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;

namespace OpenTKTest1.StateMachines
{
    class GameStateManager : StateManager
    {
        public static GameStateManager man;

        public GameState gState;
        public MenuState mState;
        public PauseState pState;

        public GameStateManager()
        {
            if(man == null)
            {
                man = this;
            }
            else
            {
                Console.WriteLine("Singleton Failure @ GameStateManager");
            }

            mState = new MenuState();
            pState = new PauseState();
            gState = new GameState();

            Game.window.UpdateFrame += update;
        }

        public override void update(object sender, FrameEventArgs e)
        {
            if(CurrentState == null)
            {
                CurrentState = mState;
                mState.enter();
            }
            else
            {
                CurrentState.update();
            }
        }
    }
}
