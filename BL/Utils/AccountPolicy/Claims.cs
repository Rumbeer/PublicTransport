namespace BL.Utils.AccountPolicy
{
    /// <summary>
    /// Defines various roles used within authentication
    /// </summary>
    public static class Claims
    {
        public const string Customer = "Customer";

        public const string Admin = "Administrator";

        public const string AuthenticatedUsers = "Customer, Administrator";
    }
}
