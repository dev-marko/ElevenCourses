using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Tests.Models
{
    public class Question
    {
        public Guid id { set; get; }
        public String question { set; get; }
        public String correctAnswer { set; get; }
        public int points { set; get; }
        public string Answer1 { set; get; }
        public string Answer2 { set; get; }
        public string Answer3 { set; get; }
        public string Answer4 { set; get; }
        
        public  ICollection<QuestionInTest> Tests { set; get; }
    }
}
