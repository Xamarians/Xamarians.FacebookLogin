using Facebook.CoreKit;
using Facebook.LoginKit;
using Foundation;
using System.Threading.Tasks;
using UIKit;
using Xamarians.FacebookLogin.Platforms;
using Xamarin.Forms;
using Xamarians.FacebookLogin.iOS.DS;

[assembly: Dependency(typeof(Xamarians.FacebookLogin.iOS.DS.FacebookLogin))]
namespace Xamarians.FacebookLogin.iOS.DS
{
    public class FacebookLogin : Xamarians.FacebookLogin.IFacebookLogin
	{
		public static void Init()
		{
			
		}

		private UIViewController GetController()
		{
			return UIApplication.SharedApplication.KeyWindow.RootViewController;
		}

		LoginManager manager;
		public async Task<FbLoginResult> SignIn()
		{
			var tcs = new TaskCompletionSource<FbLoginResult>();
			manager = new LoginManager();
			manager.Init();
			var result = await manager.LogInWithReadPermissionsAsync(new string[] { "email", "public_profile" }, GetController());
			if (!result.IsCancelled)
			{
				try
				{
					var request = new GraphRequest("/me?fields=id,name,email", null, result.Token.TokenString, null, "GET");
					request.Start((connection, res, error) =>
					{
						var userInfo = res as NSDictionary;
						var id = userInfo["id"].ToString();
						var name = userInfo["name"].ToString();
						var email = userInfo["email"].ToString();
						tcs.SetResult(new FbLoginResult
						{
							AccessToken = result.Token.ToString(),
							UserId = id,
							Name = name,
							Email = email,
                            Status = FBStatus.Success
						});
					});
				}
				catch { }
			}
			else if (result.IsCancelled)
			{
				tcs.SetResult(null);
			}

			return await tcs.Task;
		}

		public Task<FbLoginResult> SignOut()
		{
			var tcs = new TaskCompletionSource<FbLoginResult>();
			if (manager != null)
			{
				manager.LogOut();
				tcs.SetResult(new FbLoginResult()
				{
					Status = FBStatus.Success,
					Message = "Successfully Logged Out"
				});
			}
			else
			{
				tcs.SetResult(new FbLoginResult()
				{
					Status = FBStatus.Error,
					Message = "You are not Logged In"
				});
			}
			return tcs.Task;
		}
	}
}
