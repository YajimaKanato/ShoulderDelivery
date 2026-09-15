namespace ShoulderDelivery.Entity
{
    /// <summary>投擲した時の情報を持つ構造体</summary>
    public readonly struct ThrowContext
    {
        /// <summary>投擲した時の移動速度</summary>
        public readonly float Speed;
        /// <summary>投擲場所</summary>
        public readonly Coordinates Position;
        /// <summary>投擲した時の回転（バイクアクション）</summary>
        public readonly Coordinates Rotation;

        public ThrowContext(float speed, Coordinates position, Coordinates rotation)
        {
            Speed = speed;
            Position = position;
            Rotation = rotation;
        }
    }
}
