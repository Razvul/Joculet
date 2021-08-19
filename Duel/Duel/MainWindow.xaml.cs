using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Classes;


namespace Duel
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Warrior Varian;
        private readonly Paladin Arthas;
        private readonly Weapons Gorehowl;
        private readonly Weapons Warbreaker;
        private readonly Weapons LightsVengeance;


        public MainWindow()
        {
            InitializeComponent();
            var ObjectList = new ListOfObjects();
            var Classes = ObjectList.GetClasses();
            var Weapons = ObjectList.GetWeapons();

            //Varian = new Warrior(65, 3, 6, 5, "Varian");
            //Arthas = new Paladin(62, 3, 5, 6, "Arthas");
            //var ArthasMaxHP = Arthas.HP;
                        
            //ComboBox_Class_Player.Items.Add(Varian);
            //ComboBox_Class_Player.Items.Add(Arthas);

            //ComboBox_Class_Oponent.Items.Add(Varian);
            //ComboBox_Class_Oponent.Items.Add(Arthas);

            foreach (var Class in Classes)
            {
                ComboBox_Class_Player.Items.Add(Class);
                ComboBox_Class_Oponent.Items.Add(Class);
            }
            ComboBox_Class_Player.SelectedIndex = 0;
            ComboBox_Class_Oponent.SelectedIndex = 1;

            //Gorehowl = new Weapons()
            //{
            //    MinDamage = 6,
            //    MaxDamage = 12,
            //    WeaponName = "Gorehowl"
            //};
            //Warbreaker = new Weapons()
            //{
            //    MinDamage = 5,
            //    MaxDamage = 13,
            //    WeaponName = "Warbreaker"
            //};
            //LightsVengeance = new Weapons()
            //{
            //    MinDamage = 6,
            //    MaxDamage = 11,
            //    WeaponName = "Light's Vengeance"
            //};

            foreach (var Weapon in Weapons)
            {
                ComboBox_Weapon_Player.Items.Add(Weapon);
                ComboBox_Weapon_Oponent.Items.Add(Weapon);
            }

            //ComboBox_Weapon_Player.Items.Add(Gorehowl);
            //ComboBox_Weapon_Player.Items.Add(Warbreaker);
            //ComboBox_Weapon_Player.Items.Add(LightsVengeance);
            ComboBox_Weapon_Player.SelectedIndex = 2;

            //ComboBox_Weapon_Oponent.Items.Add(Gorehowl);
            //ComboBox_Weapon_Oponent.Items.Add(Warbreaker);
            //ComboBox_Weapon_Oponent.Items.Add(LightsVengeance);
            ComboBox_Weapon_Oponent.SelectedIndex = 0;            
        }

        public int DamageDone(CombatClass fighter1, CombatClass fighter2)
        {
            int attackValue = fighter1.AttackValue(fighter1.MinDamage, fighter1.MaxDamage) - fighter2.Armor;
            fighter2.HP -= attackValue;
            return attackValue;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Button_Strike.IsEnabled = false;
            Button_Special_Skill.IsEnabled = false;
            Button_Wait_Oponent.IsEnabled = false;
        }

        #region Butoane
        private void Button_StartFight_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxes(false);

            var classPlayer = (CombatClass)ComboBox_Class_Player.SelectedItem;
            var classOponent = (CombatClass)ComboBox_Class_Oponent.SelectedItem;
            var weaponPlayer = (Weapons)ComboBox_Weapon_Player.SelectedItem;
            var weaponOponent = (Weapons)ComboBox_Weapon_Oponent.SelectedItem;

            classPlayer.MinDamage += weaponPlayer.MinDamage;
            classPlayer.MaxDamage += weaponPlayer.MaxDamage;

            classOponent.MinDamage += weaponOponent.MinDamage;
            classOponent.MaxDamage += weaponOponent.MaxDamage;

            Button_StartFight.IsEnabled = false;
            Button_Strike.IsEnabled = true;
            Button_Special_Skill.IsEnabled = true;
        }

        private void Button_Strike_Click(object sender, RoutedEventArgs e)
        {
            var x = (CombatClass)ComboBox_Class_Player.SelectedItem;
            var y = (CombatClass)ComboBox_Class_Oponent.SelectedItem;
            string rezultat = $"{x.Name} dealt {DamageDone(x, y)} damage to {y.Name}\n{y.Name} has {y.HP} HP left";
            ListBox_DamageOutput.Items.Add(rezultat);

            Button_Wait_Oponent.IsEnabled = true;
            Button_Strike.IsEnabled = false;
            Button_Special_Skill.IsEnabled = false;

            // daca n-am winner, fac astea
            if (CheckWinner(x, y))
            {
                Button_Special_Skill.IsEnabled = false;
                Button_Strike.IsEnabled = false;
                Button_Wait_Oponent.IsEnabled = false;
            }
            if (x.HP <= 0)
            {
                Label_Rezultat.Content = $"Winner is {y.Name}";
            }
            if (y.HP <= 0)
            {
                Label_Rezultat.Content = $"Winner is {x.Name}";
            }

        }

        private void Button_Wait_Oponent_Click(object sender, RoutedEventArgs e)
        {
            var x = (CombatClass)ComboBox_Class_Oponent.SelectedItem;
            var y = (CombatClass)ComboBox_Class_Player.SelectedItem;
            string rezultat = $"{x.Name} dealt {DamageDone(x, y)} damage to {y.Name}\n{y.Name} has {y.HP} HP left";
            ListBox_DamageOutput.Items.Add(rezultat);

            Button_Strike.IsEnabled = true;
            Button_Special_Skill.IsEnabled = true;
            Button_Wait_Oponent.IsEnabled = false;

            // daca n-am winner, fac astea
            if (CheckWinner(x, y))
            {
                Button_Special_Skill.IsEnabled = false;
                Button_Strike.IsEnabled = false;
                Button_Wait_Oponent.IsEnabled = false;
            }

            if (x.HP <= 0)
            {
                Label_Rezultat.Content = $"Winner is {y.Name}";
            }
            if (y.HP <= 0)
            {
                Label_Rezultat.Content = $"Winner is {x.Name}";
            }
        }

        private void Button_Special_Skill_Click(object sender, RoutedEventArgs e)
        {
            Button_Special_Skill.IsEnabled = false;
            Button_Wait_Oponent.IsEnabled = true;
            Button_Strike.IsEnabled = false;
            string rezultat;

            if (ComboBox_Class_Player.SelectedIndex == 0)
            {
                var crit = Varian.MortalStrike();
                Arthas.HP -= crit;
                rezultat = $"Varian dealt {crit} crit damage to Arthas\nArthas has {Arthas.HP} HP left";
                ListBox_DamageOutput.Items.Add(rezultat);
            }
            else
            {
                var heal = Arthas.HolyLight();
                rezultat = $"Arthas healed {heal} damage\nArthas has {Arthas.HP} HP left";
                ListBox_DamageOutput.Items.Add(rezultat);
            }
        }
        #endregion

        public bool CheckWinner(CombatClass p1, CombatClass p2)
        {
            bool weHaveWinner = false;
            if (p1.HP <= 0 || p2.HP <= 0)
            {
                //sectiunea rezultat updatat
                // dezactivat butoane de strike si wait, ..
                weHaveWinner = true;
                
                //Label_Rezultat.Content=""
            }
            return weHaveWinner;
        }

        public void ComboBoxes(bool x)
        {
            ComboBox_Class_Player.IsEnabled = x;
            ComboBox_Class_Oponent.IsEnabled = x;
            ComboBox_Weapon_Player.IsEnabled = x;
            ComboBox_Weapon_Oponent.IsEnabled = x;
        }

        //public void Rezultat()
        //{
        //    if (Varian.HP < VarianMaxHP / 2 || Arthas.HP < ArthasMaxHP / 2)
        //    {
        //        Button_Strike.IsEnabled = false;
        //        Button_Special_Skill.IsEnabled = false;
        //        Button_Wait_Oponent.IsEnabled = false;

        //        if (Varian.HP > Arthas.HP)
        //        {
        //            Label_Rezultat.Content = "Winner is Varian";
        //        }
        //        else
        //        {
        //            Label_Rezultat.Content = "Winner is Arthas";
        //        }
        //    }
        //}
    }
}