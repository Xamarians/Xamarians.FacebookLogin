using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarians.FacebookLogin.Platforms;

namespace Xamarians.FacebookLogin
{
    public interface IFacebookLogin
    {
        Task<FbLoginResult> SignIn();
        Task<FbLoginResult> SignOut();
    }
}
