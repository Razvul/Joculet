using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class CombatClass
    {
        public int HP;
        public int MinDamage;
        public int MaxDamage;
        public int Armor;
        public string Name;

        public CombatClass(int objHP, int objMinDamage, int objMaxDamage, int objArmor, string objName)
        {
            HP = objHP;
            MinDamage = objMinDamage;
            MaxDamage = objMaxDamage;
            Armor = objArmor;
            Name = objName;
        }

        public int AttackValue(int objMinDamage, int objMaxDamage)
        {
            MinDamage = objMinDamage;
            MaxDamage = objMaxDamage;
            Random rnd = new Random();
            int RandomAttackDamage = rnd.Next(MinDamage, MaxDamage);
            Console.WriteLine($"Your damage is  {RandomAttackDamage}");
            return RandomAttackDamage;
        }


    }
}