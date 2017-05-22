using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;

namespace OpenTKTest1.StateMachines
{
    class PauseState : iState
    {
        GameState gState;
        MenuState mState;

        List<Action> updaters;

        Button returnToGameButton;
        Button optionsButton;
        Button exitToMenu;

        Button areYouSure;
        Button yes;
        Button no;

        const string returnText = "Return to Game";
        const string optionsText = "Options";
        const string exitText = "Exit to Main Menu";

        const string areYouSureText = "This will delete all progress. Are you sure?";
        const string yesText = "Yes";
        const string noText = "No";

        bool isConfirmationOpen;

        public PauseState()
        {
            gState = GameStateManager.man.gState;
            mState = GameStateManager.man.mState;
            updaters = new List<Action>();

            returnToGameButton = new Button(returnText, Game.buttonBackground, toGameState, OpenTK.Input.MouseButton.Left);
            returnToGameButton.SetCenterPos(new Point(Game.window.Width / 2, Game.window.Height / 2));
            updaters.Add(returnToGameButton.update);

            optionsButton = new Button(optionsText, Game.buttonBackground, "Not Implemented", OpenTK.Input.MouseButton.Left);
            optionsButton.SetCenterPos(new Point(Game.window.Width / 2, Game.window.Height / 2 + 60));
            updaters.Add(optionsButton.update);

            exitToMenu = new Button(exitText, Game.buttonBackground, confirmation, OpenTK.Input.MouseButton.Left);
            exitToMenu.SetCenterPos(new Point(Game.window.Width / 2, Game.window.Height / 2 + 120));
            updaters.Add(exitToMenu.update);

            areYouSure = new Button(areYouSureText, Game.buttonBackground, "", OpenTK.Input.MouseButton.Left);
            areYouSure.SetCenterPos(new Point(Game.window.Width / 2, Game.window.Height / 2));
            areYouSure.SetActive(false);
            updaters.Add(areYouSure.update);

            yes = new Button(yesText, Game.buttonBackground, toMenuState, OpenTK.Input.MouseButton.Left);
            yes.SetCenterPos(new Point(Game.window.Width / 2 - 60, Game.window.Height / 2 + 60));
            yes.SetActive(false);
            updaters.Add(yes.update);

            no = new Button(noText, Game.buttonBackground, confirmation, OpenTK.Input.MouseButton.Left);
            no.SetCenterPos(new Point(Game.window.Width / 2 + 60, Game.window.Height / 2 + 60));
            no.SetActive(false);
            updaters.Add(no.update);

            isConfirmationOpen = false;
        }

        public void enter()
        {
            Console.WriteLine("Entered PauseState");
        }

        public void update()
        {
            checkStates();
            if(Game.input.KeyFallingEdge(OpenTK.Input.Key.Escape))
            {
                toGameState();
            }

            for (int i = 0; i < updaters.Count; i++)
            {
                updaters[i].Invoke();
            }
        }

        private void toGameState()
        {
            Console.WriteLine("Changing to GameState");
            GameStateManager.man.CurrentState = GameStateManager.man.gState;
            GameStateManager.man.CurrentState.enter();
        }

        private void toMenuState()
        {
            Console.WriteLine("Changing to MenuState");
            GameStateManager.man.CurrentState = GameStateManager.man.mState;
            mState.enter();
        }

        private void confirmation()
        {
            Console.WriteLine("Confirmation Called");
            isConfirmationOpen = !isConfirmationOpen;
            returnToGameButton.SetActive(!isConfirmationOpen);
            optionsButton.SetActive(!isConfirmationOpen);
            exitToMenu.SetActive(!isConfirmationOpen);

            areYouSure.SetActive(isConfirmationOpen);
            yes.SetActive(isConfirmationOpen);
            no.SetActive(isConfirmationOpen);
        }

        private void checkStates()
        {
            if(gState == null)
            {
                gState = GameStateManager.man.gState;
            }
            if(mState == null)
            {
                mState = GameStateManager.man.mState;
            }
        }
    }
}
