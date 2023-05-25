using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Classes
{
    public class Answer
    {
       public Question Question { get; set; }
       public string TextAnswer { get; set; }
       public List<int> answers { get; set; }
    }

}
