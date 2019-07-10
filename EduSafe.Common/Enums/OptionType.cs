using System.ComponentModel;

namespace EduSafe.Common.Enums
{
    public enum OptionType
    {
        [Description("None")]
        Unknown = 0,

        [Description("Unemployment")]
        UnemploymentOption,
        [Description("Graduate School")]
        GradSchoolOption,
        [Description("Terminate Education")]
        TerminationOption,
        [Description("Terminate Education")]
        TerminationWarranty,
        [Description("Early Hire")]
        EarlyHireOption,
        [Description("College Closure")]
        CollegeClosureOption,
    }
}
