using System;

namespace SocialMediaApp.App_Classes
{
    public class Post
    {
        public Nullable<Guid> PostID { get; set; }
        public string PostText { get; set; }
        public byte[] PostImage { get; set; }

    }
}