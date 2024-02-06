using SocialMediaApp.Models;

namespace SocialMediaApp.Helpers
{
    public static class ConnectionHelper
    {
        private static SocialMediaAppDbDataContext context;

        public static SocialMediaAppDbDataContext Context
        {
            get
            {
                if (context == null)
                {
                    context = new SocialMediaAppDbDataContext();
                }
                return context;
            }

        }

    }
}