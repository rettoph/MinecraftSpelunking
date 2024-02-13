namespace MinecraftSpelunking.Presentation.WebServer
{
    public static class Constants
    {
        public static class Routes
        {
            public const string Home = "/";

            public static class Account
            {
                public const string Index = $"/Account";
                public const string RegisterAdmin = $"{Routes.Account.Index}/RegisterAdmin";
            }
        }

        public static class QueryParameters
        {
            public const string ReturnUrl = "returnUrl";
        }
    }
}
