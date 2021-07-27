using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class Warrior : CombatClass
    {
        public Warrior(int objHP, int objMinDamage, int objMaxDamage, int objArmor, string objName):
            base(objHP, objMinDamage, objMaxDamage, objArmor, objName)
        {
        }
        public int Weapon;
        public int Helmet;

        public int MortalStrike()
        {
            int crit = 0;
            Random rdn = new Random();
            int HighDamage = rdn.Next(20, 50);
            crit = HighDamage * AttackValue(MinDamage, MaxDamage) / 100;
            return crit;
        }
    }
}