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
        private int MinDamage;
        private int MaxDamage;
        private int Armor;
        private string Name;

        //public CombatClass(int objHP, int objDamage, int objArmor, string objName)
        //{
        //    HP = objHP;
        //    Damage = objDamage;
        //    Armor = objArmor;
        //    Name = objName;
        //}

        public int AttackValue(int objMinDamage, int objMaxDamage)
        {
            MinDamage = objMinDamage;
            MaxDamage = objMaxDamage;
            Random rnd = new Random();
            int RandomAttackDamage = rnd.Next(MinDamage, MaxDamage);
            Console.WriteLine($"Your damage is  {RandomAttackDamage}");
            return RandomAttackDamage;
        }

        //public int DamageDone()
        //{
        //    return AttackValue() - Armor;
        //}
    }
}