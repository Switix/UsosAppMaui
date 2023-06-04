using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsosAppMaui.Model.Grade
{
    public class CourseData
    {
        public Dictionary<string, Dictionary<string, List<Dictionary<string, GradeValue>>>> Data { get; set; }

        public class GradeValue
        {
            public string ValueSymbol { get; set; }
        }
    }
}
