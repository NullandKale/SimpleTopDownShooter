using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;

namespace nullEngine.StateMachines
{
    class GameStateManager : StateManager
    {
        //static singleton reference
        public static GameStateManager man;

        //state storage
        public GameState gState;
        public MenuState mState;
        public PauseState pState;

        public GameStateManager()
        {
            //singleton management
            if(man == null)
            {
                man = this;
            }
            else
            {
                Console.WriteLine("Singleton Failure @ GameStateManager");
            }

            //create each state
            mState = new MenuState();
            pState = new PauseState();
            gState = new GameState();

            //add update function to update call list
            Game.window.UpdateFrame += update;
        }

        public override void update(object sender, FrameEventArgs e)
        {
            //on startup set current state to menuState and update current state
            if(CurrentState == null)
            {
                CurrentState = mState;
                mState.enter();
            }
            else
            {
                //update current state
                CurrentState.update();
            }
        }
    }
}
