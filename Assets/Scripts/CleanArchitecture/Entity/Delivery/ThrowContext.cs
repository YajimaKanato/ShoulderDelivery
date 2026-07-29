namespace ShoulderDelivery.Entity
{
    /// <summary>投擲した時の情報を持つ構造体</summary>
    public readonly struct ThrowContext
    {
        /// <summary>投擲した時の移動速度</summary>
        public readonly float Speed;
        /// <summary>投擲場所</summary>
        public readonly Vector3 Position;
        /// <summary>投擲した時の回転（バイクアクション）</summary>
        public readonly Vector3 Rotation;

        public ThrowContext(float speed, Vector3 position, Vector3 rotation)
        {
            Speed = speed;
            Position = position;
            Rotation = rotation;
        }
    }
}
