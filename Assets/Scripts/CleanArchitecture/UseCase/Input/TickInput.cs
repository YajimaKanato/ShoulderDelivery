using System;

namespace ShoulderDelivery.UseCase
{
    /// <summary>フレーム間で更新するゲームの情報を持つDTO</summary>
    public readonly struct TickInput
    {
        public readonly float Delta;

        public TickInput(float delta)
        {
            if (delta < 0)
                throw new ArgumentOutOfRangeException(nameof(delta));

            Delta = delta;
        }
    }
}
