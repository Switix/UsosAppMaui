using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsosAppMaui.Model.Course;

namespace UsosAppMaui.ViewModel
{
    [QueryProperty("Groups", "UserGroups")]
    public partial class GroupDetailViewModel:ObservableObject
    {
        [ObservableProperty]
        
        public UserGroups groups;

        partial void OnGroupsChanged(UserGroups oldValue, UserGroups newValue)
        {
           
        }

    }
}
