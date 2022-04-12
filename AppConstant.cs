public static class AppConstant
{
    public const string ADMIN_ROLE = "Administrator";
    public const string CUSTOMER_ROLE = "Customer";

    public static int DEFAULT_PAGE_COUNT = 100000;

    public static class StatusOrder {
        public const short MASUK = 1;
        public const short DIBAYAR = 2;
        public const short DIPROSES = 3;
        public const short DIKIRIM = 4;
        public const short DITERIMA = 5;
    }
}