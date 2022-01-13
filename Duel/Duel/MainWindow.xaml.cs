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
        private int strikeCounter;

        private readonly ListOfObjects _listaLuptatori = ListOfObjects.GetLuptatori();
        private readonly ListOfObjects _listaArme = ListOfObjects.GetArme();

        public MainWindow()
        {
            InitializeComponent();
            Populate();
            LoadDispatcher();
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

            #region Weapon Equipment
            classPlayer.MinDamage += weaponPlayer.MinDamage;
            classPlayer.MaxDamage += weaponPlayer.MaxDamage;

            classOponent.MinDamage += weaponOponent.MinDamage;
            classOponent.MaxDamage += weaponOponent.MaxDamage;
            #endregion

            #region PLayer status
            Label_Player.Content = classPlayer.Name;
            Label_HP_Player.Content = "Health: " + classPlayer.HP;
            Label_Player_Damage.Content = "Damage: " + classPlayer.MinDamage + "-" + classPlayer.MaxDamage;
            Label_Player_Armor.Content = "Armor: " + classPlayer.Armor;
            #endregion

            #region Oponent status
            Label_Oponent.Content = classOponent.Name;
            Label_HP_Oponent.Content = "Health: " + classOponent.HP;
            Label_Oponent_Damage.Content = "Damage: " + classOponent.MinDamage + "-" + classOponent.MaxDamage;
            Label_Oponent_Armor.Content = "Armor: " + classOponent.Armor;
            #endregion

            Button_StartFight.IsEnabled = false;
            Button_Strike.IsEnabled = true;
        }

        private /*async*/ void Button_Strike_Click(object sender, RoutedEventArgs e)
        {
            var player = (CombatClass)ComboBox_Class_Player.SelectedItem;
            var oponent = (CombatClass)ComboBox_Class_Oponent.SelectedItem;

            ListViewItem itemNou = new ListViewItem
            {
                Content = "Health: " + Attack(player, oponent),
                Foreground = Brushes.Blue
            };
            ListView_DamageOutput.Items.Add(itemNou);

            Button_Strike.IsEnabled = false;

            if (oponent.HP < 0)
            {
                Label_HP_Oponent.Content = "Dead";
            }
            else
            {
                Label_HP_Oponent.Content = "Health: " + oponent.HP;
            }

            //await Task.Delay(10000);
            timer.Start();

            strikeCounter++;

            if (strikeCounter % 3 == 0)
            {
                Button_Special_Skill.IsEnabled = true;
            }
        }

        private void Button_Special_Skill_Click(object sender, RoutedEventArgs e)
        {
            var player = (CombatClass)ComboBox_Class_Player.SelectedItem;
            var oponent = (CombatClass)ComboBox_Class_Oponent.SelectedItem;

            if (player.ClassType == "Warrior")
            {
                Warrior razboinic = new Warrior(player.HP, player.MinDamage, player.MaxDamage, player.Armor, player.Name, player.ClassType);

                int critDmg = razboinic.MortalStrike();
                oponent.HP -= critDmg;
                Label_HP_Oponent.Content = "Health: " + oponent.HP;

                string attack = $"{razboinic.Name} dealt {critDmg} critical damage to {oponent.Name}";
                ListView_DamageOutput.Items.Add(attack);
            }

            if (player.ClassType == "Paladin")
            {
                Paladin luptator = new Paladin(player.HP, player.MinDamage, player.MaxDamage, player.Armor, player.Name, player.ClassType);

                int heal = luptator.HolyLight();
                player.HP += heal;
                Label_HP_Player.Content = "Health: " + player.HP;

                string attack = $"{player.Name} healed {heal} damage";
                ListView_DamageOutput.Items.Add(attack);
            }

            Button_Special_Skill.IsEnabled = false;
        }
        #endregion

        #region Functii
        private string Attack(CombatClass fighter1, CombatClass fighter2)
        {
            string rezultat = $"{fighter1.Name} dealt {DamageDone(fighter1, fighter2)} damage to {fighter2.Name}";

            if (CheckWinner(fighter1, fighter2))// daca n-am winner, fac astea
            {
                Button_Special_Skill.IsEnabled = false;
                Button_Strike.IsEnabled = false;
            }
            return rezultat;
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
                weHaveWinner = true;
            }
            return weHaveWinner;
        }
        #endregion

        #region Timer
        private void LoadDispatcher()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            var player = (CombatClass)ComboBox_Class_Player.SelectedItem;
            var oponent = (CombatClass)ComboBox_Class_Oponent.SelectedItem;

            ListViewItem itemNou = new ListViewItem
            {
                Content = "Health: " + Attack(oponent, player),
                Foreground = Brushes.Red
            };
            ListView_DamageOutput.Items.Add(itemNou);

            if (player.HP <= 0)
            {
                Label_HP_Player.Content = "Dead";
            }
            else
            {
                Label_HP_Player.Content = "Health: " + player.HP;
            }

            Button_Strike.IsEnabled = true;

            if (player.HP <= 0 && oponent.HP <= 0)
            {
                Label_Rezultat.Content = "We have a draw";
                Button_Strike.IsEnabled = false;
            }
            else if (player.HP <= 0)
            {
                Label_Rezultat.Content = $"Winner is {oponent.Name}";
                Button_Strike.IsEnabled = false;
            }
            else if (oponent.HP <= 0)
            {
                Label_Rezultat.Content = $"Winner is {player.Name}";
                Button_Strike.IsEnabled = false;
            }
            timer.Stop();
        }
        #endregion

        public void ComboBoxes(bool x)
        {
            ComboBox_Class_Player.IsEnabled = x;
            ComboBox_Class_Oponent.IsEnabled = x;
            ComboBox_Weapon_Player.IsEnabled = x;
            ComboBox_Weapon_Oponent.IsEnabled = x;
        }

        private void Populate()
        {
            PopulateClasses();
            PopulateWeapons();
        }

        private void PopulateClasses()
        {
            var classList = _listaLuptatori.GetClasses();
            foreach (var luptator in classList.OrderBy(x=>x.Name))
            {
                ComboBox_Class_Player.Items.Add(luptator);
                ComboBox_Class_Oponent.Items.Add(luptator);
            }
            ComboBox_Class_Player.SelectedIndex = 0;
            ComboBox_Class_Oponent.SelectedIndex = 1;
        }

        private void PopulateWeapons()
        {
            var weaponList = _listaArme.GetWeapons();
            foreach (var arma in weaponList.OrderBy(x=>x.WeaponName))
            {
                ComboBox_Weapon_Player.Items.Add(arma);
                ComboBox_Weapon_Oponent.Items.Add(arma);
            }
            ComboBox_Weapon_Player.SelectedIndex = 0;
            ComboBox_Weapon_Oponent.SelectedIndex = 2;
        }
    }
}