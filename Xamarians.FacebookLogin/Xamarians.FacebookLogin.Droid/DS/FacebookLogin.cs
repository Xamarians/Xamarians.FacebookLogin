using System.Threading.Tasks;
using Android.Content;
using Xamarians.FacebookLogin.Platforms;
using Xamarians.FacebookLogin.Droid.Platforms;
using Xamarin.Forms;
using Xamarians.FacebookLogin.Droid.DS;

[assembly: Dependency(typeof(FacebookLogin))]
namespace Xamarians.FacebookLogin.Droid.DS
{
    public class FacebookLogin : IFacebookLogin
    {
        public static string FacebookAppId { get; set; }
        public static void Init(string facebookAppId)
        {
            FacebookAppId = facebookAppId;
        } 

        public Task<FbLoginResult> SignIn()
        {
            var tcs = new TaskCompletionSource<FbLoginResult>();
            FacebookLoginActivity.OnLoginCompleted(tcs);
            var fbIntent = new Intent(Xamarin.Forms.Forms.Context, typeof(FacebookLoginActivity));
            fbIntent.PutExtra("Permissions", "email");
            Xamarin.Forms.Forms.Context.StartActivity(fbIntent);
            return tcs.Task;
        }

        public Task<FbLoginResult> SignOut()
        {
            var tcs = new TaskCompletionSource<FbLoginResult>();
            FacebookLoginActivity.SignOut(tcs);
            return tcs.Task;
        }
    }
}