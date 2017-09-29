using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DotNetCoreThumbor.Test
{
    [TestClass]
    public class ThumborTest
    {

        private IThumbor thumbor;

        [TestInitialize]
        public void Initialize()
        {
            thumbor = new Thumbor("http://localhost/", "secret_key");
        }


        [TestMethod]
        public void BuildSignedUrl_ShoudBe_SameSecretKey()
        {
            // Act
            var thumborUrl = thumbor.BuildSignedUrl("http://myUrl/myimage.jpg");

            // Assert
            Assert.AreEqual(thumborUrl, "/Vmtcp9WWg_QGB_UahYp2zHni4Xc=/http://myUrl/myimage.jpg");
        }


        [TestMethod]
        public void BuildImage_Resize_to_300x300_Contains300x300()
        {
            // Act
             var thumborUrl = thumbor.BuildImage("http://myUrl/myimage.jpg").Resize(300,300);

            // Assert
            Assert.AreEqual(thumborUrl.ToUrl(), "dDRIn5qUtwPceuMImU2cCkEGRec=/300x300/http://myUrl/myimage.jpg");
            Assert.IsTrue(thumborUrl.ToUrl().Contains("300x300"));

        }

        [TestMethod]
        public void BuildImage_FullUrl_Contains_Localhost()
        {
            // Act
            var thumborUrl = thumbor.BuildImage("http://myUrl/myimage.jpg").Resize(300, 300);

            // Assert
            Assert.IsTrue(thumborUrl.ToFullUrl().Contains("http://localhost"));

        }


        [TestMethod]
        public void BuildImage_Url_Contains_Smart_When_Smart_Is_True()
        {
            // Act
            var thumborUrl = thumbor.BuildImage("http://myUrl/myimage.jpg").Resize(300, 300).Smart(true);

            // Assert
            Assert.IsTrue(thumborUrl.ToUrl().Contains("smart"));
        }


        [TestMethod]
        public void BuildImage_False_NotEquals_Create_New_Image_with_Another_Key()
        {
            // Arrange
            var newthumbor = new Thumbor("http://localhost/", "secret_key_2");

            // Act
            var thumborUrl = thumbor.BuildImage("http://myUrl/myimage.jpg").Resize(300, 300).Smart(true);
            var thumborUrl2 = newthumbor.BuildImage("http://myUrl/myimage.jpg").Resize(300, 300).Smart(true);

            // Assert
            Assert.AreNotEqual(thumborUrl.ToUrl(), thumborUrl2.ToUrl());
        }


    }
   

}
