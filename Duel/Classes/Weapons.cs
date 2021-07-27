using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class Weapons
    {
        //public int[] Gorehowl = { 7, 15 };
        //public int[] Warbreaker = { 5, 11 };
        //public int[] LightsVengeance = { 6, 10 };
        //public int MinDamage;
        //public int MaxDamage;
        //public string WeaponName;

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

        private string weaponName;

        public string WeaponName
        {
            get { return weaponName; }
            set { weaponName = value; }
        }
    }
}