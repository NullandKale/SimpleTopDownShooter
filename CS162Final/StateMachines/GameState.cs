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
        Managers.EnemyManager eMan;

        public quad background;
        public quad playerCharacter;
        public quad[] bullets;
        public quad[] badGuy;

        public GameState()
        {
            Game.worldMaxX = 1800;
            Game.worldMaxY = 1000;

            pState = GameStateManager.man.pState;
            updaters = new List<Action>();
            col = new Managers.CollisionManager(100);

            background = new quad("Content/grass.png");
            background.pos.xScale = 1f / 2f;
            background.pos.yScale = 1f / 2f;
            updaters.Add(background.update);

            playerCharacter = new quad("Content/roguelikeCharBeard_transparent.png");
            playerCharacter.AddComponent(new cFollowCamera(playerCharacter));
            cCollider playerCollider = new cCollider(playerCharacter);
            cMouseFire playerBulletMan = new cMouseFire(playerCharacter);
            playerCharacter.AddComponent(playerCollider);
            playerCharacter.AddComponent(new cKeyboardMoveandCollide(5, playerCollider));
            playerCharacter.AddComponent(playerBulletMan);
            playerCharacter.pos.xPos = Game.window.Width / 2 + 10;
            playerCharacter.pos.yPos = Game.window.Height / 2 + 10;
            playerCharacter.AddComponent(new cDEBUG_POS());
            playerCharacter.tag = "Player";
            updaters.Add(playerCharacter.update);

            bullets = new quad[10];
            for(int i = 0; i < bullets.Length; i++)
            {
                bullets[i] = new quad("Content/bullet.png");
                bullets[i].active = false;
                bullets[i].AddComponent(new cDeactivateOnCollide(bullets[i], playerCharacter));
                bullets[i].AddComponent(new cDeactivateAfter(1000));
                cFireable bulletFireable = new cFireable(bullets[i], 20);
                bullets[i].AddComponent(bulletFireable);
                playerBulletMan.addBullet(bulletFireable);
                updaters.Add(bullets[i].update);
            }

            badGuy = new quad[5000];
            for (int j = 0; j < badGuy.Length; j++)
            {
                badGuy[j] = new quad("Content/roguelikeCharBeard_transparent.png");
                cCollider badguyCollider = new cCollider(badGuy[j]);
                badGuy[j].AddComponent(badguyCollider);
                badGuy[j].AddComponent(new cEnemyAI(3, badguyCollider, playerCharacter, 300));
                badGuy[j].active = false;
                updaters.Add(badGuy[j].update);
            }
            eMan = new Managers.EnemyManager(badGuy, playerCharacter);
            updaters.Add(eMan.update);
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
