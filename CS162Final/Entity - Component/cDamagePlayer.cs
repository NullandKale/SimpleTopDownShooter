using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nullEngine.Entity___Component
{
    class cDamagePlayer : iComponent
    {
        renderable pc;
        cHealth pcHealth;
        int damage;
        cCollider collider;
        List<cCollider> collidingWith;

        public cDamagePlayer(renderable player,cHealth playerHealth, int damageAmount, cCollider col)
        {
            pc = player;
            pcHealth = playerHealth;
            damage = damageAmount;
            collider = col;
        }

        public void Run(renderable r)
        {
            collidingWith = Managers.CollisionManager.man.CheckCollision(collider, 20);

            for(int i = 0; i < collidingWith.Count; i++)
            {
                if(collidingWith[i].rRef == pc)
                {
                    pcHealth.damage(damage);
                }
            }
        }
    }
}
