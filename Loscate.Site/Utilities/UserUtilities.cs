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
                    var name = user.Claims.Where(x => x.Type == "name").SingleOrDefault()?.Value;
                    var uid = user.Claims.SingleOrDefault(x => x.Type == "user_id")?.Value;
                    var pictureUrl = user.Claims.SingleOrDefault(x => x.Type == "picture")?.Value;

                    var firebaseUser = JObject.Parse(user.Claims.SingleOrDefault(x => x.Type == "firebase")?.Value);
                    var email = firebaseUser["identities"]["email"][0].ToString();

                    return new FirebaseUser(uid, name, email, pictureUrl, ConvertImageURLToBase64(pictureUrl));
                }

                throw new NullReferenceException("null user identity");
            }

            throw new NullReferenceException("null user");
        }

        public static String ConvertImageURLToBase64(String url)
        {
            StringBuilder _sb = new StringBuilder();

            Byte[] _byte = GetImage(url);

            _sb.Append(Convert.ToBase64String(_byte, 0, _byte.Length));

            return _sb.ToString();
        }

        private static byte[] GetImage(string url)
        {
            Stream stream = null;
            byte[] buf;

            try
            {
                WebProxy myProxy = new WebProxy();
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);

                HttpWebResponse response = (HttpWebResponse)req.GetResponse();
                stream = response.GetResponseStream();

                using (BinaryReader br = new BinaryReader(stream))
                {
                    int len = (int)(response.ContentLength);
                    buf = br.ReadBytes(len);
                    br.Close();
                }

                stream.Close();
                response.Close();
            }
            catch (Exception exp)
            {
                buf = null;
            }

            return (buf);
        }
    }
}