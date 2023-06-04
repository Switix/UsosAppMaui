using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsosAppMaui.Model.Course;

namespace UsosAppMaui
{
    public partial class Semestrs
    {
        public string semId { get; set; }
        public List<Term> terms { get; set; }

        public Semestrs(string semId, List<Term> terms)
        {
            this.semId = semId;
            this.terms = terms;
        }
    }
}
