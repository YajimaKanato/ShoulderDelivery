using ShoulderDelivery.Entity;

namespace ShoulderDelivery.UseCase
{
    /// <summary>段ボールを投げる時の情報を持つDTO</summary>
    public readonly struct CardboardLaunchInput
    {
        public readonly Coordinates Position;
        public readonly Coordinates Direction;
        public readonly float InitialSpeed;

        public CardboardLaunchInput(Coordinates position, Coordinates direction, float initialSpeed)
        {
            Position = position;
            Direction = direction;
            InitialSpeed = initialSpeed;
        }
    }
}
