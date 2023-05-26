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
using TestApp.Classes;
using TestApp.Themes;

namespace TestApp.Pages
{
    /// <summary>
    /// Логика взаимодействия для TestResultsPage.xaml
    /// </summary>
    public partial class TestResultsPage : Page
    {
        List<User> users = new List<User>();
        List<Test> tests = new List<Test>();
        public TestResultsPage()
        {
            InitializeComponent();
            LoadTheme();
            GetFromDb();
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
        private void GoBackBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
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
        public void GetFromDb()
        {
            UsersListBox.Items.Clear();
            if (TypeBtn.Content.ToString() == "Пользователи")
            {
                users.Clear();
                foreach (DataRow a in App.Get($"Select * From Users Where FIO Like '%{UserFinderBox.Text}%'").Rows)
                {
                    User user = new User()
                    {
                        Id = Convert.ToInt32(a[0]),
                        FIO = a[3].ToString()
                    };
                    users.Add(user);
                    UsersListBox.Items.Add(user.FIO);
                }
            }
            else
            {
                tests.Clear();
                foreach (DataRow a in App.Get($"Select * From Tests Where Name Like '%{UserFinderBox.Text}%'").Rows)
                {
                    Test test = new Test()
                    {
                        Id = Convert.ToInt32(a[0]),
                        Name = a[1].ToString(),
                        Code = a[5].ToString()
                    };
                    tests.Add(test);
                    UsersListBox.Items.Add(test.Name);
                }
            }

            
        }

        private void ChangeType_Click(object sender, RoutedEventArgs e)
        {
            if (TypeBtn.Content.ToString() == "Тесты")
            {
                TypeBtn.Content = "Пользователи";
            }
            else
            {
                TypeBtn.Content = "Тесты";
            }
            GetFromDb();
        }

        private void UserFinderBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            GetFromDb();
        }

        private void UsersListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (UsersListBox.SelectedIndex == -1)
            {
                TestInfoGrid.Visibility = Visibility.Hidden;
                UserInfoGrid.Visibility = Visibility.Hidden;
                return;
            }
            if (TypeBtn.Content.ToString() == "Тесты")
            {
                TestInfoGrid.Visibility = Visibility.Visible;
                TestAnswersCount.Content = App.Get($"Select Count(*) From Answers Where TestId = '{tests[UsersListBox.SelectedIndex].Id}'").Rows[0][0].ToString();
                TestAnswersAvg.Content = App.Get($"SELECT ROUND(AVG(Persentage),2) AS average_value FROM Answers Where TestId = '{tests[UsersListBox.SelectedIndex].Id}'").Rows[0][0].ToString()+"%";
                TestCodeLbl.Content = tests[UsersListBox.SelectedIndex].Code;
            }
            else
            {
                UserInfoGrid.Visibility = Visibility.Visible;
                UserAnswersCount.Content = App.Get($"Select Count(*) From Answers Where UserId = '{users[UsersListBox.SelectedIndex].Id}'").Rows[0][0].ToString();
                UserAnswersAvg.Content = App.Get($"SELECT ROUND(AVG(Persentage),2) AS average_value FROM Answers Where UserId = '{users[UsersListBox.SelectedIndex].Id}'").Rows[0][0].ToString() + "%";
            }

        }
    }
}
