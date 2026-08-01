using ShoulderDelivery.Entity;

namespace ShoulderDelivery.UseCase
{
    /// <summary>投擲結果を持つDTO</summary>
    public readonly struct ThrowCardboardOutput
    {
        public readonly bool IsAccepted;
        public readonly CardboardId? CardboardId;

        public ThrowCardboardOutput(bool isAccepted, CardboardId? cardboardId)
        {
            IsAccepted = isAccepted;
            CardboardId = cardboardId;
        }
    }
}
