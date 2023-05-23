using System;
using System.Collections.Generic;
using System.Data;
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
using TestApp.Themes;

namespace TestApp.Pages
{
    /// <summary>
    /// Логика взаимодействия для ManageUsersPage.xaml
    /// </summary>
    public partial class ManageUsersPage : Page
    {
        List<User> users= new List<User>();
        public ManageUsersPage()
        {
            InitializeComponent();
            LoadTheme();
            LoadUsers();
            UpdateList();

        }
        public void GetPermissionLevels()
        {
            DataTable result = App.Get("SELECT Name FROM [PermissionLevels]");
            PermissionsCombo.Items.Clear();
            foreach (DataRow a in result.Rows)
            {
                PermissionsCombo.Items.Add(a[0].ToString());
            }
        }
        public void UpdateList()
        {
            UsersListBox.Items.Clear();
            foreach (User a in users)
            {
                UsersListBox.Items.Add(a.FIO);
            }
        }
        public void LoadUsers(string Find)
        {
            users.Clear();
            DataTable result =  App.Get($"SELECT * FROM Users WHERE FIO LIKE '%{Find}%'");
            foreach(DataRow a in result.Rows)
            {
                User user = new User()
                {
                    Id = Convert.ToInt32(a[0]),
                    Login = a[1].ToString(),
                    Password = a[2].ToString(),
                    FIO = a[3].ToString(),
                    UserTheme = Convert.ToInt32(a[4]),
                    PermissionLevel = Convert.ToInt32(a[5])
                };
                users.Add(user);
            }
        }
        public void LoadUsers()
        {
            users.Clear();
            DataTable result = App.Get($"SELECT * FROM Users");
            foreach (DataRow a in result.Rows)
            {
                User user = new User()
                {
                    Id = Convert.ToInt32(a[0]),
                    Login = a[1].ToString(),
                    Password = a[2].ToString(),
                    FIO = a[3].ToString(),
                    UserTheme = Convert.ToInt32(a[4]),
                    PermissionLevel = Convert.ToInt32(a[5])
                };
                users.Add(user);
            }
        }
        public void LoadTheme()
        {
            Theme theme = App.GetCurrentTheme();
            MainGrid.Background = new SolidColorBrush(theme.Background);
            foreach (var a in MainGrid.Children)
            {
                if (a is Grid)
                {
                    foreach (var b in ((Grid)a).Children)
                    {
                        if (b is Button)
                        {
                            ((Button)b).Background = new SolidColorBrush(theme.Main);
                            ((Button)b).Foreground = new SolidColorBrush(theme.Background);
                        }
                        if (b is Label)
                        {
                            ((Label)b).Foreground = new SolidColorBrush(theme.Main);
                        }
                        if (b is TextBox)
                        {
                            ((TextBox)b).Background = new SolidColorBrush(theme.BackgroundTwo);
                            ((TextBox)b).Foreground = new SolidColorBrush(theme.Main);
                            ((TextBox)b).BorderBrush = new SolidColorBrush(theme.Borders);
                        }
                        if (b is PasswordBox)
                        {
                            ((PasswordBox)b).Background = new SolidColorBrush(theme.BackgroundTwo);
                            ((PasswordBox)b).Foreground = new SolidColorBrush(theme.Main);
                            ((PasswordBox)b).BorderBrush = new SolidColorBrush(theme.Borders);
                        }
                    }
                }
                if (a is Button)
                {
                    ((Button)a).Background = new SolidColorBrush(theme.Main);
                    ((Button)a).Foreground = new SolidColorBrush(theme.Background);
                }
                if (a is Label)
                {
                    ((Label)a).Foreground = new SolidColorBrush(theme.Main);
                }
                if (a is TextBox)
                {
                    ((TextBox)a).Background = new SolidColorBrush(theme.BackgroundTwo);
                    ((TextBox)a).Foreground = new SolidColorBrush(theme.Main);
                    ((TextBox)a).BorderBrush = new SolidColorBrush(theme.Borders);
                }
                if (a is PasswordBox)
                {
                    ((PasswordBox)a).Background = new SolidColorBrush(theme.BackgroundTwo);
                    ((PasswordBox)a).Foreground = new SolidColorBrush(theme.Main);
                    ((PasswordBox)a).BorderBrush = new SolidColorBrush(theme.Borders);
                }
                if (a is ListBox)
                {
                    ((ListBox)a).Background = new SolidColorBrush(theme.BackgroundTwo);
                    ((ListBox)a).Foreground = new SolidColorBrush(theme.Main);
                    ((ListBox)a).BorderBrush = new SolidColorBrush(theme.Borders);
                }
            }
        }
        private void ThemeButton_Click(object sender, RoutedEventArgs e)
        {
            if (App.Theme < App.ThemeList.Count)
            {
                App.Theme++;
            }
            else
            {
                App.Theme = 1;
            }
            LoadTheme();
            App.SetTheme();
        }

        private void GoBackBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            int ndx = UsersListBox.SelectedIndex;
            App.Set($"UPDATE Users SET PermissionLevel = {PermissionsCombo.SelectedIndex+1} WHERE id = {users[ndx].Id};");
            MessageBox.Show("Успешно!");
        }

        private void UsersListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int ndx = UsersListBox.SelectedIndex;
            if (ndx == -1)
            {
                FIOLabel.Visibility = Visibility.Hidden;
                DeleteUserBtn.Visibility = Visibility.Hidden;
                SaveBtn.Visibility = Visibility.Hidden;
                PermissionsCombo.Visibility = Visibility.Hidden;
                PermissionLevelLbl.Visibility = Visibility.Hidden;
                FIOLabel.Content = "";
                return;
            }
            FIOLabel.Visibility = Visibility.Visible;
            DeleteUserBtn.Visibility = Visibility.Visible;
            SaveBtn.Visibility = Visibility.Visible;
            PermissionsCombo.Visibility = Visibility.Visible;
            PermissionLevelLbl.Visibility = Visibility.Visible;
            GetPermissionLevels();
            PermissionsCombo.SelectedIndex = users[ndx].PermissionLevel-1;
            FIOLabel.Content = users[ndx].FIO;

        }

        private void DeleteUserBtn_Click(object sender, RoutedEventArgs e)
        {
            int ndx = UsersListBox.SelectedIndex;
            App.Set($"DELETE FROM Users WHERE id = {users[ndx].Id};");
            if (UserFinderBox.Text == "")
            {
                LoadUsers();
            }
            else
            {
                LoadUsers(UserFinderBox.Text);
            }
            UpdateList();
        }

        private void UserFinderBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (UserFinderBox.Text == "")
            {
                LoadUsers();
            }
            else
            {
                LoadUsers(UserFinderBox.Text);
            }
            UpdateList();
        }
    }
}
