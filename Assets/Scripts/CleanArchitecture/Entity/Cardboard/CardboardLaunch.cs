namespace ShoulderDelivery.Entity
{
    /// <summary>段ボールを投げる時の情報を持つDTO</summary>
    public readonly struct CardboardLaunch
    {
        public readonly Coordinates Position;
        public readonly Coordinates Rotation;
        public readonly Coordinates Direction;
        public readonly float ThrowPower;

        public CardboardLaunch(Coordinates position
            , Coordinates rotation
            , Coordinates direction
            , float throwPower)
        {
            Position = position;
            Rotation = rotation;
            Direction = direction;
            ThrowPower = throwPower;
        }
    }
}
