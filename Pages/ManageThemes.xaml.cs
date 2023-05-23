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
using TestApp.Themes;

namespace TestApp.Pages
{
    /// <summary>
    /// Логика взаимодействия для ManageThemes.xaml
    /// </summary>
    public partial class ManageThemes : Page
    {
        Theme TempTheme = new Theme();
        public ManageThemes()
        {
            InitializeComponent();
            TempTheme = App.GetCurrentTheme();
            LoadTheme();
            UpdateThemesListBox();
        }
        public void LoadTheme()
        {
            Theme theme = TempTheme;
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
        public void UpdateThemesListBox()
        {
            ThemesListBox.Items.Clear();
            foreach(Theme a in App.ThemeList)
            {
                ThemesListBox.Items.Add(a.Name);
            }
        }

        private void ThemesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ThemesListBox.SelectedIndex == -1)
            {
                return;
            }
            string ndx = ThemesListBox.SelectedValue.ToString();
            for(int i = 0; i<App.ThemeList.Count; i++)
            {
                if(App.ThemeList[i].Name == ndx)
                {
                    TempTheme = App.ThemeList[i];
                }
            }
            LoadTheme();
            TempThemeName.Text = ThemesListBox.SelectedValue.ToString();
        }

        private void ThemeFinderBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ThemesListBox.Items.Clear();
            foreach (Theme a in App.ThemeList.Where(f => f.Name.Contains(ThemesFinderBox.Text)))
            {
                ThemesListBox.Items.Add(a.Name);
            }
            
        }

        private void GoBackBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void CreateTheme_Click(object sender, RoutedEventArgs e)
        {
            App.Set($"INSERT INTO Themes (Name, Background,Borders, BackgroundTwo, Main) VALUES ('Новая тема', '{$"#{TempTheme.Background.R:X2}{TempTheme.Background.G:X2}{TempTheme.Background.B:X2}"}', '{$"#{TempTheme.Borders.R:X2}{TempTheme.Borders.G:X2}{TempTheme.Borders.B:X2}"}','{$"#{TempTheme.BackgroundTwo.R:X2}{TempTheme.BackgroundTwo.G:X2}{TempTheme.BackgroundTwo.B:X2}"}','{$"#{TempTheme.Main.R:X2}{TempTheme.Main.G:X2}{TempTheme.Main.B:X2}"}');");
            App.GetThemes();
            UpdateThemesListBox();
        }

        private void SaveThemeBtn_Click(object sender, RoutedEventArgs e)
        {
            App.Set($"Update Themes SET Name='{TempThemeName.Text}', Background = '{$"#{TempTheme.Background.R:X2}{TempTheme.Background.G:X2}{TempTheme.Background.B:X2}"}', Borders = '{$"#{TempTheme.Borders.R:X2}{TempTheme.Borders.G:X2}{TempTheme.Borders.B:X2}"}', BackgroundTwo = '{$"#{TempTheme.BackgroundTwo.R:X2}{TempTheme.BackgroundTwo.G:X2}{TempTheme.BackgroundTwo.B:X2}"}', Main = '{$"#{TempTheme.Main.R:X2}{TempTheme.Main.G:X2}{TempTheme.Main.B:X2}"}' WHERE id = '{TempTheme.id}'");
            App.GetThemes();
            UpdateThemesListBox();
            App.Theme = ThemesListBox.SelectedIndex + 1;
        }

        private void DeleteTheme_Click(object sender, RoutedEventArgs e)
        {
            App.Set($"UPDATE Users SET Theme = NULL WHERE Theme = '{TempTheme.id}'");
            App.Set($"DELETE FROM Themes WHERE id = '{TempTheme.id}'");
            App.GetThemes();
            App.Theme = 1;
            TempTheme = App.GetCurrentTheme();
            UpdateThemesListBox();
        }

        private void BackgroundCP_ColorChanged(object sender, RoutedEventArgs e)
        {
            TempTheme.Background = Color.FromRgb((byte)BackgroundCP.Color.RGB_R, (byte)BackgroundCP.Color.RGB_G, (byte)BackgroundCP.Color.RGB_B);
            LoadTheme();
        }

        private void BordersCP_ColorChanged(object sender, RoutedEventArgs e)
        {
            TempTheme.Borders = Color.FromRgb((byte)BordersCP.Color.RGB_R, (byte)BordersCP.Color.RGB_G, (byte)BordersCP.Color.RGB_B);
            LoadTheme();
        }

        private void BackgroundTwoCP_ColorChanged(object sender, RoutedEventArgs e)
        {
            TempTheme.BackgroundTwo = Color.FromRgb((byte)BackgroundTwoCP.Color.RGB_R, (byte)BackgroundTwoCP.Color.RGB_G, (byte)BackgroundTwoCP.Color.RGB_B);
            LoadTheme();
        }

        private void MainCP_ColorChanged(object sender, RoutedEventArgs e)
        {
            TempTheme.Main = Color.FromRgb((byte)MainCP.Color.RGB_R, (byte)MainCP.Color.RGB_G, (byte)MainCP.Color.RGB_B);
            LoadTheme();
        }
    }
}
