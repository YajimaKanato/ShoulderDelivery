using ShoulderDelivery.Entity;

namespace ShoulderDelivery.UseCase
{
    /// <summary>投擲成功結果を持つDTO</summary>
    public readonly struct ThrowCardboardAcceptOutput
    {
        public readonly CardboardId CardboardId;

        public ThrowCardboardAcceptOutput(CardboardId cardboardId)
        {
            CardboardId = cardboardId;
        }
    }
}
