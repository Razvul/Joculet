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
        private int Armor;
        private string Name;

        public CombatClass(int objHP, int objDamage, int objArmor, string objName)
        {
            HP = objHP;
            Damage = objDamage;
            Armor = objArmor;
            Name = objName;
        }

        public void Attack()
        {

        }
    }
}