using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class CombatClass
    {
        private int HP;
        private int Damage;
        private string Name;

        public CombatClass(int objHP, int objDamage, string objName)
        {
            HP = objHP;
            Damage = objDamage;
            Name = objName;
        }

        public void Attack()
        {

        }
    }
}
