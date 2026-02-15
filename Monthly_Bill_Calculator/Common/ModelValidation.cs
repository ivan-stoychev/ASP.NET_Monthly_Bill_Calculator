namespace Monthly_Bill_Calculator.Common
{
    public static class ModelValidation
    {
        // Price range
        public const string PriceMin = "0.0001";
        public const string PriceMax = "99999";

        // Consumption range
        public const double ConsumptionMin = 0.0001;
        public const double ConsumptionMax = 99999999;

        // Year range
        public const int MinYear = 1900;
        public const int MaxYear = 2100;


        // Month range
        public const int MinMonth = 1;
        public const int MaxMonth = 12;

    }
}