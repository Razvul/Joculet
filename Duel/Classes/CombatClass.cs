using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class CombatClass
    {
        #region Proprietati
        private int hp;
        public int HP
        {
            get { return hp; }
            set { hp = value; }
        }

        private int maxHP;
        public int MaxHP
        {
            get { return maxHP; }
            set { maxHP = value; }
        }


        private int minDamage;
        public int MinDamage
        {
            get { return minDamage; }
            set { minDamage = value; }
        }

        private int maxDamage;
        public int MaxDamage
        {
            get { return maxDamage; }
            set { maxDamage = value; }
        }

        private int armor;
        public int Armor
        {
            get { return armor; }
            set { armor = value; }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string classType;
        public string ClassType
        {
            get { return classType; }
            set { classType = value; }
        }

        #endregion

        public CombatClass(int objHP, int objMinDamage, int objMaxDamage, int objArmor, string objName, string objClass)
        {
            HP = objHP;
            MinDamage = objMinDamage;
            MaxDamage = objMaxDamage;
            Armor = objArmor;
            Name = objName;
            ClassType = objClass;
        }

        public int AttackValue(int objMinDamage, int objMaxDamage)
        {
            MinDamage = objMinDamage;
            MaxDamage = objMaxDamage;
            Random rnd = new Random();
            int RandomAttackDamage = rnd.Next(MinDamage, MaxDamage + 1);
            return RandomAttackDamage;
        }
    }
}