using System.ComponentModel;

namespace EduSafe.Common.Enums
{
    public enum CollegeType
    {
        Unknown = 0,

        [Description("Public")]
        Public,
        [Description("Private")]
        Private,
        [Description("For-Profit")]
        ForProfit,
    }
}
