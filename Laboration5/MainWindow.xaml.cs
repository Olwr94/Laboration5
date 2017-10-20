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

namespace Laboration5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        //Add button
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(nameTextBox.Text) || string.IsNullOrWhiteSpace(emailTextBox.Text))
            {
                nameTextBox.Text = "";
            }
            else
            {
                userListBox.Items.Add(nameTextBox.Text.Trim());
                nameTextBox.Text = "";
            }
        }
        //Change button
        private void btnChange_Click(object sender, RoutedEventArgs e)
        {

        }
        //Remove button
        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            if (userListBox.SelectedIndex >= 0)
            {
                int position = userListBox.SelectedIndex;
                userListBox.Items.RemoveAt(position);
                if (userListBox.Items.Count <= position)
                    userListBox.SelectedIndex = position - 1;
                else
                    userListBox.SelectedIndex = position;

                if (userListBox.Items.Count == 0)
                    btnRemove.IsEnabled = false;
            }
        }
        //User List box
        private void userListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnRemove.IsEnabled = userListBox.SelectedIndex >= 0;
        }
        //name text box
        private void nameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(nameTextBox.Text) || userListBox.Items.Contains(nameTextBox.Text))
            {
                btnAdd.IsEnabled = false;

                if (userListBox.Items.Contains(nameTextBox.Text))
                    labelErrorText.Content = "You can not type the same thing!";
                if (string.IsNullOrWhiteSpace(nameTextBox.Text))
                    labelErrorText.Content = "You need to type something";
            }
            else if(btnAdd!=null)
            {
                btnAdd.IsEnabled = true;
                labelErrorText.Content = "";
            }
        }
    }
}
