using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class Warrior : CombatClass
    {
        public Warrior(int objHP, int objMinDamage, int objMaxDamage, int objArmor, string objName, string objClass):
            base(objHP, objMinDamage, objMaxDamage, objArmor, objName, objClass)
        {
        }
        public int Weapon;
        public int Helmet;

        public int MortalStrike()
        {
            Random rdn = new Random();
            int HighDamage = rdn.Next(20, 50);
            int crit = HighDamage * HP / 100;
            return crit;
        }
    }
}