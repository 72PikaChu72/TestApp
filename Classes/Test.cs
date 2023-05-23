using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Classes
{
    public class Test
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Type { get; set; }
        public List<Question> Questions { get; set; }
        public bool ShowAnswers { get; set; }
        public bool IsPublic { get; set; }
        public List<User> ForUsers { get; set; }
        public string Code { get; set; }

    }
}
