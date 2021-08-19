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

        public MainWindow()
        {
            InitializeComponent();
            var ObjectList = new ListOfObjects();
            var Classes = ObjectList.GetClasses();
            var Weapons = ObjectList.GetWeapons();

            foreach (var Class in Classes)
            {
                ComboBox_Class_Player.Items.Add(Class);
                ComboBox_Class_Oponent.Items.Add(Class);
            }
            ComboBox_Class_Player.SelectedIndex = 0;
            ComboBox_Class_Oponent.SelectedIndex = 1;

            foreach (var Weapon in Weapons)
            {
                ComboBox_Weapon_Player.Items.Add(Weapon);
                ComboBox_Weapon_Oponent.Items.Add(Weapon);
            }

            ComboBox_Weapon_Player.SelectedIndex = 1;
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

            Label_Player.Content = classPlayer.Name;
            Label_Oponent.Content = classOponent.Name;

            Label_HP_Player.Content = classPlayer.HP;
            Label_HP_Oponent.Content = classOponent.HP;

            Button_StartFight.IsEnabled = false;
            Button_Strike.IsEnabled = true;
            Button_Special_Skill.IsEnabled = false;
        }

        private void Button_Strike_Click(object sender, RoutedEventArgs e)
        {
            var x = (CombatClass)ComboBox_Class_Player.SelectedItem;
            var y = (CombatClass)ComboBox_Class_Oponent.SelectedItem;

            string rezultat1 = $"{x.Name} dealt {DamageDone(x, y)} damage to {y.Name}\n{y.Name} has {y.HP} HP left";
            ListBox_DamageOutput.Items.Add(rezultat1);

            string rezultat2 = $"{y.Name} dealt {DamageDone(y, x)} damage to {x.Name}\n{x.Name} has {x.HP} HP left";
            ListBox_DamageOutput.Items.Add(rezultat2);

            // daca n-am winner, fac astea
            if (CheckWinner(x, y))
            {
                Button_Special_Skill.IsEnabled = false;
                Button_Strike.IsEnabled = false;
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
            //Button_Special_Skill.IsEnabled = false;
            //Button_Wait_Oponent.IsEnabled = true;
            //Button_Strike.IsEnabled = false;
            //string rezultat;

            //if (ComboBox_Class_Player.SelectedIndex == 0)
            //{
            //    var crit = Varian.MortalStrike();
            //    Arthas.HP -= crit;
            //    rezultat = $"Varian dealt {crit} crit damage to Arthas\nArthas has {Arthas.HP} HP left";
            //    ListBox_DamageOutput.Items.Add(rezultat);
            //}
            //else
            //{
            //    var heal = Arthas.HolyLight();
            //    rezultat = $"Arthas healed {heal} damage\nArthas has {Arthas.HP} HP left";
            //    ListBox_DamageOutput.Items.Add(rezultat);
            //}
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
    }
}