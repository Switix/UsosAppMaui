using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsosAppMaui.Model.TimeTable
{
    public class Lesson
    {
        public DateTime start_time { get; set; }
        public DateTime end_time { get; set; }
        public Name name { get; set; }
    }
}
