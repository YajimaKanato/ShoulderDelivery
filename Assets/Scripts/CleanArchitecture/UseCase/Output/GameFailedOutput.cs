using System;

namespace ShoulderDelivery.UseCase
{
    /// <summary>ゲームの失敗結果を持つDTO</summary>
    public readonly struct GameFailedOutput
    {
        public readonly int Total;
        public readonly int DeliveryCount;
        public readonly float RemainingTime;

        public GameFailedOutput(int total, int deliveryCount, float remainingTime)
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
