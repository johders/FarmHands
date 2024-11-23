using CommunityToolkit.Mvvm.ComponentModel;

namespace Mde.Project.Mobile.ViewModels
{
    [QueryProperty(nameof(RoleName), nameof(RoleName))]
    public class RegisterViewModel : ObservableObject
    {
		private string roleName;

		public string RoleName
		{
			get { return roleName; }
			set { roleName = value; }
		}



	}
}
