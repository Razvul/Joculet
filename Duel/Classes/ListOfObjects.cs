using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class ListOfObjects
    {
        private readonly Warrior Varian = new Warrior(65, 3, 6, 5, "Varian");
        private readonly Warrior Garrosh = new Warrior(70, 4, 6, 5, "Garrosh");

        private readonly Paladin Arthas = new Paladin(62, 3, 5, 6, "Arthas");
        private readonly Paladin Tirion = new Paladin(63, 3, 6, 4, "Tirion");

        public ListOfObjects()
        {
            LoadList();
        }

        private readonly Weapons Gorehowl = new Weapons()
        {
            MinDamage = 6,
            MaxDamage = 12,
            WeaponName = "Gorehowl"
        };
        private readonly Weapons Warbreaker = new Weapons()
        {
            MinDamage = 5,
            MaxDamage = 13,
            WeaponName = "Warbreaker"
        };
        private readonly Weapons LightsVengeance = new Weapons()
        {
            MinDamage = 6,
            MaxDamage = 11,
            WeaponName = "Light's Vengeance"
        };
        private readonly Weapons Ashbringer = new Weapons()
        {
            MinDamage = 5,
            MaxDamage = 13,
            WeaponName = "Ashbringer"
        };
        readonly List<CombatClass> ListaClase = new List<CombatClass>();

        private void LoadListClasses()
        {
            ListaClase.Add(Varian);
            ListaClase.Add(Garrosh);
            ListaClase.Add(Arthas);
            ListaClase.Add(Tirion);
        }

        readonly List<Weapons> ListaArme = new List<Weapons>();

        private void LoadListWeapons()
        {
            ListaArme.Add(Gorehowl);
            ListaArme.Add(Warbreaker);
            ListaArme.Add(LightsVengeance);
            ListaArme.Add(Ashbringer);
        }

        private void LoadList()
        {
            LoadListClasses();
            LoadListWeapons();
        }

        public List<Weapons> GetWeapons()
        {
            return ListaArme;
        }

        public List<CombatClass> GetClasses()
        {
            return ListaClase;
        }
    }
}