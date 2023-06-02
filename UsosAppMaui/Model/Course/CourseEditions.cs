using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsosAppMaui.Model.Course
{
    public class CourseEditions
    {
        [JsonExtensionData]
        public Dictionary<string, JToken> Terms { get; set; }

    }
}
