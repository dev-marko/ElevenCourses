using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Tests.Models
{
    public class QuestionInTest
    {
        public Guid questionId { set; get; }
        public Question question { set; get; }
        public Guid testId { set; get; }
        public Test test { set; get; }
    }
}
