using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using TestApp.Themes;

namespace TestApp
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        // Данные о сервере
        public static SqlConnection Connection = new SqlConnection("server=PikaChu-PC\\PIKACHUSQL;Trusted_Connection=yes;database=TestAppDB");

        //Данные о пользователе
        public static int UserId;
        public static string UserFIO;
        public static int PermissionLevel;
        public static int Theme = 1;
        public static List<Theme> ThemeList = new List<Theme>();
        public static Theme GetCurrentTheme()
        {
            return ThemeList[Theme-1];
        }
        public static void GetThemes()
        {
            ThemeList.Clear();
            DataTable result = Get("SELECT * FROM Themes");
            foreach (DataRow i in result.Rows) 
            {
                Theme theme = new Theme() { 
                    id = Convert.ToInt32(i[0]),
                    Name = i[1].ToString(),
                    Background = (Color)ColorConverter.ConvertFromString(i[2].ToString()),
                    Borders = (Color)ColorConverter.ConvertFromString(i[3].ToString()),
                    BackgroundTwo = (Color)ColorConverter.ConvertFromString(i[4].ToString()),
                    Main = (Color)ColorConverter.ConvertFromString(i[5].ToString())
                };
                ThemeList.Add(theme);
            }
        }
        public static void SetTheme()
        {
            Set($"UPDATE [Users] SET Theme = '{ThemeList[Theme-1].id}' WHERE FIO = '{UserFIO}'");
        }
        public static DataTable Get(string Query)
        {
            try
            {
                DataTable dataTable = new DataTable("dataBase");
                Connection.Open();
                SqlCommand sqlCommand = Connection.CreateCommand();
                sqlCommand.CommandText = Query;
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                sqlDataAdapter.Fill(dataTable);
                Connection.Close();
                return dataTable;
            }
            catch (Exception ex)
            {
                Connection.Close();
                MessageBox.Show("Ошибка " + ex.Message);
                return null;
            }
        }
        public static void Set(string Query)
        {
            try
            {
                Connection.Open();
                SqlCommand sqlCommand = Connection.CreateCommand();
                sqlCommand.CommandText = Query;
                sqlCommand.ExecuteNonQuery();
                Connection.Close();
            }
            catch (Exception ex)
            {
                Connection.Close();
                MessageBox.Show("Ошибка " + ex.Message);
            }
        }
    }
}
