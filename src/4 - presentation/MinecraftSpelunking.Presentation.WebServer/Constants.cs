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
                public const string Login = $"{Routes.Account.Index}/Login";
                public const string Logout = $"{Routes.Account.Index}/Logout";
            }

            public static class Api
            {
                public const string Index = $"/api";

                public static class v1
                {
                    public const string Index = $"{Routes.Api.Index}/v1";

                    public static class AddressBlockAssignment
                    {
                        public const string Index = $"{Routes.Api.v1.Index}/address-block-assignment";

                        public const string Get = $"{Routes.Api.v1.AddressBlockAssignment.Index}/get";
                    }
                }
            }
        }

        public static class QueryParameters
        {
            public const string ReturnUrl = nameof(ReturnUrl);
        }
    }
}
