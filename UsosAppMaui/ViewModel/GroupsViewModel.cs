using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsosAppMaui.Service;
using UsosAppMaui.Model.Course;
using CommunityToolkit.Mvvm.Input;
using UsosAppMaui.Pages;
using System.Collections;

namespace UsosAppMaui
{
    public partial class GroupsViewModel : ObservableObject
    {
        private UsosService usosService;
        [ObservableProperty]
        public List<Semestrs> result;

        public GroupsViewModel(UsosService usosService)
        {
            this.usosService = usosService;
            var sems = usosService.getUserCourses();
            result = new List<Semestrs>();
            
            foreach(var sem in sems)
            {
                result.Add(new Semestrs(sem.Key, sem.Value));                
            }
            result.Sort((obj1, obj2) =>
            {
                string semId1 = obj1.semId.Substring(0, 4);
                string semId2 = obj2.semId.Substring(0, 4);
                int semYear1 = int.Parse(semId1);
                int semYear2 = int.Parse(semId2);

                if (semYear1 < semYear2)
                    return 1;
                if (semYear1 > semYear2)
                    return -1;
                return obj1.semId.CompareTo(obj2.semId);
            });
        }


        [RelayCommand]
        public async Task GroupTap(UserGroups group)
        {
            await Shell.Current.GoToAsync(nameof(GroupDetailPage),
                new Dictionary<string, object>
                {
                    { "UserGroups", group }
                });
        }
    }
}
