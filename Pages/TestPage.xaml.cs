using Newtonsoft.Json;
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
using TestApp.Classes;
using TestApp.Themes;

namespace TestApp.Pages
{
    /// <summary>
    /// Логика взаимодействия для TestPage.xaml
    /// </summary>
    public partial class TestPage : Page
    {
        Test CurrentTest = new Test();
        Question CurrentQuestion = new Question();
        List<Answer> Answers = new List<Answer>();
        public void GetATestFromBase(string Code)
        {
            string json = App.Get($"Select json from Tests Where Code = '{Code}'").Rows[0][0].ToString();
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto,
                Converters = new List<JsonConverter> { new QuestionConverter() }
            };
            CurrentTest = JsonConvert.DeserializeObject<Test>(json, settings);
            CurrentTest.Id = Convert.ToInt32(App.Get($"Select Id from Tests Where Code = '{Code}'").Rows[0][0]);
        }
        int i = 0;
        public void NextQuestion()
        {
            if(i < CurrentTest.Questions.Count)
            {
                CurrentQuestion = CurrentTest.Questions[i];
                QuestionTextLbl.Content = CurrentQuestion.QuestionText;
                QuestionStackPanel.Children.Clear();
                if (CurrentQuestion is TextQuestion)
                {
                    QuestionStackPanel.Children.Add(new TextBox { Width = 500, Height = 40 });
                }
                else if (CurrentQuestion is RadialQuestion)
                {
                    foreach (var Answer in ((RadialQuestion)CurrentQuestion).Answers)
                    {
                        QuestionStackPanel.Children.Add(new RadioButton {Content = Answer, Foreground = new SolidColorBrush(App.GetCurrentTheme().Main)});
                    }
                }
                else if (CurrentQuestion is CheckBoxQuestion)
                {
                    foreach (var Answer in ((CheckBoxQuestion)CurrentQuestion).Answers)
                    {
                        QuestionStackPanel.Children.Add(new CheckBox { Content = Answer, Foreground = new SolidColorBrush(App.GetCurrentTheme().Main) });
                    }
                }
                i++;
                return;
            }
            int CorrectAnswersCount = 0;
            int TotalAnswersCount = 0;
            string AnswerString = "";
            for(int i = 0;i<CurrentTest.Questions.Count; i++)
            {
                if (CurrentTest.Questions[i] is TextQuestion)
                {
                    AnswerString += Answers[i].TextAnswer + "@";
                }
                if(CurrentTest.Questions[i] is RadialQuestion)
                {
                    TotalAnswersCount++;
                    if (((RadialQuestion)CurrentTest.Questions[i]).CorrectAnswerIndex == Answers[i].answers[0])
                    {
                        CorrectAnswersCount++;
                    }
                    AnswerString += ((RadialQuestion)CurrentTest.Questions[i]).Answers[Answers[i].answers[0]]+"@";
                }
                if (CurrentTest.Questions[i] is CheckBoxQuestion)
                {
                    foreach(int Answer in Answers[i].answers)
                    {
                        if (((CheckBoxQuestion)CurrentTest.Questions[i]).CorrectAnswersIndex.Contains(Answer)){
                            CorrectAnswersCount++;
                        }
                        AnswerString += ((CheckBoxQuestion)CurrentTest.Questions[i]).Answers[Answer]+",";
                    }
                    foreach (int x in ((CheckBoxQuestion)CurrentTest.Questions[i]).CorrectAnswersIndex)
                    {
                        TotalAnswersCount++;
                    }
                    AnswerString = AnswerString.Remove(AnswerString.Length - 1, 1);
                    AnswerString += "@";
                }
            }
            double percentage = (double)CorrectAnswersCount / TotalAnswersCount * 100;
            MessageBox.Show($"Тест окончен!\nПравильных ответов - {CorrectAnswersCount} из {TotalAnswersCount}\n{Math.Round(percentage,2)}%");
            App.Set($"INSERT INTO Answers (TestId, UserId, Persentage, CorrectAnswers, Date, Answers) VALUES ('{CurrentTest.Id}', '{App.UserId}', '{Math.Round(percentage, 2).ToString().Replace(',','.')}', '{CorrectAnswersCount}/{TotalAnswersCount}', '{DateTime.Now}','{AnswerString}')");
            NavigationService.Navigate(new MainPage());
        }
        public TestPage(string Code)
        {
            InitializeComponent();
            LoadTheme();
            GetATestFromBase(Code);
            TestNameLbl.Content = CurrentTest.Name;
            TestDescriptionLbl.Content = CurrentTest.Description;
            StartGrid.Visibility = Visibility.Visible;
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
        private void StartATestBtn_Click(object sender, RoutedEventArgs e)
        {
            StartGrid.Visibility = Visibility.Hidden;
            QuestionGrid.Visibility = Visibility.Visible;
            NextQuestion();

        }

        private void GoBackBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
        int PressCount = 0;
        private void NextQuestionBtn_Click(object sender, RoutedEventArgs e)
        {
            PressCount++;
            Answer answer = new Answer();
            answer.Question = CurrentQuestion;
            answer.answers = new List<int>();
            if (CurrentQuestion is TextQuestion)
            {
                answer.TextAnswer = ((TextBox)QuestionStackPanel.Children[0]).Text;
            }
            else if(CurrentQuestion is RadialQuestion)
            {

                foreach (RadioButton a in QuestionStackPanel.Children)
                {
                    if ((bool)a.IsChecked)
                    {
                        answer.answers.Add(QuestionStackPanel.Children.IndexOf(a));
                        break;
                    }
                }
            }
            else if (CurrentQuestion is CheckBoxQuestion)
            {
                answer.answers = new List<int>();
                foreach (CheckBox a in QuestionStackPanel.Children)
                {
                    if ((bool)a.IsChecked)
                    {
                        answer.answers.Add(QuestionStackPanel.Children.IndexOf(a));
                    }
                }
            }
            if (CurrentTest.ShowAnswers && PressCount<2)
            {
                if (CurrentQuestion is RadialQuestion)
                {

                    answer.answers = new List<int>();
                    foreach (RadioButton a in QuestionStackPanel.Children)
                    {
                        if ((bool)a.IsChecked)
                        {
                            a.Foreground = new SolidColorBrush(Colors.Red);
                        }
                        if (QuestionStackPanel.Children.IndexOf(a) == ((RadialQuestion)CurrentQuestion).CorrectAnswerIndex)
                        {
                            a.Foreground = new SolidColorBrush(Colors.Green);
                        }
                    }
                }
                else if (CurrentQuestion is CheckBoxQuestion)
                {
                    answer.answers = new List<int>();
                    foreach (CheckBox a in QuestionStackPanel.Children)
                    {
                        if ((bool)a.IsChecked)
                        {
                            answer.answers.Add(QuestionStackPanel.Children.IndexOf(a));
                            a.Foreground = new SolidColorBrush(Colors.Red);
                        }
                        if (((CheckBoxQuestion)CurrentQuestion).CorrectAnswersIndex.Contains(QuestionStackPanel.Children.IndexOf(a)))
                        {
                            a.Foreground = new SolidColorBrush(Colors.Green);
                        }
                    }
                }
                if (!(CurrentQuestion is TextQuestion))
                {
                    return;
                }
            }

            Answers.Add(answer);
            PressCount = 0;
            NextQuestion();
            
        }
    }
}
