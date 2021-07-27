using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class Weapons
    {
        #region Proprietati
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
        #endregion
    }
}