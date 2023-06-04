using UsosAppMaui.Service;
using UsosAppMaui.ViewModel;

namespace UsosAppMaui.Pages;

public partial class GroupDetailPage : ContentPage
{
	public GroupDetailPage(GroupDetailViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}