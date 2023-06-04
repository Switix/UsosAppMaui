using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace UsosAppMaui.Model.Grade
{
    public class UserGrade
    {
        [JsonExtensionData]
        public Dictionary<string, JToken> Terms { get; set; }
    }
}
