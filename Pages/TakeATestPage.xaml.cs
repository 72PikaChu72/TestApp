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
    /// Логика взаимодействия для TakeATestPage.xaml
    /// </summary>
    public partial class TakeATestPage : Page
    {
        List<string[]> PublicTests = new List<string[]>();
        public TakeATestPage()
        {
            InitializeComponent();
            LoadTheme();
            DataTable FoundTests = App.Get("Select Code,Name from Tests where IsPublic = 1");
            foreach (DataRow test in FoundTests.Rows)
            {
                string[] tester = new string[2];
                tester[0] = test[0].ToString();
                tester[1] = test[1].ToString();
                PublicTestsListBox.Items.Add(tester[1]);
                PublicTests.Add(tester);
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
                        if (b is ListBox)
                        {
                            ((ListBox)b).Background = new SolidColorBrush(theme.BackgroundTwo);
                            ((ListBox)b).Foreground = new SolidColorBrush(theme.Main);
                            ((ListBox)b).BorderBrush = new SolidColorBrush(theme.Borders);
                        }
                        if (b is RadioButton)
                        {
                            ((RadioButton)b).Background = new SolidColorBrush(theme.BackgroundTwo);
                            ((RadioButton)b).Foreground = new SolidColorBrush(theme.Main);
                            ((RadioButton)b).BorderBrush = new SolidColorBrush(theme.Borders);
                        }
                        if (b is CheckBox)
                        {
                            ((CheckBox)b).Background = new SolidColorBrush(theme.BackgroundTwo);
                            ((CheckBox)b).Foreground = new SolidColorBrush(theme.Main);
                            ((CheckBox)b).BorderBrush = new SolidColorBrush(theme.Borders);
                        }
                        if (b is ComboBox)
                        {
                            ((ComboBox)b).Background = new SolidColorBrush(theme.BackgroundTwo);
                            ((ComboBox)b).Foreground = new SolidColorBrush(theme.Main);
                            ((ComboBox)b).BorderBrush = new SolidColorBrush(theme.Borders);
                        }
                    }
                }
                if (a is ListBox)
                {
                    ((ListBox)a).Background = new SolidColorBrush(theme.BackgroundTwo);
                    ((ListBox)a).Foreground = new SolidColorBrush(theme.Main);
                    ((ListBox)a).BorderBrush = new SolidColorBrush(theme.Borders);
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

        private void TakeAPublicTest(object sender, RoutedEventArgs e)
        {
            if (PublicTestsListBox.SelectedIndex != -1)
            {
                NavigationService.Navigate(new TestPage(PublicTests[PublicTestsListBox.SelectedIndex][0]));
            }
            
        }

        private void TakeATestByCode_Click(object sender, RoutedEventArgs e)
        {
            if(Convert.ToInt32(App.Get($"Select Count(*) from Tests Where Code = '{TestCodeBox.Text}'").Rows[0][0]) > 0)
            {
                NavigationService.Navigate(new TestPage(TestCodeBox.Text));
            }
        }
    }
}
