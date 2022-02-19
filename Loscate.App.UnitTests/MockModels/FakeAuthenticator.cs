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
        
        public bool IsSubscribe { get; set; }
        
        public bool IsSignIn { get; set; }

        public bool HaveUser { get; set; } = true;

        public string TestUserData = "{\"email\":\"test@test.ru\",\"password\":\"qwerty\",\"returnSecureToken\":true}";
        
        public string GetAuthToken()
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                HttpClient client = new HttpClient();
                var content = new StringContent(TestUserData, Encoding.UTF8, "application/json");
                var response  = client.PostAsync(
                    "https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword?key=AIzaSyCpm_bIDe1uRtMikNuvHnvy8uCi8boczck", content).Result;
                var jsonString = JObject.Parse(response.Content.ReadAsStringAsync().Result); 
                token = jsonString["idToken"]?.ToString();
            }
            
            return token;
        }

        public bool IsHaveUser()
        {
            return HaveUser;
        }

        public void SignIn()
        {
            IsSignIn = true;
        }

        public void SignOut()
        {
            IsSignIn = false;
        }

        public void SubscribeToTokenUpdate(Action action)
        {
            try
            {
                action?.Invoke();
            }
            catch
            {
                // ignored
            }

            IsSubscribe = true;
        }

        public void UnSubscribeToTokenUpdate(Action action)
        {
            
        }
    }
}
