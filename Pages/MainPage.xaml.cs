using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Activation;
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
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public void CheckPermission()
        {
            int Level = App.PermissionLevel;
            if (Level < 3)
            {
                ManageUsersBtn.Visibility = Visibility.Hidden;
                ManageThemesBtn.Visibility = Visibility.Hidden;
            }
            if (Level < 2)
            {
                CreateTestBtn.Visibility = Visibility.Hidden;
                TestResultsBtn.Visibility = Visibility.Hidden;
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
            }
        }
        public MainPage()
        {
            InitializeComponent();
            FIOLabel.Content = "Добро пожаловать, " + " " + App.UserFIO;
            LoadTheme();
            CheckPermission();
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

        private void ExitBtnClick(object sender, RoutedEventArgs e)
        {
            App.UserFIO = null;
            App.PermissionLevel = 0;
            NavigationService.Navigate(new LoginPage());
        }

        private void TakeATestBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new TakeATestPage());
        }

        private void ManageUsersBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ManageUsersPage());
        }

        private void ManageThemesBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ManageThemes());
        }

        private void CreateTestBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new TestCreation());
        }

        private void MyResultsBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MyResultsPage());
        }
    }
}
