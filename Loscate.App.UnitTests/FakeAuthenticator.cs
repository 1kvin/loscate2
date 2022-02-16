using Loscate.App.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Firebase.Auth;
using Google.Apis.Auth.OAuth2;
using Newtonsoft.Json.Linq;

namespace Loscate.App.UnitTests
{
    public class FakeAuthenticator : IFirebaseAuthenticator
    {
        private string token;
        public string GetAuthToken()
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                HttpClient client = new HttpClient();
                var content = new StringContent("{\"email\":\"test@test.ru\",\"password\":\"qwerty\",\"returnSecureToken\":true}", Encoding.UTF8, "application/json");
                var response  = client.PostAsync(
                    "https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword?key=AIzaSyCpm_bIDe1uRtMikNuvHnvy8uCi8boczck", content).Result;
                var jsonString = JObject.Parse(response.Content.ReadAsStringAsync().Result); 
                token = jsonString["idToken"]?.ToString();
            }
           
            
            return token;
        }

        public bool IsHaveUser()
        {
            throw new NotImplementedException();
        }

        public void SignIn()
        {
            throw new NotImplementedException();
        }

        public void SignOut()
        {
            throw new NotImplementedException();
        }

        public void SubscribeToTokenUpdate(Action action)
        {
            throw new NotImplementedException();
        }

        public void UnSubscribeToTokenUpdate(Action action)
        {
            throw new NotImplementedException();
        }
    }
}
