using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsosAppMaui.Model.Unit;
using UsosAppMaui.Service;

namespace UsosAppMaui.ViewModel
{
    public partial class GradeViewModel:ObservableObject
    {
        private UsosService usosService;

        public List<Unit> units;

        [ObservableProperty]
        public List<GradeTerms> gradeTerms = new List<GradeTerms>();

        public GradeViewModel(UsosService usosService)
        {
            units = usosService.getUserGrades();
            
       

            Dictionary<string, List<Unit>> unitGroups = units.GroupBy(unit => unit.term_id)
    .ToDictionary(group => group.Key, group => group.ToList());

            foreach (var term in unitGroups)
            {
                gradeTerms.Add(new GradeTerms(term.Key, term.Value));
            }
            gradeTerms.Sort((obj1, obj2) =>
            {
                string termId1 = obj1.term_id;
                string termId2 = obj2.term_id;
                int termYear1 = int.Parse(termId1.Substring(0, 4));
                int termYear2 = int.Parse(termId2.Substring(0, 4));

                if (termYear1 < termYear2)
                    return 1;
                if (termYear1 > termYear2)
                    return -1;
                return obj1.term_id.CompareTo(obj2.term_id);
            });
        }
        
    }
}
