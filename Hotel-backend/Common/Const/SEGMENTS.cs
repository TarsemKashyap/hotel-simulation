using System.Globalization;
using System.Runtime.Intrinsics.X86;

namespace Common
{
    public class SEGMENTS
    {
        public const string BUSINESS = "Business";
        public const string SMALL_BUSINESS = "Small Business";
        public const string CORPORATE_CONTRACT = "Corporate contract";
        public const string FAMILIES = "Families";
        public const string AFLUENT_MATURE_TRAVELERS = "Afluent Mature Travelers";
        public const string INTERNATIONAL_LEISURE_TRAVELERS = "International leisure travelers";
        public const string CORPORATE_BUSINESS_MEETINGS = "Corporate/Business Meetings";
        public const string ASSOCIATION_MEETINGS = "Association Meetings";
        public static readonly string[] list = new string[] { BUSINESS, SMALL_BUSINESS, CORPORATE_CONTRACT, FAMILIES, AFLUENT_MATURE_TRAVELERS, INTERNATIONAL_LEISURE_TRAVELERS, CORPORATE_BUSINESS_MEETINGS, ASSOCIATION_MEETINGS };

        public static string UI_Label(string label)
        {
            return label switch
            {
                BUSINESS => BUSINESS,
                SMALL_BUSINESS => SMALL_BUSINESS,
                CORPORATE_CONTRACT => "Corporate Contract",
                FAMILIES => FAMILIES,
                AFLUENT_MATURE_TRAVELERS => AFLUENT_MATURE_TRAVELERS,
                INTERNATIONAL_LEISURE_TRAVELERS => "International Leisure Travelers",
                CORPORATE_BUSINESS_MEETINGS => CORPORATE_BUSINESS_MEETINGS,
                ASSOCIATION_MEETINGS => ASSOCIATION_MEETINGS,
                _ => label
            };
        }
    }



    public class MARKETING_TECHNIQUE
    {
        public const string ADVERTISING = "Advertising";
        public const string SALES_FORCE = "Sales Force";
        public const string PROMOTIONS = "Promotions";
        public const string PUBLIC_RELATIONS = "Public Relations";
    }
}
