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
using TestApp.Themes;

namespace TestApp.Pages
{
    /// <summary>
    /// Логика взаимодействия для RegisterPage.xaml
    /// </summary>
    public partial class RegisterPage : Page
    {
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
        public RegisterPage()
        {
            InitializeComponent();
            LoadTheme();
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
        }
        
        private void GoBackBtn(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void RegisterBtn(object sender, RoutedEventArgs e)
        {
            Theme theme = App.GetCurrentTheme();
            string FIO = "";
            string Login = LoginBox.Text;
            string Password = PasswordBox.Text;
            //#FFABADB3
            SolidColorBrush DefaultColor = new SolidColorBrush(theme.Borders);
            SolidColorBrush RedColor = new SolidColorBrush(Color.FromRgb(255, 0, 0));
            FIOBox.BorderBrush = DefaultColor;
            LoginBox.BorderBrush = DefaultColor;
            PasswordBox.BorderBrush = DefaultColor;

            Regex RFIO = new Regex(@"^[\p{L}']+ [\p{L}']+ [\p{L}']+$");
            Regex RPassword = new Regex(@"^.{8,}$");
            if (RFIO.IsMatch(FIOBox.Text))
            {
                FIO = FIOBox.Text;
            }
            else
            {
                FIOBox.BorderBrush = RedColor;
                MessageBox.Show("Проверьте правильность ФИО");
                return;
            }
            if (RPassword.IsMatch(PasswordBox.Text))
            {
                Password = PasswordBox.Text;
            }
            else
            {
                PasswordBox.BorderBrush = RedColor;
                MessageBox.Show("Пароль должен содержать более 8 символов");
                return;
            }
            if(App.Get($"SELECT COUNT(*) FROM Users WHERE Login = '{Login}'").Rows[0][0].ToString() != "0")
            {
                LoginBox.BorderBrush = RedColor;
                MessageBox.Show("Пользователь с таким Логином уже зарегестрирован!");
                return;
            }
            try
            {
                App.Set($"Insert Into [Users](FIO,Login,Password,Theme,PermissionLevel)Values('{FIO}','{Login}','{Password}','1','1')");
            }
            catch { return; }
            MessageBox.Show("Успешно зарегистрирован!");
            App.UserFIO = FIOBox.Text;
            NavigationService.Navigate(new MainPage());
        }
    }
}
