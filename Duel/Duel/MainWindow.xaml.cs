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
using System.Windows.Threading;
using Classes;


namespace Duel
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public DispatcherTimer timer;
        

        public MainWindow()
        {
            InitializeComponent();
            var ObjectList = new ListOfObjects();
            var Classes = ObjectList.GetClasses();
            var Weapons = ObjectList.GetWeapons();

            LoadDispatcher();

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
            Button_Special_Skill.IsEnabled = true;
        }

        private /*async*/ void Button_Strike_Click(object sender, RoutedEventArgs e)
        {
            var player = (CombatClass)ComboBox_Class_Player.SelectedItem;
            var oponent = (CombatClass)ComboBox_Class_Oponent.SelectedItem;

            Attack(player, oponent);
            Button_Strike.IsEnabled = false;

            if (oponent.HP < 0)
            {
                Label_HP_Oponent.Content = 0;
            }
            else
            {
                Label_HP_Oponent.Content = oponent.HP;
            }

            //await Task.Delay(10000);
            timer.Start();         
        }

        private void Button_Special_Skill_Click(object sender, RoutedEventArgs e)
        {
            var player = (CombatClass)ComboBox_Class_Player.SelectedItem;
            var oponent = (CombatClass)ComboBox_Class_Oponent.SelectedItem;
                      
            string mesaj = "Special skill used!";
            ListBox_DamageOutput.Items.Add(mesaj);

            if (player.ClassType == "Warrior")
            {
                Warrior razboinic = (Warrior)player;

                int critDmg = razboinic.MortalStrike();
                oponent.HP -= critDmg;
                Label_HP_Oponent.Content = oponent.HP;

                string attack = $"{razboinic.Name} dealt {critDmg} critical damage to {oponent.Name}";
                ListBox_DamageOutput.Items.Add(attack);
                
            }

            if (player.ClassType == "Paladin")
            {
                Paladin luptator = (Paladin)player;
                int curentHP = player.HP;

                int heal = luptator.HolyLight();
                curentHP += heal;
                Label_HP_Player.Content = curentHP;

                string attack = $"{player.Name} healed {heal} damage";
                ListBox_DamageOutput.Items.Add(attack);
            }           
        }
        #endregion

        #region Functii
        private void Attack(CombatClass fighter1, CombatClass fighter2)
        {
            string rezultat = $"{fighter1.Name} dealt {DamageDone(fighter1, fighter2)} damage to {fighter2.Name}\n";
            ListBox_DamageOutput.Items.Add(rezultat);

            if (CheckWinner(fighter1, fighter2))// daca n-am winner, fac astea
            {
                Button_Special_Skill.IsEnabled = false;
                Button_Strike.IsEnabled = false;
            }
            if (fighter1.HP <= 0 && fighter2.HP <= 0)
            {
                Label_Rezultat.Content = "We have a draw";
            }
            else if (fighter1.HP <= 0)
            {
                Label_Rezultat.Content = $"Winner is {fighter2.Name}";
            }
            else if (fighter2.HP <= 0)
            {
                Label_Rezultat.Content = $"Winner is {fighter1.Name}";
            }            
        }

        public int DamageDone(CombatClass fighter1, CombatClass fighter2)
        {
            int attackValue = fighter1.AttackValue(fighter1.MinDamage, fighter1.MaxDamage) - fighter2.Armor;
            fighter2.HP -= attackValue;
            return attackValue;
        }

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
        #endregion

        #region Timer
        private void LoadDispatcher()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(3);
            timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            var player = (CombatClass)ComboBox_Class_Player.SelectedItem;
            var oponent = (CombatClass)ComboBox_Class_Oponent.SelectedItem;

            timer.Stop();//pun aici a doua chemare a functiei
            Attack(oponent, player);
            // trebuie sa fac variabilele Player si Oponent globale
            Button_Strike.IsEnabled = true;

            if (player.HP < 0)
            {
                Label_HP_Player.Content = 0;
            }
            else
            {
                Label_HP_Player.Content = player.HP;
            }

        }
        #endregion

        public void ComboBoxes(bool x)
        {
            ComboBox_Class_Player.IsEnabled = x;
            ComboBox_Class_Oponent.IsEnabled = x;
            ComboBox_Weapon_Player.IsEnabled = x;
            ComboBox_Weapon_Oponent.IsEnabled = x;
        }
    }
}