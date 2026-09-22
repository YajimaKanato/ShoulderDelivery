using System;

namespace ShoulderDelivery.UseCase
{
    /// <summary>ゲームの成功結果を保持するDTO</summary>
    public readonly struct GameClearOutput
    {
        public readonly int Total;
        public readonly int DeliveryCount;
        public readonly float RemainingTime;

        public GameClearOutput(int total, int deliveryCount, float remainingTime)
        {
            if (total < 0)
                throw new ArgumentOutOfRangeException(nameof(total));

            if (deliveryCount < 0)
                throw new ArgumentOutOfRangeException(nameof(deliveryCount));

            if (remainingTime < 0)
                throw new ArgumentOutOfRangeException(nameof(remainingTime));

            Total = total;
            DeliveryCount = deliveryCount;
            RemainingTime = remainingTime;
        }
    }
}
