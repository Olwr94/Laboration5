using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        public string emailPattern = @"(\w|\D)+[@](\w|\D)+\.(\w|\D)+";
        public MainWindow()
        {
            InitializeComponent();
        }   
        //Add button
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Match match = Regex.Match(emailTextBox.Text, emailPattern);

            if (string.IsNullOrWhiteSpace(nameTextBox.Text) || string.IsNullOrWhiteSpace(emailTextBox.Text) || !match.Success)
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
            if ((User)userListBox.SelectedItem != null)
                userInfoLabel.Content = "Name: " + ((User)userListBox.SelectedItem).Name + "\n" + "Email: " +
                                        ((User)userListBox.SelectedItem).Email;
            else
                userInfoLabel.Content = string.Empty;

        }
        //name text box
        private void nameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

            if (string.IsNullOrWhiteSpace(nameTextBox.Text) || userListBox.Items.Contains(nameTextBox.Text))
            {
                btnAdd.IsEnabled = userListBox.SelectedIndex >= 0;
                btnAdd.IsEnabled = false;

                if (userListBox.Items.Contains(nameTextBox.Text))
                    //MessageBox.Show("Can not contain same text as before");
                    labelNameErrorText.Content = "You can not type the same thing!";
                if (string.IsNullOrWhiteSpace(nameTextBox.Text))
                    //MessageBox.Show("Must type a name");
                    labelNameErrorText.Content = "Must type a name";
            }
            else if (btnAdd != null)
            {
                btnAdd.IsEnabled = true;
                labelNameErrorText.Content = "";
            }

        }

        private void emailTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
          

            if (string.IsNullOrWhiteSpace(emailTextBox.Text) || userListBox.Items.Contains(emailTextBox.Text))
            {

                btnAdd.IsEnabled = false;

                if (userListBox.Items.Contains(emailTextBox.Text))
                    //MessageBox.Show("Can not contain same text as before");
                    labelEmailErrorText.Content = "You can not type the same thing!";
                if (string.IsNullOrWhiteSpace(emailTextBox.Text))
                   // MessageBox.Show("Must type an email adress!");
                labelEmailErrorText.Content = "Must type email adress!";
            }
            else if (btnAdd != null)
            {
                btnAdd.IsEnabled = true;
                labelEmailErrorText.Content = "";
            }


        }
    }
}
