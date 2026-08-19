using ShoulderDelivery.Entity;

namespace ShoulderDelivery.UseCase
{
    /// <summary>段ボールが消えたことに関する情報を持つDTO</summary>
    public readonly struct DiscardCardboardInput
    {
        public readonly CardboardId Id;

        public DiscardCardboardInput(CardboardId id)
        {
            Id = id;
        }
    }
}
