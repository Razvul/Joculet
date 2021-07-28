using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class Paladin:CombatClass
    {
        public Paladin(int objHP, int objMinDamage, int objMaxDamage, int objArmor, string objName) : 
            base(objHP, objMinDamage, objMaxDamage, objArmor, objName)
        {
                
        }

        public int HolyLight()
        {
            Random rdn = new Random();
            int Heal = rdn.Next(15, 40);
            HP += Heal;
            return Heal;
        }
    }
}