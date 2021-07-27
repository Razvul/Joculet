﻿using System;
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
            Warrior Varian = new Warrior(130, 3, 4, 7, "Varian");
            Paladin Arthas = new Paladin(120, 2, 3, 5, "Arthas");

            ComboBox_Class_PLayer.Items.Add(Varian);
            ComboBox_Class_PLayer.Items.Add(Arthas);
            ComboBox_Class_PLayer.SelectedIndex = 0;

            ComboBox_Class_Oponent.Items.Add(Varian);
            ComboBox_Class_Oponent.Items.Add(Arthas);
            ComboBox_Class_Oponent.SelectedIndex = 1;

            Weapons Gorehowl = new Weapons()
            {
                MinDamage = 7,
                MaxDamage = 15,
                WeaponName = "Gorehowl"
            };
            Weapons Warbreaker = new Weapons()
            {
                MinDamage = 5,
                MaxDamage = 11,
                WeaponName = "Warbreaker"
            };
            Weapons LightsVengeance = new Weapons()
            {
                MinDamage = 6,
                MaxDamage = 10,
                WeaponName = "LightsVengeance"
            };

            ComboBox_Weapon_Player.Items.Add(Gorehowl);
            ComboBox_Weapon_Player.Items.Add(Warbreaker);
            ComboBox_Weapon_Player.Items.Add(LightsVengeance);
            ComboBox_Weapon_Player.SelectedIndex = 0;

            ComboBox_Weapon_Oponent.Items.Add(Gorehowl);
            ComboBox_Weapon_Oponent.Items.Add(Warbreaker);
            ComboBox_Weapon_Oponent.Items.Add(LightsVengeance);
            ComboBox_Weapon_Oponent.SelectedIndex = 2;
        }

        public void DamageDone(CombatClass fighter1, CombatClass fighter2)
        {
            int attackValue = fighter1.AttackValue(fighter1.MinDamage, fighter1.MaxDamage) - fighter2.Armor;
            fighter2.HP -= attackValue;
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
            Button_Strike.IsEnabled = true;
            Button_Special_Skill.IsEnabled = true;
        }

        private void Button_Strike_Click(object sender, RoutedEventArgs e)
        {
            //DamageDone(ComboBox_Class_PLayer, ComboBox_Class_Oponent);
            Button_Wait_Oponent.IsEnabled = true;
            Button_Strike.IsEnabled = false;
        }

        private void Button_Wait_Oponent_Click(object sender, RoutedEventArgs e)
        {
            Button_Strike.IsEnabled = true;
            Button_Wait_Oponent.IsEnabled = false;
        }

        private void Button_Special_Skill_Click(object sender, RoutedEventArgs e)
        {
            Button_Special_Skill.IsEnabled = false;
        }
        #endregion
    }
}