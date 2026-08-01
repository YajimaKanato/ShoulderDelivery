using ShoulderDelivery.Entity;

namespace ShoulderDelivery.UseCase
{
    /// <summary>配達結果を持つDTO</summary>
    public readonly struct ResolveDeliveryInput
    {
        public readonly CardboardId CardboardId;
        public readonly TargetId TargetId;

        public ResolveDeliveryInput(CardboardId cardboardId, TargetId targetId)
        {
            CardboardId = cardboardId;
            TargetId = targetId;
        }
    }
}
