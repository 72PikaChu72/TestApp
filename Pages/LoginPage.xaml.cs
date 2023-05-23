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
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
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
            }
        }
        public LoginPage()
        {
            InitializeComponent();
            App.GetThemes();
            LoadTheme();
        }
        
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            DataTable result = new DataTable();
            try
            {
                result = App.Get($"Select FIO,PermissionLevel,Theme from Users WHERE login = '{LoginBox.Text}' and password = '{PasswordBox.Password}'");
                App.UserFIO = result.Rows[0][0].ToString();
                App.PermissionLevel = Convert.ToInt16(result.Rows[0][1]);
                for(int i = 0; i < App.ThemeList.Count; i++)
                {
                    if (App.ThemeList[i].id == Convert.ToInt16(result.Rows[0][2]))
                    {
                        App.Theme = i+1; break;
                    }
                }
            }
            catch
            {
                MessageBox.Show("Неправильный логин или пароль!");
                App.UserFIO = null;
                App.PermissionLevel = 0;
                App.Theme = 1;
                return;
            }

            NavigationService.Navigate(new MainPage());
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegisterPage());
        }
    }
}
