using SocialMediaApp.Models;
using System;
using System.IO;
using System.Web;

namespace SocialMediaApp.Helpers
{
    public class PostHelpers
    {
        private readonly SocialMediaAppDbDataContext db = new SocialMediaAppDbDataContext();
        public int UploadImageInDataBase(HttpPostedFileBase file, Post post)
        {
            if (file.InputStream.Length != 0)
            {
                post.PostPicture = Compress(ConvertToBytes(file));
            }

            post.DateAdded = DateTime.Now;
            db.Posts.InsertOnSubmit(post);
            db.SubmitChanges();

            if (post.Postid > 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        public byte[] ConvertToBytes(HttpPostedFileBase image)
        {
            BinaryReader reader = new BinaryReader(image.InputStream);
            byte[] imageBytes = reader.ReadBytes(image.ContentLength);
            return imageBytes;
        }

        public static byte[] Compress(byte[] data)
        {
            System.IO.MemoryStream myMemStream = new System.IO.MemoryStream(data);
            System.Drawing.Image fullsizeImage = System.Drawing.Image.FromStream(myMemStream);
            System.Drawing.Image newImage = fullsizeImage.GetThumbnailImage(Convert.ToInt32(fullsizeImage.Width / 1.6), Convert.ToInt32(fullsizeImage.Height / 1.6), null, IntPtr.Zero);
            System.IO.MemoryStream myResult = new System.IO.MemoryStream();
            newImage.Save(myResult, System.Drawing.Imaging.ImageFormat.Jpeg);
            return myResult.ToArray();
        }
    }
}