using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsosAppMaui.Model.Course
{
    public class UserGroups
    {
        public int group_number { get; set; }
        public ClassType class_type { get; set; }
        public string course_id { get; set; }
        public CourseName course_name { get; set; }
        public List<Person> lecturers { get; set; }
        public List<Person> participants { get; set; }
    }
}
