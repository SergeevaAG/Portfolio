namespace CustisBack.APIs.HeadHunter
{
    public static class HeadHunterOptions
    {
        public static string TokenType { get; set; } = "Bearer";
        public static string AccessToken { get; set; } = null; // will be generated automatically
        public static string ClientId { get; set; }
        public static string Secret { get; set; }
        public static string Code { get; set; }
    }
}