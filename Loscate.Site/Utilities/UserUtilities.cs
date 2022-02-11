using Loscate.DTO.Firebase;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace Loscate.Site.Utilities
{
    public static class UserUtilities
    {
        public static FirebaseUser ToFirebaseUser(this ClaimsPrincipal user)
        {
            return GetFirebaseUser(user);
        }

        private static FirebaseUser GetFirebaseUser(ClaimsPrincipal user)
        {
            if (user != null)
            {
                if (user.Identity != null)
                {
                    //https://firebase.google.com/docs/rules/rules-and-auth
                    var name = user.Claims.SingleOrDefault(x => x.Type == "name")?.Value;
                    var uid = user.Claims.SingleOrDefault(x => x.Type == "user_id")?.Value;
                    var pictureUrl = user.Claims.SingleOrDefault(x => x.Type == "picture")?.Value;

                    var firebaseUser = JObject.Parse(user.Claims.SingleOrDefault(x => x.Type == "firebase")?.Value!);
                    var email = firebaseUser["identities"]?["email"]?[0]?.ToString();

                    return new FirebaseUser(uid, name, email, pictureUrl, ConvertImageUrlToBase64(pictureUrl));
                }

                throw new NullReferenceException("null user identity");
            }

            throw new NullReferenceException("null user");
        }

        private static string ConvertImageUrlToBase64(string url)
        {
            try
            {
                var byteArray = GetImage(url);
                return Convert.ToBase64String(byteArray, 0, byteArray.Length);
            }
            catch
            {
                return string.Empty;
            }
        }

        private static byte[] GetImage(string url)
        {
            byte[] buf;
            
            var req = (HttpWebRequest)WebRequest.Create(url);

            var response = (HttpWebResponse)req.GetResponse();
            var stream = response.GetResponseStream();

            using (var br = new BinaryReader(stream))
            {
                var len = (int)(response.ContentLength);
                buf = br.ReadBytes(len);
                br.Close();
            }

            stream.Close();
            response.Close();

            return buf;
        }
    }
}