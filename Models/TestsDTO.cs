using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Tests.Models
{
    public class TestsDTO
    {
        public Test test { set; get; }
        public List<Question> questions { set; get; }
        public TestsDTO()
        {
            this.test = null;
            this.questions = new List<Question>();
        }
    }
}
