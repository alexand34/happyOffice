using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web;

namespace Office.BusinessLogic
{
    public class StoreImagesService
    {
        public string UploadFile(HttpPostedFile file, string mapPath)
        {
            string fileName = new FileInfo(file.FileName).Name;

            if (file.ContentLength > 0)
            {
                Guid id = Guid.NewGuid();

                var filePath = Path.GetFileName(id.ToString() + "_" + fileName);

                if (!File.Exists(mapPath + filePath))
                {
                    file.SaveAs(mapPath + filePath);
                    return mapPath + filePath;
                }
                return null;
            }
            return null;
        }
    }
}
