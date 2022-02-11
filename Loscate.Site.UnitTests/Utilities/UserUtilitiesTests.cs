using System;
using System.Collections.Generic;
using System.Security.Claims;
using Loscate.DTO.Firebase;
using Loscate.Site.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Loscate.Site.UnitTests.Utilities;

[TestClass]
public class UserUtilitiesTests
{
    private const string testUserName = "Artem";
    private const string testUserUid = "uid";
    private const string testUserGoodPicture = "https://www.google.com/images/branding/googlelogo/2x/googlelogo_color_272x92dp.png";
    private const string testUserBadPicture = "123123";
    private const string testUserEmail = "myEmail";
    private const string testUserFirebaseProfile = "{  \"identities\" :   {    \"email\" : [\"myEmail\"]  }}";

    [TestMethod]
    public void NullUserTest()
    {
        Assert.ThrowsException<NullReferenceException>(() => { UserUtilities.ToFirebaseUser(null); }, "null user");
    }
    
    [TestMethod]
    public void NullUserIdentityTest()
    {
        Assert.ThrowsException<NullReferenceException>(() => { new ClaimsPrincipal().ToFirebaseUser(); }, "null user identity");
    }

    [TestMethod]
    public void AllFieldTest()
    {
        var user = CommonTest(testUserGoodPicture);
        Assert.IsFalse(string.IsNullOrWhiteSpace(user.PhotoBase64));
    }
    
    [TestMethod]
    public void WrongImageUrlTest()
    {
        var user = CommonTest(testUserBadPicture);
        Assert.IsTrue(string.IsNullOrWhiteSpace(user.PhotoBase64));
    }
    
    private FirebaseUser CommonTest(string pictureUrl)
    {
        var claims = new List<Claim>() 
        { 
            new("name", testUserName),
            new("user_id", testUserUid),
            new("picture", pictureUrl),
            new("firebase", testUserFirebaseProfile),
        };
        var identity = new ClaimsIdentity(claims, "TestAuthType");
        var claimsPrincipal = new ClaimsPrincipal(identity);


        var firebaseUser = claimsPrincipal.ToFirebaseUser();
        
        Assert.AreEqual(firebaseUser.Name, testUserName);
        Assert.AreEqual(firebaseUser.UID, testUserUid);
        Assert.AreEqual(firebaseUser.PictureUrl, pictureUrl);
        Assert.AreEqual(firebaseUser.EMail, testUserEmail);

        return firebaseUser;
    }
}