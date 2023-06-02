using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace UsosAppMaui.Model.Map
{
    public class Building
    {
        public string id { get; set; }
        public Name name { get; set; }
        public Location location { get; set; }
    }
}
