using Loscate.DTO.Firebase;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace Loscate.App.Models
{
    public class XamarinUser : FirebaseUser
    {
        public XamarinUser(FirebaseUser firebaseUser) 
        {
            UID = firebaseUser.UID;
            Name = firebaseUser.Name;
            EMail = firebaseUser.EMail;
            PictureUrl = firebaseUser.PictureUrl;
            PhotoBase64 = firebaseUser.PhotoBase64;
            UpdateUserImage();
        }
        public ImageSource UserImage { get; set; }

        public void UpdateUserImage()
        {
            UserImage = string.IsNullOrEmpty(PhotoBase64)
                ? ImageSource.FromUri(new Uri(PictureUrl))
                : ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(PhotoBase64)));
        }
    }
}
