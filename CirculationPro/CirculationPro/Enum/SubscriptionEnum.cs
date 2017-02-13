using System.ComponentModel;

namespace CirculationPro.Enum
{
    public class SubscriptionEnum
    {
        public static readonly SubscriptionEnum ACTIVE = new SubscriptionEnum("Active", 0);
        public static readonly SubscriptionEnum INACTIVE = new SubscriptionEnum("Inactive", 1);
        public static readonly SubscriptionEnum VAC_NIE = new SubscriptionEnum("VAC_NIE", 2);
        public static readonly SubscriptionEnum VAC_STOPPED = new SubscriptionEnum("VAC_STOPPED", 3);
        public static readonly SubscriptionEnum VAC_HELD = new SubscriptionEnum("VAC_HELD", 4);
        public static readonly SubscriptionEnum VAC_FORWARD = new SubscriptionEnum("VAC_FORWARD", 5);

        private string value;
        private int ordinal;

        public SubscriptionEnum(string value, int ordinal)
        {
            this.value = value;
            this.ordinal = ordinal;
        }

        public override string ToString()
        {
            return this.value;
        }
    }
}
