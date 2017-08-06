using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Office.BusinessLogic
{
    public class StoreImagesService
    {
        private readonly string imagePath = "~/images";
        public StoreImagesService()
        {
            var path = Path.Combine(imagePath);
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);   
        }

        public void CreatePersonGroupFolder(string personGroupid)
        {
            var path = Path.Combine(imagePath, personGroupid);
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }

        public void CreatePersonFolder(string personGroupId, string personId)
        {
            var path = Path.Combine(imagePath, personGroupId, personId);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        private string GetStringSha256Hash(string text)
        {
            if (String.IsNullOrEmpty(text))
                return String.Empty;

            using (var sha = new System.Security.Cryptography.SHA256Managed())
            {
                byte[] textData = System.Text.Encoding.UTF8.GetBytes(text);
                byte[] hash = sha.ComputeHash(textData);
                return BitConverter.ToString(hash).Replace("-", String.Empty);
            }
        }

        public void StorePersonPhotoToPersonGroupFolder(Bitmap userImage, string personGroupId, string personId)     
        {
            Image img = userImage; 
            var path = Path.Combine(imagePath, personGroupId, personId);
            Random r = new Random();
            string fileName = personId + r.Next().ToString();
            img.Save(path + img, ImageFormat.Png);
        }

        public string[] GetPersonPhotos(string personGroupId, string personId)
        {
            string path = Path.Combine(imagePath, personGroupId, personId);
            var photos = Directory.GetFiles(path);
            return photos;
        }

        public void DeletePersonGroupFolder(string personGroupId)
        {
            var path = Path.Combine(imagePath, personGroupId);
            Directory.Delete(path);
        }

        public void DeletePersonFolder(string personGroupId, string personId)
        {
            var path = Path.Combine(imagePath, personGroupId, personId);
            Directory.Delete(path);
        }
    }
}
