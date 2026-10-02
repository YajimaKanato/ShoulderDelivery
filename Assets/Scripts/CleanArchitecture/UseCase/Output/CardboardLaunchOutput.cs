using ShoulderDelivery.Entity;

namespace ShoulderDelivery.UseCase
{
    /// <summary>段ボールを投げる情報を持つDTO</summary>
    public readonly struct CardboardLaunchOutput
    {
        public readonly Coordinates Position;
        public readonly Coordinates Direction;
        public readonly float InitialSpeed;

        public CardboardLaunchOutput(Coordinates position, Coordinates direction, float initialSpeed)
        {
            Position = position;
            Direction = direction;
            InitialSpeed = initialSpeed;
        }
    }
}
