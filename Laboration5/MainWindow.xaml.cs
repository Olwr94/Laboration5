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

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(nameTextBox.Text) && string.IsNullOrWhiteSpace(emailTextBox.Text))
            {
                nameTextBox.Text = "";
                emailTextBox.Text = "";
            }

            else
            {
                userListBox.Items.Add(nameTextBox.Text.Trim());
                userListBox.Items.Add(emailTextBox.Text.Trim());
                nameTextBox.Text = "";
                emailTextBox.Text = "";
            }

        }

        private void btnChange_Click(object sender, RoutedEventArgs e)
        {

        }
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

        private void userListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnRemove.IsEnabled = userListBox.SelectedIndex >= 0;
        }

        private void nameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (nameTextBox.Text == string.Empty)
            {
                if (string.IsNullOrWhiteSpace(nameTextBox.Text) || userListBox.Items.Contains(nameTextBox.Text))
                {
                    btnAdd.IsEnabled = userListBox.SelectedIndex >= 0;
                    btnAdd.IsEnabled = false;

                    if (userListBox.Items.Contains(nameTextBox.Text))
                        labelErrorText.Content = "You can not type the same thing!";
                    if (string.IsNullOrWhiteSpace(nameTextBox.Text))
                        labelErrorText.Content = "You need to type something";
                }
                else if (btnAdd != null)
                {
                    btnAdd.IsEnabled = true;
                    labelErrorText.Content = "";
                }
            }
        }

        private void emailTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (emailTextBox.Text == string.Empty)
            {
                if (string.IsNullOrWhiteSpace(emailTextBox.Text) || userListBox.Items.Contains(emailTextBox.Text))
                {

                    btnAdd.IsEnabled = false;

                    if (userListBox.Items.Contains(emailTextBox.Text))
                        labelErrorText.Content = "You can not type the same thing!";
                    if (string.IsNullOrWhiteSpace(emailTextBox.Text))
                        labelErrorText.Content = "You need to type something";
                }
                else if (btnAdd != null)
                {
                    btnAdd.IsEnabled = true;
                    labelErrorText.Content = "";
                }
            }

        }
    }
}
