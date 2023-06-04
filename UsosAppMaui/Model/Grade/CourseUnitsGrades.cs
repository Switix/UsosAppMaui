using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsosAppMaui.Model.Grade
{
    public class CourseUnitsGrades
    {
        [JsonExtensionData]
        public Dictionary<string, JToken> CourseUnitGrade { get; set; }
    }
}
