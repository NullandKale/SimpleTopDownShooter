using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;

namespace OpenTKTest1.StateMachines
{
    class MenuState : iState
    {
        GameState gState;
        List<Action> updaters;

        Button newGameButton;
        Button loadGameButton;
        Button settingsButton;
        Button exitButton;

        const string newGameText = "Start New Game";
        const string loadGameText = "Load Game - Not Implemented";
        const string settingsText = "Settings - Not Implemented";
        const string exitText = "Exit To Desktop";

        public MenuState()
        {
            gState = GameStateManager.man.gState;
            updaters = new List<Action>();

            newGameButton = new Button(newGameText, Game.buttonBackground, toGameState, OpenTK.Input.MouseButton.Left);
            newGameButton.SetCenterPos(new Point(Game.window.Width / 2, Game.window.Height / 2));
            updaters.Add(newGameButton.update);

            loadGameButton = new Button(loadGameText, Game.buttonBackground, "Not Implemented", OpenTK.Input.MouseButton.Left);
            loadGameButton.SetCenterPos(new Point(Game.window.Width / 2, (Game.window.Height / 2) + 60));
            updaters.Add(loadGameButton.update);

            settingsButton = new Button(settingsText, Game.buttonBackground, "Not Implemented Either", OpenTK.Input.MouseButton.Left);
            settingsButton.SetCenterPos(new Point(Game.window.Width / 2, (Game.window.Height / 2) + 120));
            updaters.Add(settingsButton.update);

            exitButton = new Button(exitText, Game.buttonBackground, exit, OpenTK.Input.MouseButton.Left);
            exitButton.SetCenterPos(new Point(Game.window.Width / 2, (Game.window.Height / 2) + 180));
            updaters.Add(exitButton.update);
        }

        public void enter()
        {
            Console.WriteLine("Entered MenuState");
        }

        public void update()
        {
            checkStates();
            for(int i = 0; i < updaters.Count; i++)
            {
                updaters[i].Invoke();
            }
        }

        private void checkStates()
        {
            if (gState == null)
            {
                gState = GameStateManager.man.gState;
            }
        }

        private void exit()
        {
            Console.WriteLine("Good Bye!");
            Program.exit();
        }

        private void toGameState()
        {
            Console.WriteLine("Changing to GameState");
            GameStateManager.man.CurrentState = GameStateManager.man.gState;
            gState.enter();
        }

    }
}
