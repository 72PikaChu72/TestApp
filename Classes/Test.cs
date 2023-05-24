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

        public List<Question> Questions = new List<Question>();
        public bool ShowAnswers { get; set; }
        public bool IsPublic { get; set; }

        //TODO: (Maybe)
        //public List<User> ForUsers { get; set; }

        public string Code { get; set; }

    }
}
