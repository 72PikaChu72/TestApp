using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Classes
{
    public class Question
    {
        public string QuestionText { get; set; }
        public int QuestionId { get; set; }

    }
    public class RadialQuestion : Question
    {
        public int CorrectAnswerIndex { get; set; }
        public string[] Answers { get; set; }

    }
    public class CheckBoxQuestion : Question
    {
        public int[] CorrectAnswersIndex { get; set; }
        public string[] Answers { get; set; }

    }
    public class TextQuestionAnswerable : Question
    {
        public string CorrectAnswer { get; set; }

    }
    public class TextQuestion : Question
    {

    }

}
