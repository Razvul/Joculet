using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace Classes
{
    public class ListOfObjects
    {
        private readonly Warrior Varian = new Warrior(65, 3, 6, 5, "Varian", "Warrior");
        private readonly Warrior Garrosh = new Warrior(70, 4, 6, 5, "Garrosh", "Warrior");

        private readonly Paladin Arthas = new Paladin(62, 3, 5, 6, "Arthas", "Paladin");
        private readonly Paladin Tirion = new Paladin(63, 3, 6, 4, "Tirion", "Paladin");

        private List<CombatClass> _listaLuptatori = new List<CombatClass>();
        private List<Weapons> _listaArme = new List<Weapons>();

        #region Singleton
        private static readonly ListOfObjects _luptatori = new ListOfObjects();
        private static readonly ListOfObjects _arme = new ListOfObjects();

        public ListOfObjects()
        {
            //LoadList();
            LoadListe();
            //SaveListe();
        }

        public static ListOfObjects GetLuptatori()
        {
            return _luptatori;
        }

        public static ListOfObjects GetArme()
        {
            return _arme;
        }
        #endregion

        private void LoadListe()
        {
            LoadArme();
            LoadLuptatori();
        }

        private void SaveListe()
        {
            SaveClassList();
            SaveWeaponList();
        }

        private void LoadLuptatori()
        {
            var pathLuptatori = $@"E:\VS_Project\Joculet\DataBase\ListOfCombatClasses.json";
            using (StreamReader sr = new StreamReader(pathLuptatori))
            {
                string y = sr.ReadToEnd();
                _listaLuptatori = JsonConvert.DeserializeObject<List<CombatClass>>(y);
            }
        }

        private void LoadArme()
        {
            var pathArme = $@"E:\VS_Project\Joculet\DataBase\ListOfWeapons.json";

            using (StreamReader sr = new StreamReader(pathArme))
            {
                string y = sr.ReadToEnd();
                _listaArme = JsonConvert.DeserializeObject<List<Weapons>>(y);
            }
        }

        public void SaveClassList()
        {
            var pathLuptatori = $@"E:\VS_Project\Joculet\DataBase\ListOfCombatClasses.json";

            var textLuptatori = JsonConvert.SerializeObject(_listaLuptatori);

            File.WriteAllText(pathLuptatori, textLuptatori);
        }

        public void SaveWeaponList()
        {
            var pathArme = $@"E:\VS_Project\Joculet\DataBase\ListOfWeapons.json";

            var textArme = JsonConvert.SerializeObject(_listaArme);

            File.WriteAllText(pathArme, textArme);
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
            return _listaArme;
        }

        public List<CombatClass> GetClasses()
        {
            return _listaLuptatori;
        }
    }
}