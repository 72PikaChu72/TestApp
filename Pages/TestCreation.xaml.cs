using Newtonsoft.Json;
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
    /// Логика взаимодействия для TestCreation.xaml
    /// </summary>
    public partial class TestCreation : Page
    {
        Test NewTest = new Test();
        public Question CurrentQuestion;
        public TestCreation()
        {
            InitializeComponent();
            LoadTheme();
            TestGrid.Visibility = Visibility.Visible;
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
                        if(b is RadioButton)
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
        public void UpdateQuestionsList()
        {
            QuestionsListBox.Items.Clear();
            for(int i = 0; i< NewTest.Questions.Count; i++)
            {
                NewTest.Questions[i].QuestionId = i;
            }
            foreach (Question a in NewTest.Questions)
            {
                QuestionsListBox.Items.Add(a.QuestionId+". "+a.QuestionText);
            }
        }
        private void CreateQuestion_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckTestProperties())
            {
                return;
            }
            ChooseQuestion messageBox = new ChooseQuestion();
            messageBox.ShowDialog();
            CustomMessageBoxResult result = messageBox.Result;
            switch (result)
            {
                case CustomMessageBoxResult.Text:
                    NewTest.Questions.Add(new TextQuestion() { QuestionId = NewTest.Questions.Count(), QuestionText = "Новый вопрос" });
                    break;
                case CustomMessageBoxResult.Radial:
                    NewTest.Questions.Add(new RadialQuestion() { QuestionId = NewTest.Questions.Count(), QuestionText = "Новый вопрос" });
                    break;
                case CustomMessageBoxResult.CheckBox:
                    NewTest.Questions.Add(new CheckBoxQuestion() { QuestionId = NewTest.Questions.Count(), QuestionText = "Новый вопрос" });
                    break;
                case CustomMessageBoxResult.Cancel:
                    return;
            }
            UpdateQuestionsList();
            QuestionsListBox.SelectedIndex = QuestionsListBox.Items.Count - 1;
        }

        private void GoBackBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void TestGrid_Initialized(object sender, EventArgs e)
        {
            Random random = new Random();
            string Dict = "QWERTYUIOPASDFGHJKLZXCVBNM1234567890";
            string Code = "";
            for(int i = 0;i<8 ; i++)
            {
                Code += Dict[random.Next(0, Dict.Length - 1)];
            }
            DataTable Count = App.Get($"SELECT COUNT(*) AS Count FROM Tests WHERE Code = '{Code}'");
            if (Convert.ToInt32(Count.Rows[0][0]) == 0)
            {
                TestCodeTextBox.Text = Code;
            }
            else
            {
                TestGrid_Initialized(sender, e);
                return;
            }
            
            
        }
        private bool CheckTestProperties()
        {
            if (TestNameBox.Text == "" || TestCodeTextBox.Text == "")
            {
                MessageBox.Show("Заполните название и код теста");
                return false;
            }
            DataTable Count = App.Get($"SELECT COUNT(*) AS Count FROM Tests WHERE Code = '{TestCodeTextBox.Text}'");
            if (Convert.ToInt32(Count.Rows[0][0]) != 0)
            {
                MessageBox.Show("Тест с таким кодом уже существует!");
                return false;
            }
            NewTest.Code = TestCodeTextBox.Text;
            NewTest.Description = TestDescriptionBox.Text;
            NewTest.Name = TestNameBox.Text;
            NewTest.IsPublic = (bool)IsPublicCheckBox.IsChecked;
            NewTest.ShowAnswers = (bool)ShowAnswersCheckBox.IsChecked;
            return true;
        }
        private void QuestionsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!CheckTestProperties())
            {
                return;
            }
            TestGrid.Visibility = Visibility.Hidden;
            RadialQuestionGrid.Visibility = Visibility.Hidden;
            CheckBoxQuestionGrid.Visibility = Visibility.Hidden;
            QuestionTextLbl.Visibility = Visibility.Hidden;
            QuestionTextTextBox.Visibility = Visibility.Hidden;
            if (QuestionsListBox.SelectedIndex == -1)
            {
                return;
            }

            
            try
            {
                if (CurrentQuestion != null&& QuestionsListBox.SelectedIndex!=-1)
                { 
                    int id = CurrentQuestion.QuestionId;
                    for (int i = 0; i < NewTest.Questions.Count; i++)
                    {
                        if (NewTest.Questions[i].QuestionId == id)
                        {
                            NewTest.Questions[i] = CurrentQuestion;
                        }
                    }
                    
                }
                
            }
            catch(System.NullReferenceException)
            {
                MessageBox.Show("???");
            }
            CurrentQuestion = NewTest.Questions[QuestionsListBox.SelectedIndex];
            if (CurrentQuestion is TextQuestion) 
            {
                QuestionTextLbl.Visibility = Visibility.Visible;
                QuestionTextTextBox.Visibility = Visibility.Visible;
                QuestionTextTextBoxValue.Text = CurrentQuestion.QuestionText;
            }
            else if(CurrentQuestion is RadialQuestion)
            {
                RadialQuestionGrid.Visibility = Visibility.Visible;
                QuestionTextLbl.Visibility = Visibility.Visible;
                QuestionTextTextBox.Visibility = Visibility.Visible;
                QuestionTextTextBoxValue.Text = CurrentQuestion.QuestionText;
                
            }
            else if(CurrentQuestion is CheckBoxQuestion)
            {
                CheckBoxQuestionGrid.Visibility = Visibility;
                QuestionTextLbl.Visibility = Visibility.Visible;
                QuestionTextTextBox.Visibility = Visibility.Visible;
                QuestionTextTextBoxValue.Text = CurrentQuestion.QuestionText;
            }
            else
            {
                MessageBox.Show(CurrentQuestion.GetType().ToString());
            }
            LoadTheme();
            
        }


        private void SaveTestBtn_Click(object sender, RoutedEventArgs e)
        {
            string json = JsonConvert.SerializeObject(NewTest);
            App.Set($"INSERT INTO Tests (Name,Description,ShowAnswers,IsPublic,Code,json) VALUES ('{NewTest.Name}','{NewTest.Description}','{NewTest.ShowAnswers}','{NewTest.IsPublic}','{NewTest.Code}','{json}')");
            MessageBox.Show("Тест опубликован!");
            NavigationService.Navigate(new MainPage());
        }

        private void RemoveRadialAnswer_Click(object sender, RoutedEventArgs e)
        {
            if (((RadialQuestion)CurrentQuestion).Answers.Count <= 0)
            {
                return;
            }
            ((RadialQuestion)CurrentQuestion).Answers.RemoveAt(((RadialQuestion)CurrentQuestion).Answers.Count-1);
            RadialAnswers.Children.Clear();
            foreach (string a in ((RadialQuestion)CurrentQuestion).Answers)
            {
                WrapPanel wrap = new WrapPanel();
                RadioButton radioButton = new RadioButton() { 
                    Content = ((RadialQuestion)CurrentQuestion).Answers.IndexOf(a),
                    IsChecked = ((RadialQuestion)CurrentQuestion).CorrectAnswerIndex == RadialAnswers.Children.Count, Width = 15
                };
                radioButton.Checked += RadioButton_Checked;
                wrap.Children.Add(radioButton);

                TextBox text = new TextBox { 
                    Text = a,
                    Width = 300 
                };
                text.TextChanged += RadioTextChanged;
                wrap.Children.Add(text);
                RadialAnswers.Children.Add(wrap);
            }
        }
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            foreach(WrapPanel a in RadialAnswers.Children)
            {
                foreach(var btn in a.Children)
                {
                    if(btn is RadioButton)
                    {
                        if (((RadioButton)btn).Content != ((RadioButton)sender).Content)
                        {
                            ((RadioButton)btn).IsChecked = false;
                        }
                        else
                        {
                            ((RadialQuestion)CurrentQuestion).CorrectAnswerIndex = RadialAnswers.Children.IndexOf(a);
                        }
                    }
                }
            }
        }
        private void RadioTextChanged(object sender, RoutedEventArgs e)
        {
            ((RadialQuestion)CurrentQuestion).Answers.Clear();
            foreach (WrapPanel a in RadialAnswers.Children)
            {
                foreach (var txt in a.Children)
                {
                    if (txt is TextBox)
                    {
                        ((RadialQuestion)CurrentQuestion).Answers.Add(((TextBox)txt).Text);
                    }
                }
            }
        }
        private void CreateRadialAnswer_Click(object sender, RoutedEventArgs e)
        {
            ((RadialQuestion)CurrentQuestion).Answers.Add("Введите вариант ответа");
            RadialAnswers.Children.Clear();
            foreach (string a in ((RadialQuestion)CurrentQuestion).Answers)
            {
                WrapPanel wrap = new WrapPanel();
                RadioButton radioButton = new RadioButton() { Content = ((RadialQuestion)CurrentQuestion).Answers.IndexOf(a), IsChecked = ((RadialQuestion)CurrentQuestion).CorrectAnswerIndex == RadialAnswers.Children.Count, Width = 15 };
                radioButton.Checked += RadioButton_Checked;
                wrap.Children.Add(radioButton);

                TextBox text = new TextBox { Text = a, Width = 300 };
                text.TextChanged += RadioTextChanged;
                wrap.Children.Add(text);
                RadialAnswers.Children.Add(wrap);
            }
        }

        private void QuestionTextTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (CurrentQuestion != null && QuestionsListBox.SelectedIndex != -1)
            {
                CurrentQuestion.QuestionText = QuestionTextTextBoxValue.Text;
            }
        }

        private void TestNameBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(!(TestNameBox is null))
            {
                NewTest.Name = TestNameBox.Text;
            }
        }

        private void RichTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!(TestDescriptionBox is null))
            {
                NewTest.Description = TestDescriptionBox.Text;
            }
            
        }

        private void ShowAnswersCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            NewTest.ShowAnswers = (bool)ShowAnswersCheckBox.IsChecked;
        }

        private void TestCodeTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(!(TestCodeTextBox is null))
            {
                NewTest.Code = TestCodeTextBox.Text;
            }
           
        }
        private void CheckBoxAnswer_Checked(object sender, RoutedEventArgs e)
        {
            ((CheckBoxQuestion)CurrentQuestion).CorrectAnswersIndex.Clear();
            foreach (WrapPanel a in CheckBoxAnswers.Children)
            {
                foreach (var btn in a.Children)
                {
                    if (btn is CheckBox)
                    {
                        if ((bool)((CheckBox)btn).IsChecked)
                        {
                            ((CheckBoxQuestion)CurrentQuestion).CorrectAnswersIndex.Add(CheckBoxAnswers.Children.IndexOf(a));
                        }
                    }
                }
            }
        }
        private void CheckBoxAnswerTextChanged(object sender, RoutedEventArgs e)
        {
            ((CheckBoxQuestion)CurrentQuestion).Answers.Clear();
            foreach (WrapPanel a in CheckBoxAnswers.Children)
            {
                foreach (var txt in a.Children)
                {
                    if (txt is TextBox)
                    {
                        ((CheckBoxQuestion)CurrentQuestion).Answers.Add(((TextBox)txt).Text);
                    }
                }
            }
        }
        private void CreateCheckBoxAnswer_Click(object sender, RoutedEventArgs e)
        {
            ((CheckBoxQuestion)CurrentQuestion).Answers.Add("Введите вариант ответа");
            CheckBoxAnswers.Children.Clear();
            foreach (string a in ((CheckBoxQuestion)CurrentQuestion).Answers)
            {
                WrapPanel wrap = new WrapPanel();
                CheckBox checkBox = new CheckBox() { Content = ((CheckBoxQuestion)CurrentQuestion).Answers.IndexOf(a), IsChecked = ((CheckBoxQuestion)CurrentQuestion).CorrectAnswersIndex.Contains(CheckBoxAnswers.Children.Count), Width = 15 };
                checkBox.Click += CheckBoxAnswer_Checked;
                wrap.Children.Add(checkBox);

                TextBox text = new TextBox { Text = a, Width = 300 };
                text.TextChanged += CheckBoxAnswerTextChanged;
                wrap.Children.Add(text);
                CheckBoxAnswers.Children.Add(wrap);
            }
        }

        private void RemoveCheckBoxAnswer_Click(object sender, RoutedEventArgs e)
        {
            if (((CheckBoxQuestion)CurrentQuestion).Answers.Count <= 0)
            {
                return;
            }
            ((CheckBoxQuestion)CurrentQuestion).Answers.RemoveAt(((CheckBoxQuestion)CurrentQuestion).Answers.Count - 1);
            CheckBoxAnswers.Children.Clear();
            foreach (string a in ((CheckBoxQuestion)CurrentQuestion).Answers)
            {
                WrapPanel wrap = new WrapPanel();
                CheckBox checkBox = new CheckBox() { Content = ((CheckBoxQuestion)CurrentQuestion).Answers.IndexOf(a), IsChecked = ((CheckBoxQuestion)CurrentQuestion).CorrectAnswersIndex.Contains(CheckBoxAnswers.Children.Count), Width = 15 };
                checkBox.Click += CheckBoxAnswer_Checked;
                wrap.Children.Add(checkBox);

                TextBox text = new TextBox { Text = a, Width = 300 };
                text.TextChanged += CheckBoxAnswerTextChanged;
                wrap.Children.Add(text);
                CheckBoxAnswers.Children.Add(wrap);
            }
        }

        private void DeleteQuestion_Click(object sender, RoutedEventArgs e)
        {
            if(QuestionsListBox.SelectedIndex != -1)
            {
                NewTest.Questions.RemoveAt(QuestionsListBox.SelectedIndex);
                UpdateQuestionsList();
            }
        }
    }
}
