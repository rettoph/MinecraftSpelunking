namespace MinecraftSpelunking.Presentation.Common
{
    public static class Constants
    {
        public static class Routes
        {
            public const string Home = "/";

            public static class Account
            {
                public const string Index = $"/{nameof(Account)}";
                public const string RegisterAdmin = $"{Routes.Account.Index}/{nameof(RegisterAdmin)}";
                public const string Login = $"{Routes.Account.Index}/{nameof(Login)}";
                public const string Logout = $"{Routes.Account.Index}/{nameof(Logout)}";
            }

            public static class AddressBlocks
            {
                public const string Index = $"/{nameof(AddressBlocks)}";
                public const string BulkRequestSubNetworks = $"{Routes.AddressBlocks.Index}/{nameof(BulkRequestSubNetworks)}";
            }

            public static class JavaServers
            {
                public const string View = $"/{nameof(View)}";
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
                        public const string Complete = $"{Routes.Api.v1.AddressBlockAssignment.Index}/complete";
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
