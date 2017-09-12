using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarians.FacebookLogin;
using Xamarin.Forms;
#if __ANDROID__
using Newtonsoft.Json;
#endif
using Xamarians.FacebookLogin.Platforms;

namespace Sample
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
		}

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var result = await DependencyService.Get<IFacebookLogin>().SignIn();
            if(result.Status == Xamarians.FacebookLogin.Platforms.FBStatus.Success)
            {
#if __ANDROID__
                var userDetails = JsonConvert.DeserializeObject<FbLoginResult>(result.JsonData);
                await DisplayAlert("Success", "Welcome" + userDetails.Name, "Ok");
#endif
#if __IOS__
                await DisplayAlert("Success", "Welcome" + result.Name, "Ok");
#endif

            }
            else
            {
                await DisplayAlert("Error", result.Message, "Ok");
            }

        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            var result = await DependencyService.Get<IFacebookLogin>().SignOut();
            if (result.Status == Xamarians.FacebookLogin.Platforms.FBStatus.Success)
            {
                await DisplayAlert("Success", result.Message, "Ok");
            }
            else
            {
                await DisplayAlert("Error", result.Message, "Ok");
            }
        }
    }
}
