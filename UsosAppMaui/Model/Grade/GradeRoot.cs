using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using JsonExtensionDataAttribute = Newtonsoft.Json.JsonExtensionDataAttribute;

namespace UsosAppMaui.Model.Grade
{
    public class GradeRoot
    {
        [JsonExtensionData]
        public Dictionary<string, JToken> Terms { get; set; }
    }
}
