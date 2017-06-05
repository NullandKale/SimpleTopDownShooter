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

        renderable[] enemies;
        renderable playerCharacter;
        List<int> activeEnemies;
        int level;

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
            for(int i = 0; i < activeEnemies.Count; i++)
            {
                if(enemies[activeEnemies[i]].active)
                {
                    return true;
                }
            }
            return false;
        }

        public static void resurrect(int arrayPos, renderable r)
        {
            if (man.activeEnemies.Contains(arrayPos) && r.active == false)
            {
                man.spawn(arrayPos, r);
            }
        }

        public void spawn(int arrayPos, renderable r)
        {
            if(activeEnemies == null)
            {
                activeEnemies = new List<int>();
            }

            if(!activeEnemies.Contains(arrayPos))
            {
                activeEnemies.Add(arrayPos);
            }

            r.setPos(getRandomPos());
            r.active = true;
        }

        public void cleanEnemies()
        {
            for(int i = 0; i < enemies.Length; i++)
            {
                enemies[i].active = false;
            }

            activeEnemies.Clear();
        }

        public void respawn(int level)
        {
            int count = getEnemyCount(level);
            if(count > enemies.Length)
            {
                count = enemies.Length;
            }

            for(int i = 0; i < activeEnemies.Count; i++)
            {
                enemies[activeEnemies[i]].active = false;
            }

            activeEnemies.Clear();

            for(int i = 0; i < count; i++)
            {
                spawn(i, enemies[i]);
                activeEnemies.Add(i);
            }

            Console.WriteLine("Level: " + level + " with " + count + " enemies to Kill");
        }

        public Point getRandomPos()
        {
            Point p = new Point(Game.rng.Next(5, Game.worldMaxX - 64), Game.rng.Next(5, Game.worldMaxY - 64));

            //if the point is too close to the player get a new point
            if(Math.Abs(p.X - playerCharacter.pos.xPos) < 100 && Math.Abs(p.Y - playerCharacter.pos.yPos) < 100)
            {
                p = getRandomPos();
            }
            return p;
        }

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
