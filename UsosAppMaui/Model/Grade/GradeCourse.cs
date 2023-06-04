using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsosAppMaui.Model.Grade
{
    public class GradeCourse
    {
        public List<object> course_grades { get; set; }

        [JsonExtensionData]
        public  Dictionary<string,JToken> course_units_grades { get; set; }
        
    }
}
