using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using nullEngine.Entity___Component;
using System.Drawing;

namespace nullEngine.Managers
{
    class EnemyManager
    {
        public static EnemyManager man;
        public int enemiesLeft;
        public int level;

        renderable[] enemies;
        renderable playerCharacter;
        List<int> activeEnemies;

        public EnemyManager(renderable[] enemies, renderable player)
        {
            if(man == null)
            {
                man = this;
            }
            else
            {
                throw new SingletonException(this);
            }
            this.enemies = enemies;
            activeEnemies = new List<int>();
            playerCharacter = player;
            level = 0;
        }

        public void update()
        {
            if(!checkEnemies())
            {
                level++;
                respawn(level);
            }
        }

        public bool checkEnemies()
        {
            enemiesLeft = 0;
            for(int i = 0; i < activeEnemies.Count; i++)
            {
                if(enemies[activeEnemies[i]].active)
                {
                    enemiesLeft++;
                }
            }

            if(enemiesLeft > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void resurrect(int arrayPos, renderable r)
        {
            if (man.activeEnemies.Contains(arrayPos) && r.active == false)
            {
                man.spawn(arrayPos, r);
            }
        }

        public void Reset()
        {
            level = 0;
            cleanEnemies();
        }

        public void spawn(int arrayPos, renderable r)
        {
            //if activeEnemies list is null initialize the list
            if(activeEnemies == null)
            {
                activeEnemies = new List<int>();
            }

            //
            if(!activeEnemies.Contains(arrayPos))
            {
                activeEnemies.Add(arrayPos);
            }

            r.setPos(getRandomPos());
            r.active = true;
        }

        public void cleanEnemies()
        {
            //for all of the currently active enemies make sure they are inactive
            for (int i = 0; i < activeEnemies.Count; i++)
            {
                enemies[activeEnemies[i]].active = false;
            }

            //clear the active enemeies list
            activeEnemies.Clear();
        }

        public void respawn(int level)
        {
            //get the number of enemies to spawn
            int count = getEnemyCount(level) * 10;

            //if there are not enough enemies in the pool set the number of enemies to spawn to the number of enemies in the pool
            if(count > enemies.Length)
            {
                count = enemies.Length;
            }

            cleanEnemies();

            //for all of the enemies to spawn spawn an enemy from the pool
            for(int i = 0; i < count; i++)
            {
                spawn(i, enemies[i]);
                //and add it to the active enemies list
                activeEnemies.Add(i);
            }

            //debug info
            Console.WriteLine("Level: " + level + " with " + count + " enemies to Kill");
        }

        public Point getRandomPos()
        {
            //generate a random position within the world map
            Point p = new Point(Game.rng.Next(5, Game.worldMaxX - 64), Game.rng.Next(5, Game.worldMaxY - 64));

            //if the point is too close to the player call this function again
            if(Math.Abs(p.X - playerCharacter.pos.xPos) < 100 && Math.Abs(p.Y - playerCharacter.pos.yPos) < 100)
            {
                p = getRandomPos();
            }
            //return the point
            return p;
        }

        //calculates how many enemies to spawn this is a fibonacci sequence generator
        public int getEnemyCount(int level)
        {
            if(level <= 1)
            {
                return 1;
            }
            return getEnemyCount(level - 1) + getEnemyCount(level - 2);
        }
    }
}
