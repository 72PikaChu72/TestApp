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
        public int CorrectAnswerIndex = 0;
        public List<string> Answers = new List<string>();

    }
    public class CheckBoxQuestion : Question
    {
        public List<int> CorrectAnswersIndex = new List<int>();
        public List<string> Answers = new List<string>();

    }
    public class TextQuestion : Question
    {

    }

}
