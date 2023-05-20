using ElevenCourses.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Tests.Models
{
    public class Results
    {
        public Guid id { set; get; }
        public String userId { set; get; }
        public ApplicationUser user { set; get; }
        public int points { set; get; }
        public String testName{ set; get; }
        public int procent { set; get; }
        public Guid testid { set; get; }
        public Test test { set; get; }

    }
}
