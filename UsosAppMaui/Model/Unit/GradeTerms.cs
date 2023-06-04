using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsosAppMaui.Model.Unit
{
    public partial class GradeTerms
    {
        public string term_id { get; set; }
        public List<Unit> units { get; set; }

        public GradeTerms(string term_id, List<Unit> units)
        {
            this.term_id = term_id;
            this.units = units;
        }
    }
}
