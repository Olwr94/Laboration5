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
        public string EmailPattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";

        public MainWindow()
        {
            InitializeComponent();
        }

        //Method to add objects name into generic list and then returns the name
        private List<string> UserNameList(ListBox lb)
        {

            List<string> objectName = new List<string>();
            for (int i = 0; i < lb.Items.Count; i++)
            {
                objectName.Add(((User)lb.Items.GetItemAt(i)).Name);
            }

            return objectName;
        }

        //Method to add objects email into a generic list and then returns the email
        private List<string> UserEmailList(ListBox lb)
        {

            List<string> objectEmail = new List<string>();
            for (int i = 0; i < lb.Items.Count; i++)
            {
                objectEmail.Add(((User)lb.Items.GetItemAt(i)).Email);
            }

            return objectEmail;
        }

        //Add button
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Match match = Regex.Match(emailTextBox.Text, EmailPattern, RegexOptions.IgnoreCase);
            //Checks if the Textboxes is empty or email already exists.
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
            var normalUser = (User)userListBox.SelectedItem;
            var adminUser = (User)adminListBox.SelectedItem;

            if (btnChange.Content.ToString() == "Edit")
            {
                btnChange.Content = "Apply";
                //Puts selected objects name and email into textboxes
                if (userListBox.SelectedIndex >= 0)
                {
                    nameTextBox.Text = normalUser.Name;
                    emailTextBox.Text = normalUser.Email;
                }
                if (adminListBox.SelectedIndex >= 0)
                {
                    nameTextBox.Text = adminUser.Name;
                    emailTextBox.Text = adminUser.Email;
                }
            }
            else if (btnChange.Content.ToString() == "Apply")
            {
              
                btnChange.Content = "Edit";
                normalUser.Name = nameTextBox.Text;
                normalUser.Email = emailTextBox.Text;
                userListBox.Items.Refresh();
                adminListBox.Items.Refresh();
                adminListBox.SelectedIndex = -1;
                userListBox.SelectedIndex = -1;
                nameTextBox.Clear();
                emailTextBox.Clear();
                userInfoLabel.Content = $"Name: {normalUser.Name}\nEmail: {normalUser.Email}";
            }
        
        }

        //Remove object button
        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            //Selection moves down after a removed objects till there are none then it moves up till listbox is empty
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

        //User listbox
        private void userListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnRemove.IsEnabled = userListBox.SelectedIndex >= 0;
            btnMoveUserToAdmin.IsEnabled = userListBox.SelectedIndex >= 0;
            btnChange.IsEnabled = userListBox.SelectedIndex >= 0;

            //Shows userinfo from user listbox in label
            if ((User)userListBox.SelectedItem != null)
                userInfoLabel.Content =
                    $"Name: {((User)userListBox.SelectedItem).Name} \nEmail: {((User)userListBox.SelectedItem).Email}";
            else
                userInfoLabel.Content = string.Empty;
        }

        //Name textbox
        private void nameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //Checks if name already exists in any of the listboxes and sets the add button to false if it does
            if (string.IsNullOrWhiteSpace(nameTextBox.Text) || UserNameList(userListBox).Contains(nameTextBox.Text) ||
                UserNameList(adminListBox).Contains(nameTextBox.Text))
            {
                btnAdd.IsEnabled = false;
                //Gives error text if name exists in either list boxes
                if (UserNameList(userListBox).Contains(nameTextBox.Text) || UserNameList(adminListBox).Contains(nameTextBox.Text))
                    labelNameErrorText.Content = "Name is already taken";
                //Gives error text if Name textbox is empty or whitespace
                if (string.IsNullOrWhiteSpace(nameTextBox.Text))
                    labelNameErrorText.Content = "Type a name";
            }
            else if (btnAdd != null)
            {
                btnAdd.IsEnabled = true;
                labelNameErrorText.Content = "";
            }
        }

        //Email textbox
        private void emailTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //Checks if Email already exists in any of the listboxes and sets the add button to false if it does
            if (string.IsNullOrWhiteSpace(emailTextBox.Text) || UserEmailList(userListBox).Contains(emailTextBox.Text) ||
                UserEmailList(adminListBox).Contains(emailTextBox.Text))
            {
                btnAdd.IsEnabled = false;
                //Gives error text if email exists in either list boxes
                if (UserEmailList(userListBox).Contains(emailTextBox.Text) || UserEmailList(adminListBox).Contains(emailTextBox.Text))
                    labelEmailErrorText.Content = "Email adress is already taken";
                //Gives error text if email textbox is empty or whitespace
                if (string.IsNullOrWhiteSpace(emailTextBox.Text))
                    labelEmailErrorText.Content = "Type an email adress!";
            }
            else if (btnAdd != null)
            {
                btnAdd.IsEnabled = true;
                labelEmailErrorText.Content = "";
            }
        }

        //Move user button
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

        //Move Admin button
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

        //Admin list box
        private void adminListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnRemove.IsEnabled = adminListBox.SelectedIndex >= 0;
            btnChange.IsEnabled = adminListBox.SelectedIndex >= 0;
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

