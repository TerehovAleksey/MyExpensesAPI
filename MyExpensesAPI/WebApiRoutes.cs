namespace MyExpensesAPI
{
    public static class WebApiRoutes
    {
        public const string API_V1 = "1.0";

        public const string API_PATH = "api/";

        private const string ACCOUNT = "account";
        private const string CATEGORY = "category";
        private const string CURRENCY = "currency";

        public const string API_ACCOUNT = API_PATH + ACCOUNT;
        public const string API_CATEGORY = API_PATH + CATEGORY;
        public const string API_CURRENCY = API_PATH + CURRENCY;
    }
}
