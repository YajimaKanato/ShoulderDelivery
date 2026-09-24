namespace ShoulderDelivery.Entity
{
    /// <summary>配達時の情報を持つDTO</summary>
    public readonly struct DeliveryBreakdown
    {
        public readonly int DeliveryCount;
        public readonly int RequiredDeliveryCount;
        public readonly int DeliveryCombo;

        public DeliveryBreakdown(int deliveryCount, int requiredDeliveryCount, int deliveryCombo)
        {
            DeliveryCount = deliveryCount;
            RequiredDeliveryCount = requiredDeliveryCount;
            DeliveryCombo = deliveryCombo;
        }
    }
}
