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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string AccusationSuspect_String = "";
        string AccusationWeapon_String = "";
        string AccusationRoom_String = "";

        string SuggestionSuspect_String = "";
        string SuggestionWeapon_String = "";
        string SuggestionRoom_String = "";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void SecretPassageNW_Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"This is the NorthWest Secret Passage Button");
        }

        private void SecretPassageSW_Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"This is the SouthWest Secret Passage Button");
        }

        private void SecretPassageNE_Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"This is the NorthEast Secret Passage Button");
        }

        private void SecretPassageSE_Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"This is the SouthEast Secret Passage Button");
        }

        private void AccuseSuspect_Dropdown_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AccusationSuspect_String = (string)((ComboBoxItem)((ComboBox)sender).SelectedValue).Content;
        }

        private void AccuseWeapon_Dropdown_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AccusationWeapon_String = (string)((ComboBoxItem)((ComboBox)sender).SelectedValue).Content;
        }

        private void AccuseRoom_Dropdown_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AccusationRoom_String = (string)((ComboBoxItem)((ComboBox)sender).SelectedValue).Content;
        }

        private void Accuse_Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"You have accused " + AccusationSuspect_String + " of killing the victim using the " + AccusationWeapon_String + " in the " + AccusationRoom_String);
        }

        private void MoveLeft_Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"You are requesting to Move Left");
        }

        private void MoveUp_Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"You are requesting to Move Up");
        }

        private void MoveDown_Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"You are requesting to Move Down");
        }

        private void MoveRight_Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"You are requesting to Move Right");
        }

        private void SuggestSuspect_Dropdown_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SuggestionSuspect_String = (string)((ComboBoxItem)((ComboBox)sender).SelectedValue).Content;
        }

        private void SuggestWeapon_Dropdown_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SuggestionWeapon_String = (string)((ComboBoxItem)((ComboBox)sender).SelectedValue).Content;
        }

        private void SuggestRoom_Dropdown_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SuggestionRoom_String = (string)((ComboBoxItem)((ComboBox)sender).SelectedValue).Content;
        }

        private void Suggest_Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"You have suggested that " + SuggestionSuspect_String + " killed the victim using the " + SuggestionWeapon_String + " in the " + SuggestionRoom_String);
        }
    }
}
