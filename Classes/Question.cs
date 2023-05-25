using Newtonsoft.Json;
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
        [JsonProperty("type")]
        public string Type { get { return "Radial"; } }

        [JsonProperty("correctAnswerIndex")]
        public int CorrectAnswerIndex { get; set; }

        [JsonProperty("answers")] 
        public List<string> Answers = new List<string>();
    }

    public class CheckBoxQuestion : Question
    {
        [JsonProperty("type")]
        public string Type { get { return "CheckBox"; } }

        [JsonProperty("correctAnswersIndex")]
        public List<int> CorrectAnswersIndex  = new List<int>();

        [JsonProperty("answers")]
        public List<string> Answers = new List<string>();
    }

    public class TextQuestion : Question
    {
        [JsonProperty("type")]
        public string Type { get { return "Text"; } }
    }

}
