using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using nullEngine.Entity___Component;

namespace nullEngine.StateMachines
{
    class GameState : iState
    {
        PauseState pState;
        List<Action> updaters;
        Managers.CollisionManager col;

        public Button goodFPS;
        public Button badFPS;

        public quad background;
        public quad playerCharacter;
        public quad box;

        public GameState()
        {
            pState = GameStateManager.man.pState;
            updaters = new List<Action>();
            col = new Managers.CollisionManager();

            background = new quad("Content/grass.png");
            background.pos.xScale = 1f / 2f;
            background.pos.yScale = 1f / 2f;
            updaters.Add(background.update);

            goodFPS = new Button("FPS - Good", Game.buttonBackground, "", OpenTK.Input.MouseButton.Left);
            updaters.Add(goodFPS.update);

            badFPS = new Button("FPS - Bad", Game.buttonBackground, "", OpenTK.Input.MouseButton.Left);
            updaters.Add(badFPS.update);

            playerCharacter = new quad("Content/roguelikeCharBeard_transparent.png");
            playerCharacter.AddComponent(new KeyboardControl(5));
            playerCharacter.AddComponent(new cDeactivateOnCollide(playerCharacter));
            playerCharacter.pos.xPos = Game.window.Width / 2;
            playerCharacter.pos.yPos = Game.window.Height / 2;
            updaters.Add(playerCharacter.update);

            box = new quad("Content/roguelikeCharBeard_transparent.png");
            box.AddComponent(new cCollider(box));
            updaters.Add(box.update);

        }

        public void enter()
        {
            Console.WriteLine("Entered GameState");
        }

        public void update()
        {
            checkStates();
            col.update();

            if(Game.input.KeyFallingEdge(OpenTK.Input.Key.Escape))
            {
                toPauseState();
            }

            for (int i = 0; i < updaters.Count; i++)
            {
                updaters[i].Invoke();
            }

            if(Game.frameTime <= 18)
            {
                goodFPS.SetActive(true);
                badFPS.SetActive(false);
            }
            else
            {
                if(Game.tick == 15)
                {
                    Console.WriteLine(Game.frameTime);
                }
                goodFPS.SetActive(false);
                badFPS.SetActive(true);
            }
        }

        private void toPauseState()
        {
            Console.WriteLine("Changing to PauseState");
            GameStateManager.man.CurrentState = GameStateManager.man.pState;
            pState.enter();
        }

        private void checkStates()
        {
            if (pState == null)
            {
                pState = GameStateManager.man.pState;
            }
        }
    }
}
