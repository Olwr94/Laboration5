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
                emailTextBox.Text = "";
            }
            else
            { 
                userListBox.Items.Add(new User(nameTextBox.Text, emailTextBox.Text));
                nameTextBox.Text = "";
                emailTextBox.Text = "";
            }
        }
        //Change button
        private void btnChange_Click(object sender, RoutedEventArgs e)
        {
            if(userListBox.SelectedIndex != 0)
            {
                btnChange.IsEnabled = true;
            }
        }
        //Remove button
        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            if (userListBox.SelectedIndex >= 0)
            {
                int position = userListBox.SelectedIndex;
                userListBox.Items.RemoveAt(position);
                if (userListBox.Items.Count <= position)
                {
                    userListBox.SelectedIndex = position - 1;
                }
                else
                {
                    userListBox.SelectedIndex = position;
                }
                if (userListBox.Items.Count == 0)
                    btnRemove.IsEnabled = false;
            }
        }
        //User List box
        private void userListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnRemove.IsEnabled = userListBox.SelectedIndex >= 0;
            if((User)userListBox.SelectedItem != null)
            {
                UserInfo.Content = ((User)userListBox.SelectedItem).Name + " " + 
                                   ((User)userListBox.SelectedItem).Email;
            }
        }
        //name text box
        private void nameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {                
            if (string.IsNullOrWhiteSpace(nameTextBox.Text) || userListBox.Items.Contains(nameTextBox.Text))
            {
                btnAdd.IsEnabled = userListBox.SelectedIndex >= 0;
                btnAdd.IsEnabled = false;

                if (userListBox.Items.Contains(nameTextBox.Text))
                    MessageBox.Show("You can not type the same thing!");
                if (string.IsNullOrWhiteSpace(nameTextBox.Text))
                    MessageBox.Show("Fill in both fields");                       
            }
            else if (btnAdd != null)
            {
                btnAdd.IsEnabled = true;
            }       
        }
        private void emailTextBox_TextChanged(object sender, TextChangedEventArgs e)
        { 
             
             if (string.IsNullOrWhiteSpace(emailTextBox.Text) || userListBox.Items.Contains(emailTextBox.Text))
             {
                btnAdd.IsEnabled = false;

                if (userListBox.Items.Contains(emailTextBox.Text))
                    MessageBox.Show("You can not type the same thing!");
                if (string.IsNullOrWhiteSpace(emailTextBox.Text))
                    MessageBox.Show("Fill in both fields");
             }
             else if (btnAdd != null)
             {
                btnAdd.IsEnabled = true;
             }         
        }
    }
}
