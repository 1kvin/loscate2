using Loscate.Site.DbContext;
using Loscate.Site.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Loscate.Site.UnitTests.Utilities.DtoConvert;

[TestClass]
public class ConvertPinTest
{
    private const int testUserId = 1;
    private const string testUserUid = "uid";

    private const int testPinId = 1;
    private const double testPinLatitude = 1;
    private const double testPinLongitude = 2;
    private const string testPinPhoto = "photo";
    private const string testPinFullName = "FullName";
    private const string testPinShortName = "ShortName";
    
    private readonly FirebaseUser testUser;

    public ConvertPinTest()
    {
        testUser = new FirebaseUser { Id = testUserId, Uid = testUserUid };
    }
    
    [TestMethod]
    public void DbPinToDtoConvert()
    {
        var dbPin = new Pin()
        {
            Id = testPinId,
            Latitude = testPinLatitude,
            Longitude = testPinLongitude,
            Photo = testPinPhoto,
            FullName = testPinFullName,
            ShortName = testPinShortName,
            User = testUser,
            UserId = testUser.Id
        };

        var dtoPin = dbPin.ConvertToDto();
        
        Assert.AreEqual(dtoPin.Latitude, testPinLatitude);
        Assert.AreEqual(dtoPin.Longitude, testPinLongitude);
        Assert.AreEqual(dtoPin.ShortName, testPinShortName);
        Assert.AreEqual(dtoPin.FullName, testPinFullName);
        Assert.AreEqual(dtoPin.PhotoBase64, testPinPhoto);
        Assert.AreEqual(dtoPin.Id, testPinId);
        Assert.AreEqual(dtoPin.UserUID, testUserUid);
    }
    
    [TestMethod]
    public void DtoPinToDbConvert()
    {
        var dtoPin = new DTO.Map.Pin()
        {
            Id = testPinId,
            Latitude = testPinLatitude,
            Longitude = testPinLongitude,
            FullName = testPinFullName,
            ShortName = testPinShortName,
            UserUID = testUserUid,
            PhotoBase64 = testPinPhoto
        };

        var dbPin = dtoPin.ConvertToDb(testUser);
        
        Assert.AreEqual(dbPin.Latitude, testPinLatitude);
        Assert.AreEqual(dbPin.Longitude, testPinLongitude);
        Assert.AreEqual(dbPin.ShortName, testPinShortName);
        Assert.AreEqual(dbPin.FullName, testPinFullName);
        Assert.AreEqual(dbPin.Photo, testPinPhoto);
        Assert.AreEqual(dbPin.Id, testPinId);
        Assert.AreEqual(dbPin.UserId, testUserId);
    }
}