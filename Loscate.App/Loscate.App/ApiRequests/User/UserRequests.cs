using System.Collections.Generic;
using System.Threading.Tasks;
using Loscate.App.Services;
using Loscate.App.Utilities;
using Loscate.DTO.Firebase;

namespace Loscate.App.ApiRequests.User
{
    public static class UserRequests
    {
        public static async Task<string> ChangeUserName(IFirebaseAuthenticator firebaseAuthenticator, string newName)
        {
            return await ApiRequest.MakeRequest<string>(firebaseAuthenticator,
                $"api/user/editUserName?newName={newName}");
        }

        public static async Task<string> ChangeUserPhoto(IFirebaseAuthenticator firebaseAuthenticator, string newPhoto)
        {
            var values = new Dictionary<string, string>
            {
                { "photo", newPhoto }
            };

            return await ApiRequest.MakePostRequest<string>(firebaseAuthenticator, $"api/user/editPhoto", values);
        }

        public static async Task<FirebaseUser> GetUser(IFirebaseAuthenticator firebaseAuthenticator)
        {
            return await ApiRequest.MakeRequest<FirebaseUser>(firebaseAuthenticator, "api/user/getFirebaseUser");
        }
    }
}