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
        public quad f;
        public quad[,] badGuy;

        public GameState()
        {
            pState = GameStateManager.man.pState;
            updaters = new List<Action>();
            col = new Managers.CollisionManager(512);

            background = new quad("Content/grass.png");
            background.pos.xScale = 1f / 2f;
            background.pos.yScale = 1f / 2f;
            updaters.Add(background.update);

            goodFPS = new Button("FPS - Good", Game.buttonBackground, "", OpenTK.Input.MouseButton.Left);
            updaters.Add(goodFPS.update);

            badFPS = new Button("FPS - Bad", Game.buttonBackground, "", OpenTK.Input.MouseButton.Left);
            updaters.Add(badFPS.update);

            playerCharacter = new quad("Content/roguelikeCharBeard_transparent.png");
            cCollider playerCollider = new cCollider(playerCharacter);
            playerCharacter.AddComponent(playerCollider);
            playerCharacter.AddComponent(new cKeyboardMoveandCollide(5, playerCollider));
            playerCharacter.AddComponent(new cFollowCamera(10, false));
            playerCharacter.pos.xPos = Game.window.Width / 2 + 10;
            playerCharacter.pos.yPos = Game.window.Height / 2 + 10;
            playerCharacter.AddComponent(new cDEBUG_POS());
            updaters.Add(playerCharacter.update);

            f = new quad(Game.font.getTile((int)letter.F));
            f.active = false;
            f.AddComponent(new cMouseFire(f, 1, 0.1f, playerCharacter));
            updaters.Add(f.update);

            badGuy = new quad[10, 10];
            for (int i = 0; i < badGuy.GetLength(0); i++)
            {
                for (int j = 0; j < badGuy.GetLength(1); j++)
                {
                    badGuy[i, j] = new quad("Content/roguelikeCharBeard_transparent.png");
                    badGuy[i, j].AddComponent(new cCollider(badGuy[i, j]));
                    badGuy[i, j].pos.xPos = 200 * i;
                    badGuy[i, j].pos.yPos = 200 * j;
                    updaters.Add(badGuy[i, j].update);
                }
            }

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

            if(Game.input.isClickedRising(OpenTK.Input.MouseButton.Left))
            {
                f.active = true;
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
