using ElevenCourses.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Tests.Models
{
    public class Test
    {
        public Guid id { set; get; }
        public string userId { set; get; }
        public string name { set; get; }
        public int time { set; get; }
        public ApplicationUser user { set; get; }
        public  ICollection<QuestionInTest> Questions { set; get; }

    }
}
