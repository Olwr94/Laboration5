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
using System.Xml.Xsl;

namespace Laboration5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string EmailPattern = @"(\w|\D)+[@](\w|\D)+\.(\w|\D)+";

        public MainWindow()
        {
            InitializeComponent();
        }

        //Method to add objects name into generic list and then returns the names.
        private List<string> UserNameList(ListBox lb)
        {

            List<string> objectName = new List<string>();
            for (int i = 0; i < lb.Items.Count; i++)
            {
                objectName.Add(((User)lb.Items.GetItemAt(i)).Name);
            }

            return objectName;
        }

        private List<string> UserEmailList(ListBox lb)
        {

            List<string> objectName = new List<string>();
            for (int i = 0; i < lb.Items.Count; i++)
            {
                objectName.Add(((User)lb.Items.GetItemAt(i)).Email);
            }

            return objectName;
        }

        //Add button
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Match match = Regex.Match(emailTextBox.Text, EmailPattern);

            if ((string.IsNullOrWhiteSpace(nameTextBox.Text) || string.IsNullOrWhiteSpace(emailTextBox.Text)) ||
                !match.Success)
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
            if (userListBox.SelectedIndex >= 0)
            {
                nameTextBox.Text = ((User)userListBox.SelectedItem).Name;
                emailTextBox.Text = ((User)userListBox.SelectedItem).Email;
            }
            if (adminListBox.SelectedIndex >= 0)
            {
                nameTextBox.Text = ((User)adminListBox.SelectedItem).Name;
                emailTextBox.Text = ((User)adminListBox.SelectedItem).Email;
            }
            if (userListBox.SelectedIndex != 0)
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
            btnMoveUserToAdmin.IsEnabled = userListBox.SelectedIndex >= 0;

            //Shows userinfo from user listbox in label
            if ((User)userListBox.SelectedItem != null)
                userInfoLabel.Content =
                    $"Name: {((User)userListBox.SelectedItem).Name} \nEmail: {((User)userListBox.SelectedItem).Email}";
            else
                userInfoLabel.Content = string.Empty;


        }

        private void nameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //Checks if name already exists in any of the listboxes and set the add button to false if it does
            if (string.IsNullOrWhiteSpace(nameTextBox.Text) || UserNameList(userListBox).Contains(nameTextBox.Text) ||
                UserNameList(adminListBox).Contains(nameTextBox.Text))
            {
                btnAdd.IsEnabled = false;

                if (UserNameList(userListBox).Contains(nameTextBox.Text) || UserNameList(adminListBox).Contains(nameTextBox.Text))
                    labelNameErrorText.Content = "Name is already taken";
                if (string.IsNullOrWhiteSpace(nameTextBox.Text))
                    labelNameErrorText.Content = "Type a name";
            }
            else if (btnAdd != null)
            {
                btnAdd.IsEnabled = true;
                labelNameErrorText.Content = "";
            }
        }

        private void emailTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //Checks if Email already exists in any of the listboxes and set the add button to false if it does
            if (string.IsNullOrWhiteSpace(emailTextBox.Text) || UserEmailList(userListBox).Contains(emailTextBox.Text) ||
                UserEmailList(adminListBox).Contains(emailTextBox.Text))
            {
                btnAdd.IsEnabled = false;

                if (UserEmailList(userListBox).Contains(emailTextBox.Text) || UserEmailList(adminListBox).Contains(emailTextBox.Text))
                    labelEmailErrorText.Content = "Email adress is already taken";
                if (string.IsNullOrWhiteSpace(emailTextBox.Text))
                    labelEmailErrorText.Content = "Type an email adress!";
            }
            else if (btnAdd != null)
            {
                btnAdd.IsEnabled = true;
                labelEmailErrorText.Content = "";
            }
        }

        private void btnMoveUserToAdmin_Click(object sender, RoutedEventArgs e)
        {
            //Moves selected user objects from user listbox to admin listbox
            if (userListBox.SelectedIndex != -1)
            {
                adminListBox.Items.Add(userListBox.SelectedValue);
                userListBox.Items.Remove(userListBox.SelectedValue);
            }
            if (userListBox.Items.Count == 0)
                btnMoveUserToAdmin.IsEnabled = false;
        }


        private void btnMoveAdminToUser_Click(object sender, RoutedEventArgs e)
        {
            //Moves selected user objects from admin listbox to user listbox
            if (adminListBox.SelectedIndex != -1)
            {
                userListBox.Items.Add(adminListBox.SelectedValue);
                adminListBox.Items.Remove(adminListBox.SelectedValue);
            }
            if (adminListBox.Items.Count == 0)
                btnMoveAdminToUser.IsEnabled = false;
        }

        private void adminListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnMoveAdminToUser.IsEnabled = adminListBox.SelectedIndex >= 0;
            //Shows admin info from admin listbox
            if ((User)adminListBox.SelectedItem != null)
                userInfoLabel.Content =
                    $"Name: {((User)adminListBox.SelectedItem).Name} \nEmail: {((User)adminListBox.SelectedItem).Email}";
            else
                userInfoLabel.Content = string.Empty;
        }
    }
}
